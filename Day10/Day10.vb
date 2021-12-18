Module Day10
    Sub Day10_main()
        Dim input = Day10_ReadInput("Day10\Day10_input.txt")

        'part1
        Debug.Assert(New Day10_Line("{([(<{}[<>[]}>{[]{[(<()>").IllegalScore = 1197)
        Debug.Assert(New Day10_Line("[[<[([]))<([[{}[[()]]]").IllegalScore = 3)
        Debug.Assert(New Day10_Line("[{[{({}]{}}([{[{{{}}([]").IllegalScore = 57)
        Debug.Assert(New Day10_Line("[<(<(<(<{}))><([]([]()").IllegalScore = 3)
        Debug.Assert(New Day10_Line("<{([([[(<>()){}]>(<<{{").IllegalScore = 25137)
        Debug.Assert(New Day10_Line("[<>({}){}[([])<>]]").IllegalScore = 0)
        Debug.Assert(Day10_Part1(Day10_ReadInput("Day10\Day10_test01.txt")) = 26397)
        Console.WriteLine("Day10 Part1: " & Day10_Part1(input))

        'part2
        Debug.Assert(New Day10_Line("[({(<(())[]>[[{[]{<()<>>").AutoCompleteScore = 288957)
        Debug.Assert(New Day10_Line("[(()[<>])]({[<{<<[]>>(").AutoCompleteScore = 5566)
        Debug.Assert(New Day10_Line("(((({<>}<{<{<>}{[]{[]{}").AutoCompleteScore = 1480781)
        Debug.Assert(New Day10_Line("{<[[]]>}<{[{[{[]{()[[[]").AutoCompleteScore = 995444)
        Debug.Assert(New Day10_Line("<{([{{}}[<[[[<>{}]]]>[]]").AutoCompleteScore = 294)
        Debug.Assert(New Day10_Line("<{([([[(<>()){}]>(<<{{").AutoCompleteScore = 0)
        Debug.Assert(Day10_Part2(Day10_ReadInput("Day10\Day10_test01.txt")) = 288957)
        Console.WriteLine("Day10 Part2: " & Day10_Part2(input))
        '2896974097
    End Sub
    Function Day10_ReadInput(inputpath As String) As List(Of Day10_Line)
        Dim output As New List(Of Day10_Line)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            output.Add(New Day10_Line(sr.ReadLine))
        End While
        sr.Close()
        Return output
    End Function
    Class Day10_Line
        Public line As String
        Public Sub New(s As String)
            line = s
        End Sub

        Public ReadOnly Property IllegalScore As Integer
            Get
                '): 3 points. - a 
                ']: 57 points. - b
                '}: 1197 points. - c 
                '>: 25137 points. - d
                Dim BracketStack As New Stack(Of Char)
                For Each c In line
                    Select Case c
                        Case "("c, "["c, "{"c, "<"c
                            BracketStack.Push(c)
                        Case ")"c
                            If BracketStack.Pop <> "("c Then
                                Return 3
                                Exit Property
                            End If
                        Case "]"c
                            If BracketStack.Pop <> "["c Then
                                Return 57
                                Exit Property
                            End If
                        Case "}"c
                            If BracketStack.Pop <> "{"c Then
                                Return 1197
                                Exit Property
                            End If
                        Case ">"c
                            If BracketStack.Pop <> "<"c Then
                                Return 25137
                                Exit Property
                            End If
                    End Select
                Next

                Return 0
            End Get
        End Property

        Public ReadOnly Property AutoCompleteScore As Int64
            Get
                If IllegalScore > 0 Then
                    Return 0
                    Exit Property
                End If

                Dim BracketStack As New Stack(Of Char)
                Dim c As Char
                For Each c In line
                    Select Case c
                        Case "("c, "["c, "{"c, "<"c
                            BracketStack.Push(c)
                        Case ")"c, "]"c, "}"c, ">"c
                            BracketStack.Pop()
                            'should be the valid pair since it is already checked if it is valid
                    End Select
                Next

                '): 1 point.
                ']: 2 points.
                '}: 3 points.
                '>: 4 points.

                Dim score As Int64 = 0
                While BracketStack.Count > 0
                    score *= 5
                    Select Case BracketStack.Pop
                        Case "("c
                            score += 1
                        Case "["c
                            score += 2
                        Case "{"c
                            score += 3
                        Case "<"c
                            score += 4
                    End Select
                End While
                Return score
            End Get
        End Property
    End Class

    Function Day10_Part1(input As List(Of Day10_Line)) As Integer
        Return input.Sum(Function(f) f.IllegalScore)
    End Function

    Function Day10_Part2(input As List(Of Day10_Line)) As Int64
        Dim autocomp = input.FindAll(Function(f) f.IllegalScore = 0)
        autocomp.Sort(Function(f, g) f.AutoCompleteScore.CompareTo(g.AutoCompleteScore))
        'For i = 0 To autocomp.Count - 1
        '    Console.WriteLine(i & ": " & autocomp(i).IllegalScore & " " & autocomp(i).AutoCompleteScore)
        'Next
        Return autocomp(Math.Floor(autocomp.Count / 2)).AutoCompleteScore
    End Function
End Module
