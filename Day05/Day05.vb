Module Day05
    Sub Day05_main()
        Dim testinput = Day05_ReadInput("Day05\Day05_test01.txt")
        Dim input = Day05_ReadInput("Day05\Day05_input.txt")

        'part1
        Debug.Assert(Day05_Part1(testinput) = 0)
        Console.WriteLine("Day05 Part1: " & Day05_Part1(input))

        'part2
        Debug.Assert(Day05_Part2(testinput) = 0)
        Console.WriteLine("Day05 Part2: " & Day05_Part2(input))

    End Sub
    Function Day05_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While

        Return Nothing
    End Function


    Function Day05_Part1(input) As Integer
        Return 0
    End Function

    Function Day05_Part2(input) As Integer
        Return 0
    End Function
End Module
