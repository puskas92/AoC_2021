Module Day09
    Sub Day09_main()
        Dim input = Day09_ReadInput("Day09\Day09_input.txt")

        'part1-2
        Debug.Assert(Day09_Part12(Day09_ReadInput("Day09\Day09_test01.txt")).Equals(New Tuple(Of Integer, Integer)(15, 1134)))
        Dim result = Day09_Part12(input)
        Console.WriteLine("Day09 Part1: " & result.Item1)
        Console.WriteLine("Day09 Part2: " & result.Item2)

    End Sub
    Function Day09_ReadInput(inputpath As String) As List(Of List(Of Integer))
        Dim output As New List(Of List(Of Integer))
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            Dim line = sr.ReadLine
            output.Add(line.ToList.ConvertAll(Function(f) Convert.ToInt32(f.ToString)))
        End While
        sr.Close()
        Return output
    End Function

    Function Day09_Part12(input As List(Of List(Of Integer))) As Tuple(Of Integer, Integer)
        'part1
        Dim minlist As New List(Of Tuple(Of Integer, Integer))
        For i = 0 To input.Count - 1
            For j = 0 To input(i).Count - 1

                If (i = 0 OrElse input(i)(j) < input(i - 1)(j)) AndAlso (i = input.Count - 1 OrElse input(i)(j) < input(i + 1)(j)) AndAlso (j = 0 OrElse input(i)(j) < input(i)(j - 1)) AndAlso (j = input(i).Count - 1 OrElse input(i)(j) < input(i)(j + 1)) Then minlist.Add(New Tuple(Of Integer, Integer)(i, j))
            Next
        Next
        Dim part1result As Integer = 0
        For Each minpos In minlist
            part1result += 1 + input(minpos.Item1)(minpos.Item2)
        Next

        'part2
        Dim basinsmap As New List(Of List(Of Integer))
        For Each line In input
            basinsmap.Add(New List(Of Integer))
            For Each point In line
                basinsmap.Last.Add(-1)
            Next
        Next

        Dim directions As New List(Of Tuple(Of Integer, Integer))
        directions.Add(New Tuple(Of Integer, Integer)(-1, 0))
        directions.Add(New Tuple(Of Integer, Integer)(1, 0))
        directions.Add(New Tuple(Of Integer, Integer)(0, -1))
        directions.Add(New Tuple(Of Integer, Integer)(0, 1))

        Dim maxX As Integer = input.Count - 1
        Dim maxY As Integer = input(0).Count - 1

        Dim basins As New List(Of Integer)
        For i = 0 To minlist.Count - 1
            Dim minpos = minlist(i)
            Dim ToCheckList As New Queue(Of Tuple(Of Integer, Integer))
            ToCheckList.Enqueue(minpos)

            Dim size As Integer = 0
            While ToCheckList.TryPeek(Nothing)
                Dim pos = ToCheckList.Dequeue
                If input(pos.Item1)(pos.Item2) <> 9 AndAlso basinsmap(pos.Item1)(pos.Item2) = -1 Then size += 1
                basinsmap(pos.Item1)(pos.Item2) = i


                For Each dir1 In directions
                    Dim x = pos.Item1 + dir1.Item1
                    Dim y = pos.Item2 + dir1.Item2
                    '   position is not out of band                                         not checked yet                  bigger than current position                        not 9
                    If (x >= 0 AndAlso x <= maxX AndAlso y >= 0 AndAlso y <= maxY AndAlso basinsmap(x)(y) = -1 AndAlso input(x)(y) > input(pos.Item1)(pos.Item2) AndAlso input(x)(y) <> 9) Then ToCheckList.Enqueue(New Tuple(Of Integer, Integer)(x, y))
                Next
            End While

            basins.Add(size)
        Next

        basins.Sort()

        Dim part2result = basins.Last * basins(basins.Count - 2) * basins(basins.Count - 3)
        Return New Tuple(Of Integer, Integer)(part1result, part2result)
    End Function


End Module
