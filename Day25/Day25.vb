Module Day25
    Sub Day25_main()
        Dim input = Day25_ReadInput("Day25\Day25_input.txt")

        'part1
        Debug.Assert(Day25_Part1(Day25_ReadInput("Day25\Day25_test01.txt")) = 58)
        Console.WriteLine("Day25 Part1: " & Day25_Part1(input))
    End Sub
    Function Day25_ReadInput(inputpath As String) As List(Of List(Of Char))
        Dim result As New List(Of List(Of Char))
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            result.Add(sr.ReadLine.ToList)
        End While
        sr.Close()
        Return result
    End Function


    Function Day25_Part1(input As List(Of List(Of Char))) As Integer
        Dim changed As Boolean = False
        Dim iteration As Integer = 0
        Do
            iteration += 1
            changed = False

            Dim EastStep As New List(Of List(Of Char))
            For i = 0 To input.Count - 1
                EastStep.Add(New List(Of Char)(input(i)))
            Next i

            For i = 0 To input.Count - 1
                For j = 0 To input(i).Count - 1
                    If input(i)(j) = ">"c Then
                        If input(i)((j + 1) Mod input(i).Count) = "."c Then
                            EastStep(i)(j) = "."c
                            EastStep(i)((j + 1) Mod input(i).Count) = ">"c
                            changed = True
                        End If
                    End If
                Next
            Next
            Dim SouthStep As New List(Of List(Of Char))
            For i = 0 To EastStep.Count - 1
                SouthStep.Add(New List(Of Char)(EastStep(i)))
            Next i
            For i = 0 To EastStep.Count - 1
                For j = 0 To EastStep(i).Count - 1
                    If EastStep(i)(j) = "v"c Then
                        If EastStep((i + 1) Mod EastStep.Count)(j) = "."c Then
                            SouthStep(i)(j) = "."c
                            SouthStep((i + 1) Mod EastStep.Count)(j) = "v"c
                            changed = True
                        End If
                    End If
                Next
            Next
            'VisualizeState(input, iteration)
            input = New List(Of List(Of Char))
            For i = 0 To SouthStep.Count - 1
                input.Add(New List(Of Char)(SouthStep(i)))
            Next i
        Loop While changed
        Return iteration
    End Function


    Public Sub VisualizeState(input As List(Of List(Of Char)), iteration As Integer)
        Console.WriteLine("Iteration: " & iteration.ToString)

        For i = 0 To input.Count - 1
            Dim s As String = ""
            For j = 0 To input(i).Count - 1
                s &= input(i)(j)
            Next
            Console.WriteLine(s)
        Next
    End Sub
End Module
