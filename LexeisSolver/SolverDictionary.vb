Imports System.IO

Public Class TrieNode
    Public Children As New Dictionary(Of Char, TrieNode)
    Public IsEndOfWord As Boolean = False
End Class

Public Class Trie
    Private _root As New TrieNode

    Public Sub Insert(word As String)
        Dim node As TrieNode = _root
        For Each c As Char In word
            If Not node.Children.ContainsKey(c) Then
                node.Children(c) = New TrieNode()
            End If
            node = node.Children(c)
        Next
        node.IsEndOfWord = True
    End Sub

    Public Function StartsWithPrefix(prefix As String) As Boolean
        Dim node As TrieNode = _root
        For Each c As Char In prefix
            If Not node.Children.ContainsKey(c) Then
                Return False
            End If
            node = node.Children(c)
        Next
        Return True
    End Function
End Class

Public Class SolverDictionary
    Private _words() As HashSet(Of String)
    Private _trieByLetter() As Trie
    Private _alphabet() As Char = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ"
    Private _fromReplace() As Char = "άέήίϊΐόύϋΰώΆΈΉΊΌΎΏς"
    Private _consonnants As String = ""
    Private _toReplace() As Char = "αεηιιιουυυωαεηιουωσ"
    Private _allWords As List(Of String)

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
        ReDim _trieByLetter(_alphabet.Length)
        _allWords = New List(Of String)

        For i As Integer = 0 To _words.Length - 1
            _words(i) = New HashSet(Of String)
            _trieByLetter(i) = New Trie()
        Next

        Dim path As String = IO.Path.Combine(Application.StartupPath, $"dict-{language}.txt")
        If Not IO.File.Exists(path) Then Exit Sub
        Dim entries As String() = File.ReadAllLines(path)

        For Each s As String In entries
            If s.Length >= 3 And s.Length <= 10 And Not s.Contains("-") And Not s.Contains("‒́") And Not s.Contains("(") Then
                s = SanitizeWord(s)
                Dim letterIndex = GetLetterIndex(s.Substring(0, 1))
                _words(letterIndex).Add(s)
                _allWords.Add(s)
            End If
        Next

        For i As Integer = 0 To _alphabet.Length - 1
            For Each word As String In _words(i)
                _trieByLetter(i).Insert(word)
            Next
        Next
    End Sub

    Private Function GetLetterIndex(letter As String) As Integer
        Return InStr(_alphabet, letter.Substring(0, 1)) - 1
    End Function

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
        Dim letterIndex = GetLetterIndex(txt)
        Return _words(letterIndex).Contains(txt.ToUpper)
    End Function

    Public Function ContainsWordsThatStartWith(txt As String) As Boolean
        Dim letterIndex = GetLetterIndex(txt)
        Return _trieByLetter(letterIndex).StartsWithPrefix(txt.ToUpper)
    End Function

    Public Function NumberOfWords() As Integer
        Return _allWords.Count
    End Function

    Public Function GetRandomWord(minLength As Integer, maxLength As Integer) As String
        Dim r As New Random
        Dim ret As String = ""
        Dim filtered As List(Of String) = Nothing
        While ret = "" Or ret.Length < minLength Or ret.Length > maxLength
            ret = _allWords(r.Next(_allWords.Count))
        End While
        Return ret
    End Function
End Class
