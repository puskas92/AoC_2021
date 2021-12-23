Module Day24
    Sub Day24_main()
        Dim input = Day24_ReadInput("Day24\Day24_input.txt")

        'part1
        Debug.Assert(Day24_Part1(Day24_ReadInput("Day24\Day24_test01.txt")) = 0)
        Console.WriteLine("Day24 Part1: " & Day24_Part1(input))

        'part2
        Debug.Assert(Day24_Part2(Day24_ReadInput("Day24\Day24_test01.txt")) = 0)
        Console.WriteLine("Day24 Part2: " & Day24_Part2(input))

    End Sub
    Function Day24_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day24_Part1(input) As Integer
        Return 0
    End Function

    Function Day24_Part2(input) As Integer
        Return 0
    End Function
End Module
