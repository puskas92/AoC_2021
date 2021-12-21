Module Day22
    Sub Day22_main()
        Dim input = Day22_ReadInput("Day22\Day22_input.txt")

        'part1
        Debug.Assert(Day22_Part1(Day22_ReadInput("Day22\Day22_test01.txt")) = 0)
        Console.WriteLine("Day22 Part1: " & Day22_Part1(input))

        'part2
        Debug.Assert(Day22_Part2(Day22_ReadInput("Day22\Day22_test01.txt")) = 0)
        Console.WriteLine("Day22 Part2: " & Day22_Part2(input))

    End Sub
    Function Day22_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day22_Part1(input) As Integer
        Return 0
    End Function

    Function Day22_Part2(input) As Integer
        Return 0
    End Function
End Module
