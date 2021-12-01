Module DayXX
    Sub DayXX_main()
        'part1
        Debug.Assert(DayXX_Part1("DayXX\DayXX_test01.txt") = 0)
        Console.WriteLine("DayXX Part1: " & DayXX_Part1("DayXX\DayXX_input.txt"))

        'part2
        Debug.Assert(DayXX_Part2("DayXX\DayXX_test01.txt") = 0)
        Console.WriteLine("DayXX Part2: " & DayXX_Part2("DayXX\DayXX_input.txt"))

    End Sub
    Function DayXX_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While

        Return Nothing
    End Function


    Function DayXX_Part1(inputpath As String) As Integer
        Dim input = DayXX_ReadInput(inputpath)

        Return 0
    End Function

    Function DayXX_Part2(inputpath As String) As Integer
        Dim input = DayXX_ReadInput(inputpath)

        Return 0
    End Function
End Module
