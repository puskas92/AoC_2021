Module Day21
    Sub Day21_main()
        Dim input = Day21_ReadInput("Day21\Day21_input.txt")

        'part1
        Debug.Assert(Day21_Part1(Day21_ReadInput("Day21\Day21_test01.txt")) = 0)
        Console.WriteLine("Day21 Part1: " & Day21_Part1(input))

        'part2
        Debug.Assert(Day21_Part2(Day21_ReadInput("Day21\Day21_test01.txt")) = 0)
        Console.WriteLine("Day21 Part2: " & Day21_Part2(input))

    End Sub
    Function Day21_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day21_Part1(input) As Integer
        Return 0
    End Function

    Function Day21_Part2(input) As Integer
        Return 0
    End Function
End Module
