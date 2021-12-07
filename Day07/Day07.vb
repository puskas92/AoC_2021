Module Day07
    Sub Day07_main()
        Dim testinput = Day07_ReadInput("Day07\Day07_test01.txt")
        Dim input = Day07_ReadInput("Day07\Day07_input.txt")

        'part1
        Debug.Assert(Day07_Part1(testinput) = 37)
        Console.WriteLine("Day07 Part1: " & Day07_Part1(input))

        'part2
        Debug.Assert(Day07_Part2(testinput) = 168)
        Console.WriteLine("Day07 Part2: " & Day07_Part2(input))

    End Sub
    Function Day07_ReadInput(inputpath As String) As List(Of Integer)
        Dim sr As New IO.StreamReader(inputpath)
        Dim line = sr.ReadLine
        Dim output = line.Split(",").ToList.ConvertAll(Function(f) Convert.ToInt32(f))
        sr.Close()
        Return output
    End Function


    Function Day07_Part1(input As List(Of Integer)) As Integer
        Dim min As Integer = Int32.MaxValue
        Dim minpos As Integer = -1
        Dim i As Integer
        For i = input.Min To input.Max
            Dim fuel = input.Sum(Function(f) Math.Abs(f - i))
            If fuel < min Then
                min = fuel
                minpos = i
            End If
        Next

        Return min

    End Function

    Function Day07_Part2(input As List(Of Integer)) As Integer
        Dim min As Integer = Int32.MaxValue
        Dim minpos As Integer = -1
        Dim i As Integer
        For i = input.Min To input.Max
            Dim fuel = input.Sum(Function(f)
                                     Dim steps = Math.Abs(f - i)
                                     Return (1 + steps) * steps / 2
                                 End Function)
            If fuel < min Then
                min = fuel
                minpos = i
            End If
        Next

        Return min
    End Function
End Module
