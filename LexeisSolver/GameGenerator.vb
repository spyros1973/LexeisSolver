Imports System.Web.Script.Serialization

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
        Public Shared Function FromJson(json) As Board
            Dim d As New JavaScriptSerializer
            Try
                Return d.Deserialize(Of Board)(json)
            Catch ex As Exception
                Return Nothing
            End Try

        End Function
    End Class

    Private Async Sub Generate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
        If txtOutputPath.Text.Trim = "" OrElse Not IO.Directory.Exists(txtOutputPath.Text) Then
            MsgBox("Output path missing or invalid",, "Invalid path")
            Return
        End If
        Dim numberOfGames As Integer = numGames.Value
        Dim minimumWords As Integer = numWordThreshold.Value
        Dim minimumLongestWordLength As Integer = numMinLongestWordLength.Value
        Dim language As String
        If cmbLanguage.SelectedIndex = 0 Then
            language = "gr"
        ElseIf cmbLanguage.SelectedIndex = 1 Then
            language = "en"
        Else
            language = "sp"
        End If
        prg.Visible = True
        btnGenerate.Enabled = False
        prg.Minimum = 0
        prg.Maximum = numberOfGames
        prg.Value = 0
        My.Settings.GameOutputPath = txtOutputPath.Text
        My.Settings.Language = cmbLanguage.Text
        Randomize()
        Dim dict As New SolverDictionary(language)
        If dict.NumberOfWords = 0 Then txtOut.Text = "No words in dictionary - terminating process" : Return
        Dim gamesCreated As Integer = 0
        txtOut.Text = "Generating " & numGames.Value & " games..." & vbCrLf
        Dim levels As New List(Of Level)
        While gamesCreated < numberOfGames
            Dim b As Board = Await GenerateGame(minimumWords, minimumLongestWordLength, dict, language, numMinWordTimes.Value, chkRequireVowelNeighbors.Checked)
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

    Private Function GetNeighbors(pos As Integer) As List(Of Integer) 'should be changed to allow other board sizes
        Dim valid As New List(Of Integer)
        Select Case pos
            Case 1
                valid.Add(2)
                valid.Add(5)
                valid.Add(6)
            Case 2
                valid.Add(1)
                valid.Add(3)
                valid.Add(5)
                valid.Add(6)
                valid.Add(7)
            Case 3
                valid.Add(2)
                valid.Add(4)
                valid.Add(6)
                valid.Add(7)
                valid.Add(8)
            Case 4
                valid.Add(3)
                valid.Add(7)
                valid.Add(8)
            Case 5
                valid.Add(1)
                valid.Add(2)
                valid.Add(6)
                valid.Add(9)
                valid.Add(10)
            Case 6
                valid.Add(1)
                valid.Add(2)
                valid.Add(3)
                valid.Add(5)
                valid.Add(7)
                valid.Add(9)
                valid.Add(10)
                valid.Add(11)
            Case 7
                valid.Add(2)
                valid.Add(3)
                valid.Add(4)
                valid.Add(6)
                valid.Add(8)
                valid.Add(10)
                valid.Add(11)
                valid.Add(12)
            Case 8
                valid.AddRange({3, 4, 7, 11, 12})
            Case 9
                valid.AddRange({5, 6, 10, 13, 14})
            Case 10
                valid.AddRange({5, 6, 7, 9, 11, 13, 14, 15})
            Case 11
                valid.AddRange({6, 7, 8, 10, 12, 14, 15, 16})
            Case 12
                valid.AddRange({7, 8, 11, 15, 16})
            Case 13
                valid.AddRange({9, 10, 14})
            Case 14
                valid.AddRange({9, 10, 11, 13, 15})
            Case 15
                valid.AddRange({10, 11, 12, 14, 16})
            Case 16
                valid.AddRange({11, 12, 15})
        End Select
        For i As Integer = 0 To valid.Count - 1
            valid(i) = valid(i) - 1
        Next
        Return valid
    End Function

    Private Function IsConsonnant(s As String) As Boolean
        Return "ΒΓΔΖΘΚΛΜΝΞΠΡΣΤΦΧΨ".Contains(s)
    End Function

    Private Function CreateInitialBoard(dict As SolverDictionary, minWordLength As Integer, maxWordLength As Integer, letters As String) As String
        'different approach:
        'add one big word first and then fill in the rest
        'pick word: ΕΛΑΣΣΩΝ
        'place: EA__ Λ_ΣΣ ΝΩ__ ____
        '   pick random pos, add letter > add next to one random neighbor with no value > if no neighbors with no value exist, restart
        'replace _ with random letters

        Dim brd As New List(Of String)
        For i As Integer = 0 To 15
            brd.Add("_")
        Next
        Dim wrd As String = ""
