Module Day19
    Sub Day19_main()
        Dim input = Day19_ReadInput("Day19\Day19_input.txt")

        'part1
        Debug.Assert(Day19_Part1(Day19_ReadInput("Day19\Day19_test01.txt")) = 0)
        Console.WriteLine("Day19 Part1: " & Day19_Part1(input))

        'part2
        Debug.Assert(Day19_Part2(Day19_ReadInput("Day19\Day19_test01.txt")) = 0)
        Console.WriteLine("Day19 Part2: " & Day19_Part2(input))

    End Sub
    Function Day19_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day19_Part1(input) As Integer
        Return 0
    End Function

    Function Day19_Part2(input) As Integer
        Return 0
    End Function
End Module
