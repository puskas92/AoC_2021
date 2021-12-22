Module Day23
    Sub Day23_main()
        Dim input = Day23_ReadInput("Day23\Day23_input.txt")

        'part1
        Debug.Assert(Day23_Part1(Day23_ReadInput("Day23\Day23_test01.txt")) = 0)
        Console.WriteLine("Day23 Part1: " & Day23_Part1(input))

        'part2
        Debug.Assert(Day23_Part2(Day23_ReadInput("Day23\Day23_test01.txt")) = 0)
        Console.WriteLine("Day23 Part2: " & Day23_Part2(input))

    End Sub
    Function Day23_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day23_Part1(input) As Integer
        Return 0
    End Function

    Function Day23_Part2(input) As Integer
        Return 0
    End Function
End Module
