Module Day25
    Sub Day25_main()
        Dim input = Day25_ReadInput("Day25\Day25_input.txt")

        'part1
        Debug.Assert(Day25_Part1(Day25_ReadInput("Day25\Day25_test01.txt")) = 0)
        Console.WriteLine("Day25 Part1: " & Day25_Part1(input))

        'part2
        Debug.Assert(Day25_Part2(Day25_ReadInput("Day25\Day25_test01.txt")) = 0)
        Console.WriteLine("Day25 Part2: " & Day25_Part2(input))

    End Sub
    Function Day25_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day25_Part1(input) As Integer
        Return 0
    End Function

    Function Day25_Part2(input) As Integer
        Return 0
    End Function
End Module
