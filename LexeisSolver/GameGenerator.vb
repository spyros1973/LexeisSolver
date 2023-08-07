Imports System.Web.Script.Serialization
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class GameGenerator
    Private Class Level
        Public Property Filename As String
        Public Property Points As Integer
        Public Property Words As Integer
    End Class
    Private Class Board
        Public Property Board As New List(Of List(Of String))
        Public Property Words As New List(Of String)

        Public ReadOnly Property BoardSetup As String
            Get
                Dim tiles As String = ""
                For Each r As List(Of String) In Board
                    For Each c As String In r
                        tiles &= c
                    Next
                Next
                Return tiles
            End Get
        End Property

        Public ReadOnly Property Points As Integer
            Get
                ' 3 = 3 points, 4 = 4 points, 5 = 6 points, 6 = 7 points, 7 = 9 points, 8 = 10 points
                Dim ret As Integer = 0
                For Each s As String In Words
                    ret += s.Length
                    If s.Length = 5 Or s.Length = 6 Then
                        ret += 1
                    End If
                    If s.Length = 7 Or s.Length = 8 Then
                        ret += 2
                    End If
                Next
                Return ret
            End Get
        End Property

        Public Overrides Function ToString() As String
            Dim sb As New Text.StringBuilder
            sb.AppendLine("Board setup: " & BoardSetup)
            sb.AppendLine("Point value: " & Points)
            sb.AppendLine("Words: " & Words.Count)
            'For Each s As String In Words
            '    sb.AppendLine(s)
            'Next
            Return sb.ToString
        End Function
        Public Function ToJson() As String
            Dim d As New JavaScriptSerializer
            Return d.Serialize(Me)
        End Function
    End Class

    Private Async Sub Generate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If txtOutputPath.Text.Trim = "" OrElse Not IO.Directory.Exists(txtOutputPath.Text) Then
            MsgBox("Output path missing or invalid",, "Invalid path")
            Return
        End If
        Dim numberOfGames As Integer = numGames.Value
        Dim minimumWords As Integer = numWordThreshold.Value
        prg.Visible = True
        btnGenerate.Enabled = False
        prg.Minimum = 0
        prg.Maximum = numberOfGames
        prg.Value = 0
        My.Settings.GameOutputPath = txtOutputPath.Text
        Randomize()
        Dim dict As New SolverDictionary
        Dim gamesCreated As Integer = 0
        txtOut.Text = "Generating " & numGames.Value & " games..." & vbCrLf
        Dim levels As New List(Of Level)
        While gamesCreated < numberOfGames
            Dim b As Board = Await GenerateGame(minimumWords, dict)
            If b Is Nothing Then
                txtOut.Text &= "Game below word threshold - retrying..." & vbCrLf
            Else
                txtOut.Text &= b.ToString & vbCrLf
                txtOut.SelectionStart = txtOut.Text.Length
                txtOut.ScrollToCaret()
                Dim filename As String = (gamesCreated + 1) & ".json"
                levels.Add(New Level With {.Filename = filename, .Points = b.Points, .Words = b.Words.Count})
                IO.File.WriteAllText(IO.Path.Combine(txtOutputPath.Text, filename), b.ToJson)
                prg.Value += 1
                gamesCreated += 1
            End If
        End While
        levels.Sort(AddressOf LevelComparer)
        Dim jss As New JavaScriptSerializer

        IO.File.WriteAllText(IO.Path.Combine(txtOutputPath.Text, "Index.txt"), jss.Serialize(levels))
        btnGenerate.Enabled = True
        prg.Visible = False
    End Sub

    Private Function LevelComparer(x As Level, y As Level) As Integer
        Return x.Points < y.Points
    End Function


    Private Async Function GenerateGame(minimumWords As Integer, dict As SolverDictionary) As Task(Of Board)
        Dim ret As Board = Nothing
        Dim boardSetup As String = ""
        Dim letters As String = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩΑΕΣ"

        For i As Integer = 0 To 15
            Dim idx As Integer = CInt(Rnd() * (letters.Length - 1))
            boardSetup &= letters.Substring(idx, 1)
        Next
        Dim slv As New Solver(boardSetup, dict)
        slv.MinimumLength = 3
        slv.MaximumLength = 8

        Await Task.Run(Sub()
                           slv.Solve()
                       End Sub)

        If slv.WordsFound.Count > minimumWords Then
            slv.WordsFound.Sort()
            ret = New Board
            For i As Integer = 0 To 3
                Dim row As New List(Of String)
                For n As Integer = 0 To 3
                    row.Add(boardSetup.Substring(i * 3 + n, 1))
                Next
                ret.Board.Add(row)
            Next
            For Each s As String In slv.WordsFound
                ret.Words.Add(s)
            Next
        End If
        Return ret
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim o As New FolderBrowserDialog
        If o.ShowDialog = DialogResult.OK Then
            txtOutputPath.Text = o.SelectedPath
        End If
    End Sub

    Private Sub GameGenerator_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtOutputPath.Text = My.Settings.GameOutputPath
    End Sub
End Class