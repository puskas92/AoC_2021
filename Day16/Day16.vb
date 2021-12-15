Module Day16
    Sub Day16_main()
        Dim testinput = Day16_ReadInput("Day16\Day16_test01.txt")
        Dim input = Day16_ReadInput("Day16\Day16_input.txt")

        'part1
        Debug.Assert(Day16_Part1(testinput) = 0)
        Console.WriteLine("Day16 Part1: " & Day16_Part1(input))

        'part2
        Debug.Assert(Day16_Part2(testinput) = 0)
        Console.WriteLine("Day16 Part2: " & Day16_Part2(input))

    End Sub
    Function Day16_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day16_Part1(input) As Integer
        Return 0
    End Function

    Function Day16_Part2(input) As Integer
        Return 0
    End Function
End Module
