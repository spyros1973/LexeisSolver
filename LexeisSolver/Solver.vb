Public Class Solver
    Dim _board As String
    Dim _wordsFound As New ArrayList
    Dim _dictionary As SolverDictionary
    Dim _minLength As Integer = 3
    Dim _maxLength As Integer = 10
    Public Event WordChecked(word As String)

    Public Sub New(board As String)
        _board = board
        _dictionary = New SolverDictionary
    End Sub

    Public Sub New(board As String, dictionary As SolverDictionary)
        _board = board
        _dictionary = dictionary
    End Sub

    Public WriteOnly Property MinimumLength As Integer
        Set(value As Integer)
            _minLength = value
        End Set
    End Property

    Public WriteOnly Property MaximumLength As Integer
        Set(value As Integer)
            _maxLength = value
        End Set
    End Property

    Private Function ValidMoves(route As ArrayList) As ArrayList
        Dim current As Integer = route.Item(route.Count - 1)
        Dim valid As New ArrayList
        Select Case current
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
        For Each i As Integer In route
            If valid.Contains(i) Then
                valid.Remove(i)
            End If
        Next
        Return valid
    End Function

    Private Sub CheckRoute(currentRoute As ArrayList)
        Dim word As String = ""
        'System.Diagnostics.Debug.WriteLine(writeRoute(currentRoute))

        If currentRoute.Count > _maxLength Then
            Return
        End If

        'if no hope - return

        For n As Integer = 0 To currentRoute.Count - 1
            word &= _board.Substring(currentRoute.Item(n) - 1, 1)
        Next
        RaiseEvent WordChecked(word)
        If word.Length >= _minLength And word.Length <= _maxLength And Not _wordsFound.Contains(word) And _dictionary.ContainsWord(word) Then
            _wordsFound.Add(word)
        End If


        If Not _dictionary.ContainsWordsThatStartWith(word) Then Exit Sub

        Dim moves As ArrayList = ValidMoves(currentRoute)


        Do While moves.Count > 0
            If currentRoute.Count < _maxLength Then
                currentRoute.Add(moves.Item(0))
                CheckRoute(currentRoute)
                currentRoute.RemoveAt(currentRoute.Count - 1)
            End If
            moves.RemoveAt(0)
        Loop
    End Sub

    Private Function WriteRoute(route As ArrayList) As String
        Dim s As String = ""
        For Each i As Integer In route
            s = s & i.ToString & ","
        Next
        If s.EndsWith(","c) Then s = s.Substring(0, s.Length - 1)
        Return s
    End Function

    Public Sub Solve()
        For i As Integer = 1 To 16
            Dim r As New ArrayList
            r.Add(i)
            CheckRoute(r)
        Next
    End Sub

    Public ReadOnly Property WordsFound As ArrayList
        Get
            Return _wordsFound
        End Get
    End Property
End Class
