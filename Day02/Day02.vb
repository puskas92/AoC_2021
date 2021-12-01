Module Day02
    Sub Day02_main()
        'part1
        Debug.Assert(Day02_Part1("Day02\Day02_test01.txt") = 0)
        Console.WriteLine("Day02 Part1: " & Day02_Part1("Day02\Day02_input.txt"))

        'part2
        Debug.Assert(Day02_Part2("Day02\Day02_test01.txt") = 0)
        Console.WriteLine("Day02 Part2: " & Day02_Part2("Day02\Day02_input.txt"))

    End Sub
    Function Day02_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While

        Return Nothing
    End Function


    Function Day02_Part1(inputpath As String) As Integer
        Dim input = Day02_ReadInput(inputpath)

        Return 0
    End Function

    Function Day02_Part2(inputpath As String) As Integer
        Dim input = Day02_ReadInput(inputpath)

        Return 0
    End Function
End Module
