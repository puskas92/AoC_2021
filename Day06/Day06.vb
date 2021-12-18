Module Day06
    Sub Day06_main()
        Dim input = Day06_ReadInput("Day06\Day06_input.txt")

        'part1
        Debug.Assert(Day06_Part12(18, Day06_ReadInput("Day06\Day06_test01.txt")) = 26)
        Debug.Assert(Day06_Part12(80, Day06_ReadInput("Day06\Day06_test01.txt")) = 5934)
        Console.WriteLine("Day06 Part1: " & Day06_Part12(80, input))

        'part2
        Debug.Assert(Day06_Part12(256, Day06_ReadInput("Day06\Day06_test01.txt")) = 26984457539)
        Console.WriteLine("Day06 Part2: " & Day06_Part12(256, input))

    End Sub
    Function Day06_ReadInput(inputpath As String) As Dictionary(Of Integer, Int64) 'day, count
        Dim sr As New IO.StreamReader(inputpath)
        Dim line = sr.ReadLine
        sr.Close()

        Dim input = line.Split(",").ToList.ConvertAll(Function(f) Convert.ToInt32(f))

        Dim output As New Dictionary(Of Integer, Int64)
        Dim i As Integer
        For i = 0 To 8
            output.Add(i, input.LongCount(Function(f) f = i))
        Next

        Return output
    End Function


    Function Day06_Part12(days As Integer, input As Dictionary(Of Integer, Int64)) As Int64
        Dim currentDay As New Dictionary(Of Integer, Int64)(input)
        Dim nextDay As New Dictionary(Of Integer, Int64)
        For i = 1 To days
            For j = 0 To 7
                nextDay.Add(j, currentDay(j + 1))
            Next
            nextDay.Add(8, currentDay(0))
            nextDay(6) += currentDay(0)
            currentDay = New Dictionary(Of Integer, Int64)(nextDay)
            nextDay = New Dictionary(Of Integer, Int64)
        Next

        Return currentDay.Sum(Function(f) f.Value)
    End Function
End Module
