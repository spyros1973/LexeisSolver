Imports System.IO

Public Class SolverDictionary
    Private _words() As List(Of String)
    Private _alphabet() As Char = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ"
    Private _fromReplace() As Char = "άέήίϊΐόύϋΰώΆΈΉΊΌΎΏς"
    Private _consonnants As String = ""
    Private _toReplace() As Char = "αεηιιιουυυωαεηιουωσ"

    Public Sub New(language As String)

        If language.ToLower = "gr" Then
            _alphabet = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ"
            _fromReplace = "άέήίϊΐόύϋΰώΆΈΉΊΌΎΏς"
            _toReplace = "αεηιιιουυυωαεηιουωσ"
            _consonnants = "ΒΓΔΖΘΚΛΜΝΞΠΡΣΤΦΧΨ"
        ElseIf language.ToLower = "en" Then
            _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            _fromReplace = ""
            _toReplace = ""
            _consonnants = "BCDFGHJKLMNPQRSTVWXZ"
        Else 'language="sp"
            _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            _fromReplace = "áéóíñÁÉÓÍÑÚú"
            _toReplace = "aeoinAEOINUU"
            _consonnants = "BCDFGHJKLMNPQRSTVWXZ"

        End If
        ReDim _words(_alphabet.Length)

        For i As Integer = 0 To _words.Length - 1 : _words(i) = New List(Of String) : Next
        Dim cnt As Integer = 0
        Dim path As String = IO.Path.Combine(Application.StartupPath, $"dict-{language}.txt")
        If Not IO.File.Exists(path) Then Exit Sub
        Dim entries As String() = File.ReadAllLines(path)

        Dim n As Integer = 0
        For Each s As String In entries
            If s.Length >= 3 And s.Length <= 10 And Not s.Contains("-") And Not s.Contains("‒́") And Not s.Contains("(") Then
                n += 1
                s = SanitizeWord(s)
                Dim letterIndex = InStr(_alphabet, s.Substring(0, 1)) - 1
                'If _words(letterIndex).Count < 5000 Then
                _words(letterIndex).Add(s)
                'cnt += 1
                'End If
            Else
                'Debug.WriteLine(s & " not allowed (length is " & s.Length & ")")
            End If
        Next
        'For i As Integer = 0 To 23
        '    System.Diagnostics.Debug.WriteLine(_alphabet(i) & ": " & _words(i).Count & " words")
        'Next
    End Sub

    Public Function IsConsonnant(s As String) As Boolean
        Return _consonnants.Contains(s)
    End Function


    Private Function SanitizeWord(txt As String) As String
        For i As Integer = 0 To _fromReplace.Length - 1
            txt = txt.Replace(_fromReplace(i), _toReplace(i))
        Next
        Return txt.ToUpper
    End Function

    Public Function ContainsWord(txt As String) As Boolean
        Dim letterIndex = InStr(_alphabet, txt.Substring(0, 1)) - 1
        Return _words(letterIndex).Contains(txt.ToUpper)
    End Function

    Public Function ContainsWordsThatStartWith(txt As String) As Boolean
        Dim letterIndex = InStr(_alphabet, txt.Substring(0, 1)) - 1
        Dim s As String = txt
        Return _words(letterIndex).Find(Function(x) x.StartsWith(txt)) <> ""
    End Function

    Public Function NumberOfWords() As Integer
        Dim ret As Integer = 0
        For i As Integer = 0 To _words.Length - 1
            ret += _words(i).Count
        Next
        Return ret
    End Function

    Public Function GetRandomWord(minLength As String, maxLength As String) As String
        Dim r As New Random
        Dim ret As String = ""
        While ret = "" Or (ret.Length < minLength Or ret.Length > maxLength)
            Dim l As Integer = r.Next(_words.Length)
            Try
                If (_words(l).Count > 0) Then ret = _words(l)(r.Next(_words(l).Count))
            Catch ex As Exception
                Dim s As String = ex.Message
            End Try
        End While
        Return ret
    End Function
End Class
