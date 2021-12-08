Module Day08
    Sub Day08_main()
        Dim testinput = Day08_ReadInput("Day08\Day08_test01.txt")
        Dim input = Day08_ReadInput("Day08\Day08_input.txt")

        'part1
        Debug.Assert(Day08_Part1(testinput) = 26)
        Console.WriteLine("Day08 Part1: " & Day08_Part1(input))

        Debug.Assert(input.LongCount(Function(f) f.CountOf1478Signal <> 4) = 0) 'test the each signal line contains each trivial segment - only 1 and 4 is used for determining other segments
        'part2
        Debug.Assert(Day08_Part2(testinput) = 61229)
        Console.WriteLine("Day08 Part2: " & Day08_Part2(input))

    End Sub
    Function Day08_ReadInput(inputpath As String) As List(Of Day08_SegmentState)
        Dim result As New List(Of Day08_SegmentState)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            result.Add(New Day08_SegmentState(sr.ReadLine))
        End While
        sr.Close()
        Return result
    End Function

    Class Day08_SegmentState
        Public Signal As List(Of String)
        Public Output As List(Of String)
        Public Solution As Dictionary(Of String, Integer)
        Public Sub New(line As String)
            Signal = New List(Of String)
            Output = New List(Of String)

            Dim s1 = line.Split("|")
            For Each s2 In s1(0).Trim.Split(" ")
                Signal.Add(s2.Trim)
            Next
            Debug.Assert(Signal.Count = 10)

            For Each s2 In s1(1).Trim.Split(" ")
                Output.Add(s2.Trim)
            Next
            Debug.Assert(Output.Count = 4)
        End Sub

        Public ReadOnly Property CountOf1478 As Integer
            Get
                Return Output.LongCount(Function(f) f.Length = 2 OrElse f.Length = 3 OrElse f.Length = 4 OrElse f.Length = 7)
            End Get
        End Property
        Public ReadOnly Property CountOf1478Signal As Integer
            Get
                Return Signal.LongCount(Function(f) f.Length = 2 OrElse f.Length = 3 OrElse f.Length = 4 OrElse f.Length = 7)
            End Get
        End Property

        Public ReadOnly Property Value As Integer
            Get
                Solution = New Dictionary(Of String, Integer)

                Dim one = Signal.Find(Function(f) f.Length = 2)
                If one IsNot Nothing Then
                    Solution.Add(String.Concat(one.OrderBy(Function(f) f)), 1)
                End If

                Dim seven = Signal.Find(Function(f) f.Length = 3)
                If seven IsNot Nothing Then
                    Solution.Add(String.Concat(seven.OrderBy(Function(f) f)), 7)
                End If

                Dim four = Signal.Find(Function(f) f.Length = 4)
                If four IsNot Nothing Then
                    Solution.Add(String.Concat(four.OrderBy(Function(f) f)), 4)
                End If

                Dim eight = Signal.Find(Function(f) f.Length = 7)
                If eight IsNot Nothing Then
                    Solution.Add(String.Concat(eight.OrderBy(Function(f) f)), 8)
                End If


                Dim Num235 = Signal.FindAll(Function(f) f.Length = 5)

                Debug.Assert(one IsNot Nothing)
                Dim three = Num235.Find(Function(f) f.Contains(one(0)) AndAlso f.Contains(one(1))) 'only 3 overlaps fully with 1 from {2,3,5}
                If three IsNot Nothing Then
                    Solution.Add(String.Concat(three.OrderBy(Function(f) f)), 3)
                End If

                Debug.Assert(four IsNot Nothing)
                Dim five = Num235.Find(Function(f) ((If(f.Contains(four(0)), 1, 0) + If(f.Contains(four(1)), 1, 0) + If(f.Contains(four(2)), 1, 0) + If(f.Contains(four(3)), 1, 0)) = 3) AndAlso ((If(f.Contains(one(0)), 1, 0) + If(f.Contains(one(1)), 1, 0)) = 1)) 'only 5 overlaps with 4 on 3 digits from {2,3,5}
                If five IsNot Nothing Then
                    Solution.Add(String.Concat(five.OrderBy(Function(f) f)), 5)
                End If

                Dim two = Num235.Find(Function(f) (If(f.Contains(four(0)), 1, 0) + If(f.Contains(four(1)), 1, 0) + If(f.Contains(four(2)), 1, 0) + If(f.Contains(four(3)), 1, 0)) = 2) 'only 2 overlaps with 4 on 1 digits from {2,3,5}
                If two IsNot Nothing Then
                    Solution.Add(String.Concat(two.OrderBy(Function(f) f)), 2)
                End If

                Dim Num069 = Signal.FindAll(Function(f) f.Length = 6)
                Dim six = Num069.Find(Function(f) (If(f.Contains(one(0)), 1, 0) + If(f.Contains(one(1)), 1, 0)) = 1) 'only 6 overlaps with 1 on 1 digit from {0, 6, 9}
                If six IsNot Nothing Then
                    Solution.Add(String.Concat(six.OrderBy(Function(f) f)), 6)
                End If

                Dim nine = Num069.Find(Function(f) (If(f.Contains(four(0)), 1, 0) + If(f.Contains(four(1)), 1, 0) + If(f.Contains(four(2)), 1, 0) + If(f.Contains(four(3)), 1, 0)) = 4) 'only 9 overlaps fully with 4 from {0, 6, 9}
                If nine IsNot Nothing Then
                    Solution.Add(String.Concat(nine.OrderBy(Function(f) f)), 9)
                End If

                Dim zero = Num069.Find(Function(f) ((If(f.Contains(four(0)), 1, 0) + If(f.Contains(four(1)), 1, 0) + If(f.Contains(four(2)), 1, 0) + If(f.Contains(four(3)), 1, 0)) = 3) AndAlso ((If(f.Contains(one(0)), 1, 0) + If(f.Contains(one(1)), 1, 0)) = 2)) '0 overlaps with 1 in 2 places, and with 4 on 3 places from {0,6,9}
                If zero IsNot Nothing Then
                    Solution.Add(String.Concat(zero.OrderBy(Function(f) f)), 0)
                End If

                Dim result As Integer = 0
                For Each digit In Output
                    result *= 10
                    result += Solution(String.Concat(digit.OrderBy(Function(f) f)))
                Next

                Return result
            End Get
        End Property
    End Class

    Function Day08_Part1(input As List(Of Day08_SegmentState)) As Integer
        Return input.Sum(Function(f) f.CountOf1478)
    End Function

    Function Day08_Part2(input As List(Of Day08_SegmentState)) As Int64
        Return input.Sum(Function(f) f.Value)
    End Function
End Module
