Module Day01
    Sub Day01_main()
        'part1
        Debug.Assert(Day01_Part1("Day01\Day01_test01.txt") = 0)
        Console.WriteLine("Day01 Part1: " & Day01_Part1("Day01\Day01_input.txt"))

        'part2
        Debug.Assert(Day01_Part2("Day01\Day01_test01.txt") = 0)
        Console.WriteLine("Day01 Part2: " & Day01_Part2("Day01\Day01_input.txt"))

    End Sub
    Function Day01_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While

        Return Nothing
    End Function


    Function Day01_Part1(inputpath As String) As Integer
        Dim input = Day01_ReadInput(inputpath)

        Return 0
    End Function

    Function Day01_Part2(inputpath As String) As Integer
        Dim input = Day01_ReadInput(inputpath)

        Return 0
    End Function
End Module
