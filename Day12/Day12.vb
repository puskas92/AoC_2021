Module Day12
    Sub Day12_main()
        Dim testinput = Day12_ReadInput("Day12\Day12_test01.txt")
        Dim input = Day12_ReadInput("Day12\Day12_input.txt")

        'part1
        Debug.Assert(Day12_Part1(testinput) = 0)
        Console.WriteLine("Day12 Part1: " & Day12_Part1(input))

        'part2
        Debug.Assert(Day12_Part2(testinput) = 0)
        Console.WriteLine("Day12 Part2: " & Day12_Part2(input))

    End Sub
    Function Day12_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day12_Part1(input) As Integer
        Return 0
    End Function

    Function Day12_Part2(input) As Integer
        Return 0
    End Function
End Module
