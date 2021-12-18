Module Day01
    Sub Day01_main()
        Dim input = Day01_ReadInput("Day01\Day01_input.txt")
        'part1
        Debug.Assert(Day01_Part1(Day01_ReadInput("Day01\Day01_test01.txt")) = 7)
        Console.WriteLine("Day01 Part1: " & Day01_Part1(input))

        'part2
        Debug.Assert(Day01_Part2(Day01_ReadInput("Day01\Day01_test01.txt")) = 5)
        Console.WriteLine("Day01 Part2: " & Day01_Part2(input))

    End Sub
    Function Day01_ReadInput(inputpath As String) As List(Of Integer)
        Dim result As New List(Of Integer)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            result.Add(Convert.ToInt32(sr.ReadLine))
        End While
        sr.Close()
        Return result
    End Function


    Function Day01_Part1(input As List(Of Integer)) As Integer
        Dim increase As Integer = 0
        For i = 1 To input.Count - 1
            If input(i) > input(i - 1) Then increase += 1
        Next

        Return increase
    End Function

    Function Day01_Part2(input As List(Of Integer)) As Integer
        Dim newinput As New List(Of Integer)

        For i = 2 To input.Count - 1
            newinput.Add(input(i) + input(i - 1) + input(i - 2))
        Next

        Return Day01_Part1(newinput)
    End Function
End Module
