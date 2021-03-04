Public Class Form1
    Dim _dict As SolverDictionary

    Private Sub btnQuickEnter_Click(sender As Object, e As EventArgs) Handles btnQuickEnter.Click
        If txtQuick.Text.Length = 16 Then
            For i As Integer = 0 To 15
                Dim s As String = "t" & (i + 1).ToString("00")
                Dim c As Control() = Me.Controls.Find(s, True)
                c(0).Text = txtQuick.Text.Substring(i, 1).ToUpper
            Next
        End If
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If nudMax.Value < nudMin.Value Then nudMax.Value = nudMin.Value
        Dim startTime As DateTime = DateTime.Now
        lstWordsFound.Items.Clear()
        Button1.Enabled = False
        lblResultsCount.Text = ""
        btnQuickEnter.Enabled = False
        btnCheck.Enabled = False
        btnSortWordList.Enabled = False
        Dim board As String = ""
        For i As Integer = 0 To 15
            Dim s As String = "t" & (i + 1).ToString("00")
            Dim c As Control() = Me.Controls.Find(s, True)
            board &= c(0).Text
        Next

        Dim slv As New Solver(board, _dict)
        slv.MinimumLength = nudMin.Value
        slv.MaximumLength = nudMax.Value
        AddHandler slv.WordChecked, AddressOf WordChecked

        Await Task.Run(Sub()
                           slv.Solve()
                       End Sub)


        For Each s As String In slv.WordsFound
            lstWordsFound.Items.Add(s)
        Next
        Dim elapsedTime As TimeSpan = DateTime.Now.Subtract(startTime)

        lblResultsCount.Text = slv.WordsFound.Count.ToString & " words in " & elapsedTime.TotalSeconds.ToString("0.00") & "secs"
        Button1.Enabled = True        
        btnQuickEnter.Enabled = True
        btnCheck.Enabled = True
        btnSortWordList.Tag = 1
        SortWordList(0)
        btnSortWordList.Enabled = True
        lblStatus.Text = ""
    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnSortWordList.Tag = 0
        lblStatus.Text = "Reading dictionary..."
        Await Task.Run(Sub()
                           _dict = New SolverDictionary
                       End Sub)
        lblStatus.Text = _dict.NumberOfWords & " words read"
    End Sub

    Private Delegate Sub WordCheckDelegate(txt As String)
    Private Sub WordChecked(txt As String)

        If Me.InvokeRequired Then
            Me.Invoke(New WordCheckDelegate(AddressOf WordChecked), txt)
        Else
            lblStatus.Text = txt
        End If

    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        Dim s As String = txtCheck.Text.ToUpper
        If s <> "" Then
            Dim exists As Boolean = _dict.ContainsWord(s)
            Dim startsWith As Boolean = _dict.ContainsWordsThatStartWith(s)
            Dim out As String = "Exists: " & exists & vbCrLf & "Start with: " & startsWith
            MsgBox(out)
        End If
    End Sub

    Private Sub btnSortWordList_Click(sender As Object, e As EventArgs) Handles btnSortWordList.Click
        If btnSortWordList.Tag = 0 Then
            btnSortWordList.Tag = 1
            SortWordList(0)
        Else
            SortWordList(1)
            btnSortWordList.Tag = 0
        End If
    End Sub

    Private Sub SortWordList(mode As Integer)
        lstWordsFound.BeginUpdate()
        If (lstWordsFound.Items.Count > 1) Then

            Dim swapped As Boolean

            Do
                Dim counter As Integer = lstWordsFound.Items.Count - 1
                swapped = False
                While (counter > 0)

                    If mode = 0 Then 'long-short
                        ' Compare the items' length. 
                        If lstWordsFound.Items(counter).ToString.Length > _
                           lstWordsFound.Items(counter - 1).ToString.Length Then

                            ' If true, swap the items. 
                            Dim temp As Object = lstWordsFound.Items(counter)
                            lstWordsFound.Items(counter) = lstWordsFound.Items(counter - 1)
                            lstWordsFound.Items(counter - 1) = temp
                            swapped = True

                        End If
                    Else
                        'compare alhabetically
                        If lstWordsFound.Items(counter).ToString < _
                           lstWordsFound.Items(counter - 1).ToString Then

                            ' If true, swap the items. 
                            Dim temp As Object = lstWordsFound.Items(counter)
                            lstWordsFound.Items(counter) = lstWordsFound.Items(counter - 1)
                            lstWordsFound.Items(counter - 1) = temp
                            swapped = True

                        End If

                    End If
                    ' Decrement the counter.
                    counter -= 1
                End While
            Loop While (swapped = True)
            lstWordsFound.EndUpdate()
        End If
    End Sub

    Private Sub txtQuick_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQuick.KeyDown
        If e.KeyCode = Keys.Enter Then btnQuickEnter_Click(sender, Nothing)
    End Sub


    Private Sub txtCheck_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCheck.KeyDown
        If e.KeyCode = Keys.Enter And txtCheck.Text.Length > 0 Then btnCheck_Click(sender, Nothing)
    End Sub
End Class
