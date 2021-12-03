Module Day04
    Sub Day04_main()
        Dim testinput = Day04_ReadInput("Day04\Day04_test01.txt")
        Dim input = Day04_ReadInput("Day04\Day04_input.txt")

        'part1
        Debug.Assert(Day04_Part1(testinput) = 0)
        Console.WriteLine("Day04 Part1: " & Day04_Part1(input))

        'part2
        Debug.Assert(Day04_Part2(testinput) = 0)
        Console.WriteLine("Day04 Part2: " & Day04_Part2(input))

    End Sub
    Function Day04_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While

        Return Nothing
    End Function


    Function Day04_Part1(input) As Integer
        Return 0
    End Function

    Function Day04_Part2(input) As Integer
        Return 0
    End Function
End Module
