Module Module1

    Dim _language As String = "gr"
    Dim _characterSeed As String = ""
    Dim _verbose As Boolean = False

    Sub Main(args As String())
        Console.OutputEncoding = System.Text.Encoding.UTF8
        Dim activeArg As String = ""
        Dim shouldRun As Boolean = True
        Dim errorMessage As String = ""
        If args.Length > 0 Then
            ' Iterate through each argument
            For Each arg As String In args
                If activeArg <> "" Then
                    Select Case activeArg.ToLower
                        Case "-l"
                            _language = arg
                            activeArg = ""
                        Case "-s"
                            _characterSeed = arg
                            activeArg = ""


                    End Select
                Else
                    If arg.ToLower.StartsWith("-") Then
                        activeArg = arg
                        If activeArg.ToLower = "-v" Then _verbose = True
                    End If
                End If
            Next
        Else
            Console.WriteLine("No command-line arguments provided.")
            Console.WriteLine("Usage: SolverCLI -s <character seed> -l <gr|en> [-v]")
        End If

        If _language <> "gr" And _language <> "en" Then
            errorMessage = "No language defined - allowed values are en|gr"
            shouldRun = False
        ElseIf _characterSeed = "" Then
            errorMessage = "No character seed defined"
            shouldRun = False
        ElseIf _characterSeed.Length <> 16 Then
            errorMessage = "Character seed should be 16 letters long"
            shouldRun = False
        End If

        If shouldRun Then
            ConsoleOutput("Solving...", vbCrLf)
            Dim dic As New LexeisSolver.SolverDictionary(_language)
            Dim slv As New LexeisSolver.Solver(_characterSeed, dic)
            AddHandler slv.WordChecked, AddressOf WordChecked
            slv.MinimumLength = 3
            slv.MaximumLength = 8
            slv.Solve()
            Console.WriteLine()

            For Each s As String In slv.WordsFound
                Console.WriteLine(s)
            Next
        Else
            ConsoleOutput(errorMessage, vbCrLf)
        End If

        ConsoleOutput("Press any key to exit...", vbCrLf)
        If Not _verbose Then Console.ReadKey(True)
    End Sub

    Private Sub WordChecked(wrd As String)
        ConsoleOutput(wrd, vbCr)
    End Sub

    Private Sub ConsoleOutput(txt As String, Optional lineEnding As String = "")
        If _verbose Then Return
        Console.Write(txt & lineEnding)
    End Sub
End Module
