﻿Imports System.IO

Public Class SolverDictionary
    Private _words(24) As List(Of String)
    Private _alphabet() As Char = "ΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩ"
    Private _fromReplace() As Char = "άέήίϊΐόύϋΰώΆΈΉΊΌΎΏς"
    Private _toReplace() As Char = "αεηιιιουυυωαεηιουωσ"    

    Public Sub New()
        For i As Integer = 0 To 23 : _words(i) = New List(Of String) : Next
        Dim cnt As Integer = 0
        Dim path As String = IO.Path.Combine(Application.StartupPath, "dict.txt")
        If Not IO.File.Exists(path) Then Exit Sub
        Dim entries As String() = File.ReadAllLines(path)
        'Dim outFile As New IO.StreamWriter(IO.Path.Combine(Application.StartupPath, "dict.txt"), FileMode.CreateNew)

        Dim n As Integer = 0
        For Each s As String In entries
            If s.Length >= 3 And s.Length <= 10 Then
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
        For i As Integer = 0 To 23
            System.Diagnostics.Debug.WriteLine(_alphabet(i) & ": " & _words(i).Count & " words")
        Next
    End Sub

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
        For i As Integer = 0 To 23
            ret += _words(i).Count
        Next
        Return ret
    End Function

End Class