TryCombinations:
        brd = New List(Of String)
        For i As Integer = 0 To 15
            brd.Add("_")
        Next
        wrd = dict.GetRandomWord(minWordLength, maxWordLength)
        Dim r As New Random
        Dim pos As Integer = r.Next(16) + 1
        Dim wi As Integer = 0
        While (wi < wrd.Length - 1)
            brd(pos - 1) = wrd(wi)
            Dim neighbors As List(Of Integer) = GetNeighbors(pos)
            Dim nextPos As Integer = -1
            Dim tries As Integer = 0
            Do
                If nextPos <> -1 AndAlso brd(nextPos - 1) = "_" Then
                    wi += 1
                    pos = nextPos
                    Exit Do
                Else
                    nextPos = neighbors(r.Next(neighbors.Count)) + 1
                    tries += 1
                    If tries > 15 Then
                        'If wi > 0 Then wi -= 1
                        brd(pos - 1) = "_"
                        GoTo TryCombinations
                    End If
                End If
            Loop

        End While
        For i As Integer = 0 To brd.Count - 1
            If brd(i) = "_" Then
                Dim idx As Integer = CInt(Rnd() * (letters.Length - 1))
                brd(i) = letters.Substring(idx, 1)
            End If
        Next
        Return String.Join("", brd)
    End Function

    Private Sub AppendToSolutionLog(lang As String, brd As Board)
        Dim fw As System.IO.StreamWriter
        fw = My.Computer.FileSystem.OpenTextFileWriter(IO.Path.Combine(Application.StartupPath, $"solution-{lang}.txt"), True)
        fw.WriteLine(brd.ToJson)
        fw.Close()
    End Sub

    Private Function GetSolutionFromLog(lang As String, letters As String) As Board
        Dim ret As Board = Nothing
        Dim filename As String = IO.Path.Combine(Application.StartupPath, $"solution-{lang}.txt")
        If Not IO.File.Exists(filename) Then Return Nothing
        Dim fileReader As System.IO.StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(filename, System.Text.Encoding.UTF8)
        Do While Not fileReader.EndOfStream
            Dim s As String = fileReader.ReadLine()
            If s.Contains(""" & letters & """) Then
                ret = Board.FromJson(s)
                Exit Do
            End If
        Loop
        fileReader.Close()
        Return ret
    End Function

    Private Async Function GenerateGame(minimumWords As Integer, minimumLongestWordLength As Integer, dict As SolverDictionary, language As String, timesMinimumLongestWord As Integer, requireVowelNeighbors As Boolean) As Task(Of Board)
        Dim ret As Board = Nothing
        Dim boardSetup As String = ""
        Dim letters As String
        If language = "gr" Then
            letters = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩΑΕΣΚΠΟΑΕΙ"
        ElseIf language = "en" Then
            letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZAEDMFTHS"
        Else
            letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        End If

        Dim isOk As Boolean = False
        Do While Not isOk
            boardSetup = CreateInitialBoard(dict, minimumLongestWordLength, 8, letters)

            'boardSetup = ""
            'For i As Integer = 0 To 15
            '    Dim idx As Integer = CInt(Rnd() * (letters.Length - 1))
            '    boardSetup &= letters.Substring(idx, 1)
            'Next
            isOk = True
            If requireVowelNeighbors Then
                For i As Integer = 0 To 15
                    'if consonnant, make sure that at least some neighbors are vowels
                    If dict.IsConsonnant(boardSetup.Substring(i, 1)) Then
                        Dim cc As Integer = 0
                        Dim ttt As List(Of Integer) = GetNeighbors(i + 1)
                        For Each neighbor As String In GetNeighbors(i + 1)
                            If dict.IsConsonnant(boardSetup.Substring(neighbor, 1)) Then cc += 1
                        Next
                        If Not (cc < GetNeighbors(i).Count / 2) Then
                            isOk = False
                            Exit For
                        End If
                    End If
                Next
            End If
        Loop

        'check if in solution log, otherwise solve
        ret = GetSolutionFromLog(language, boardSetup)
        If ret Is Nothing Then
            Dim slv As New Solver(boardSetup, dict)
            slv.MinimumLength = 3
            slv.MaximumLength = 8

            Await Task.Run(Sub()
                               slv.Solve()
                           End Sub)

            Dim longestWordLength As Integer = 0
            Dim longestWordCounter As Integer = 0
            For Each s As String In slv.WordsFound
                If s.Length > longestWordLength Then longestWordLength = s.Length
            Next

            For Each s As String In slv.WordsFound
                If s.Length >= minimumLongestWordLength Then longestWordCounter += 1
            Next
            'write in solution file

            If slv.WordsFound.Count > minimumWords And longestWordLength >= minimumLongestWordLength And longestWordCounter >= timesMinimumLongestWord Then
                slv.WordsFound.Sort()
                ret = New Board
                Dim c As Integer = 0
                For i As Integer = 0 To 3
                    Dim row As New List(Of String)
                    For n As Integer = 0 To 3
                        row.Add(boardSetup.Substring(c, 1))
                        c += 1
                    Next
                    ret.Board.Add(row)
                Next
                For Each s As String In slv.WordsFound
                    ret.Words.Add(s)
                Next
                AppendToSolutionLog(language, ret)
            End If

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
        cmbLanguage.Items.Add("Greek")
        cmbLanguage.Items.Add("English")
        cmbLanguage.Items.Add("Spanish")
        cmbLanguage.Text = My.Settings.Language
        If cmbLanguage.Text = "" Then cmbLanguage.SelectedIndex = 0
    End Sub
End Class