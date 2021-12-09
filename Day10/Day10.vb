Module Day10
    Sub Day10_main()
        Dim testinput = Day10_ReadInput("Day10\Day10_test01.txt")
        Dim input = Day10_ReadInput("Day10\Day10_input.txt")

        'part1
        Debug.Assert(Day10_Part1(testinput) = 0)
        Console.WriteLine("Day10 Part1: " & Day10_Part1(input))

        'part2
        Debug.Assert(Day10_Part2(testinput) = 0)
        Console.WriteLine("Day10 Part2: " & Day10_Part2(input))

    End Sub
    Function Day10_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day10_Part1(input) As Integer
        Return 0
    End Function

    Function Day10_Part2(input) As Integer
        Return 0
    End Function
End Module
