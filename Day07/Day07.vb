Module Day07
    Sub Day07_main()
        Dim testinput = Day07_ReadInput("Day07\Day07_test01.txt")
        Dim input = Day07_ReadInput("Day07\Day07_input.txt")

        'part1
        Debug.Assert(Day07_Part1(testinput) = 0)
        Console.WriteLine("Day07 Part1: " & Day07_Part1(input))

        'part2
        Debug.Assert(Day07_Part2(testinput) = 0)
        Console.WriteLine("Day07 Part2: " & Day07_Part2(input))

    End Sub
    Function Day07_ReadInput(inputpath As String) As Object
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)

        End While
        sr.Close()
        Return Nothing
    End Function


    Function Day07_Part1(input) As Integer
        Return 0
    End Function

    Function Day07_Part2(input) As Integer
        Return 0
    End Function
End Module
