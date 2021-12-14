Module Day15
    Sub Day15_main()
        Dim testinput = Day15_ReadInput("Day15\Day15_test01.txt")
        Dim input = Day15_ReadInput("Day15\Day15_input.txt")

        'part1
        Debug.Assert(Day15_Part1(testinput) = 0)
        Console.WriteLine("Day15 Part1: " & Day15_Part1(input))

        'part2
        Debug.Assert(Day15_Part2(testinput) = 0)
        Console.WriteLine("Day15 Part2: " & Day15_Part2(input))

    End Sub
    Function Day15_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day15_Part1(input) As Integer
        Return 0
    End Function

    Function Day15_Part2(input) As Integer
        Return 0
    End Function
End Module
