Module Day13
    Sub Day13_main()
        Dim testinput = Day13_ReadInput("Day13\Day13_test01.txt")
        Dim input = Day13_ReadInput("Day13\Day13_input.txt")

        'part1
        Debug.Assert(Day13_Part1(testinput) = 0)
        Console.WriteLine("Day13 Part1: " & Day13_Part1(input))

        'part2
        Debug.Assert(Day13_Part2(testinput) = 0)
        Console.WriteLine("Day13 Part2: " & Day13_Part2(input))

    End Sub
    Function Day13_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day13_Part1(input) As Integer
        Return 0
    End Function

    Function Day13_Part2(input) As Integer
        Return 0
    End Function
End Module
