Module Day11
    Sub Day11_main()
        Dim testinput = Day11_ReadInput("Day11\Day11_test01.txt")
        Dim input = Day11_ReadInput("Day11\Day11_input.txt")

        'part1
        Debug.Assert(Day11_Part1(testinput) = 0)
        Console.WriteLine("Day11 Part1: " & Day11_Part1(input))

        'part2
        Debug.Assert(Day11_Part2(testinput) = 0)
        Console.WriteLine("Day11 Part2: " & Day11_Part2(input))

    End Sub
    Function Day11_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day11_Part1(input) As Integer
        Return 0
    End Function

    Function Day11_Part2(input) As Integer
        Return 0
    End Function
End Module
