Module DayXX
    Sub DayXX_main()
        Dim testinput = DayXX_ReadInput("DayXX\DayXX_test01.txt")
        Dim input = DayXX_ReadInput("DayXX\DayXX_input.txt")

        'part1
        Debug.Assert(DayXX_Part1(testinput) = 0)
        Console.WriteLine("DayXX Part1: " & DayXX_Part1(input))

        'part2
        Debug.Assert(DayXX_Part2(testinput) = 0)
        Console.WriteLine("DayXX Part2: " & DayXX_Part2(input))

    End Sub
    Function DayXX_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function DayXX_Part1(input) As Integer
        Return 0
    End Function

    Function DayXX_Part2(input) As Integer
        Return 0
    End Function
End Module
