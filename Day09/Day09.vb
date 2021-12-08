Module Day09
    Sub Day09_main()
        Dim testinput = Day09_ReadInput("Day09\Day09_test01.txt")
        Dim input = Day09_ReadInput("Day09\Day09_input.txt")

        'part1
        Debug.Assert(Day09_Part1(testinput) = 0)
        Console.WriteLine("Day09 Part1: " & Day09_Part1(input))

        'part2
        Debug.Assert(Day09_Part2(testinput) = 0)
        Console.WriteLine("Day09 Part2: " & Day09_Part2(input))

    End Sub
    Function Day09_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day09_Part1(input) As Integer
        Return 0
    End Function

    Function Day09_Part2(input) As Integer
        Return 0
    End Function
End Module
