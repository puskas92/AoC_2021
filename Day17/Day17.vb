Module Day17
    Sub Day17_main()
        Dim testinput = Day17_ReadInput("Day17\Day17_test01.txt")
        Dim input = Day17_ReadInput("Day17\Day17_input.txt")

        'part1
        Debug.Assert(Day17_Part1(testinput) = 0)
        Console.WriteLine("Day17 Part1: " & Day17_Part1(input))

        'part2
        Debug.Assert(Day17_Part2(testinput) = 0)
        Console.WriteLine("Day17 Part2: " & Day17_Part2(input))

    End Sub
    Function Day17_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day17_Part1(input) As Integer
        Return 0
    End Function

    Function Day17_Part2(input) As Integer
        Return 0
    End Function
End Module
