Module Day08
    Sub Day08_main()
        Dim testinput = Day08_ReadInput("Day08\Day08_test01.txt")
        Dim input = Day08_ReadInput("Day08\Day08_input.txt")

        'part1
        Debug.Assert(Day08_Part1(testinput) = 0)
        Console.WriteLine("Day08 Part1: " & Day08_Part1(input))

        'part2
        Debug.Assert(Day08_Part2(testinput) = 0)
        Console.WriteLine("Day08 Part2: " & Day08_Part2(input))

    End Sub
    Function Day08_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day08_Part1(input) As Integer
        Return 0
    End Function

    Function Day08_Part2(input) As Integer
        Return 0
    End Function
End Module
