Module Day18
    Sub Day18_main()
        Dim testinput = Day18_ReadInput("Day18\Day18_test01.txt")
        Dim input = Day18_ReadInput("Day18\Day18_input.txt")

        'part1
        Debug.Assert(Day18_Part1(testinput) = 0)
        Console.WriteLine("Day18 Part1: " & Day18_Part1(input))

        'part2
        Debug.Assert(Day18_Part2(testinput) = 0)
        Console.WriteLine("Day18 Part2: " & Day18_Part2(input))

    End Sub
    Function Day18_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day18_Part1(input) As Integer
        Return 0
    End Function

    Function Day18_Part2(input) As Integer
        Return 0
    End Function
End Module
