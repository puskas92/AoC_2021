Module Day14
    Sub Day14_main()
        Dim testinput = Day14_ReadInput("Day14\Day14_test01.txt")
        Dim input = Day14_ReadInput("Day14\Day14_input.txt")

        'part1
        Debug.Assert(Day14_Part1(testinput) = 0)
        Console.WriteLine("Day14 Part1: " & Day14_Part1(input))

        'part2
        Debug.Assert(Day14_Part2(testinput) = 0)
        Console.WriteLine("Day14 Part2: " & Day14_Part2(input))

    End Sub
    Function Day14_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day14_Part1(input) As Integer
        Return 0
    End Function

    Function Day14_Part2(input) As Integer
        Return 0
    End Function
End Module
