Module Day03

    Sub Day03_main()

        Dim testinput = Day03_ReadInput("Day03\Day03_test01.txt")
        Dim input = Day03_ReadInput("Day03\Day03_input.txt")

        'part1
        Debug.Assert(Day03_Part1(testinput) = 0)
        Console.WriteLine("Day03 Part1: " & Day03_Part1(input))

        'part2
        Debug.Assert(Day03_Part2(testinput) = 0)
        Console.WriteLine("Day03 Part2: " & Day03_Part2(input))

    End Sub
    Function Day03_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While

        Return Nothing
    End Function


    Function Day03_Part1(input) As Integer
        Return 0
    End Function

    Function Day03_Part2(input) As Integer
        Return 0
    End Function
End Module
