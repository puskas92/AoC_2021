Module Day06
    Sub Day06_main()
        Dim testinput = Day06_ReadInput("Day06\Day06_test01.txt")
        Dim input = Day06_ReadInput("Day06\Day06_input.txt")

        'part1
        Debug.Assert(Day06_Part1(testinput) = 0)
        Console.WriteLine("Day06 Part1: " & Day06_Part1(input))

        'part2
        Debug.Assert(Day06_Part2(testinput) = 0)
        Console.WriteLine("Day06 Part2: " & Day06_Part2(input))

    End Sub
    Function Day06_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While

        Return Nothing
    End Function


    Function Day06_Part1(input) As Integer
        Return 0
    End Function

    Function Day06_Part2(input) As Integer
        Return 0
    End Function
End Module
