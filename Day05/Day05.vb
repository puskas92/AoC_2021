Module Day05
    Sub Day05_main()
        Dim input = Day05_ReadInput("Day05\Day05_input.txt")

        'part1
        Debug.Assert(Day05_Part1(Day05_ReadInput("Day05\Day05_test01.txt")) = 5)
        Console.WriteLine("Day05 Part1: " & Day05_Part1(input))

        'part2
        Debug.Assert(Day05_Part2(Day05_ReadInput("Day05\Day05_test01.txt")) = 12)
        Console.WriteLine("Day05 Part2: " & Day05_Part2(input))

    End Sub
    Class Day05_Segment
        Public StartPoint As Drawing.Point
        Public EndPoint As Drawing.Point

        Public Sub New(s As String)
            StartPoint.X = Convert.ToInt32(s.Split("-")(0).Split(",")(0).Trim)
            StartPoint.Y = Convert.ToInt32(s.Split("-")(0).Split(",")(1).Trim)
            EndPoint.X = Convert.ToInt32(s.Split(">")(1).Split(",")(0).Trim)
            EndPoint.Y = Convert.ToInt32(s.Split(">")(1).Split(",")(1).Trim)
        End Sub
        Public ReadOnly Property isHorizontalOrVertical As Boolean
            Get
                Return (StartPoint.X = EndPoint.X) OrElse (StartPoint.Y = EndPoint.Y)
            End Get
        End Property
    End Class
    Function Day05_ReadInput(inputpath As String) As List(Of Day05_Segment)
        Dim output As New List(Of Day05_Segment)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            output.Add(New Day05_Segment(sr.ReadLine))
        End While
        sr.Close()
        Return output
    End Function


    Function Day05_Part1(input As List(Of Day05_Segment)) As Integer
        Dim Map As New Dictionary(Of Integer, Dictionary(Of Integer, Integer))

        For Each seg In input
            If seg.isHorizontalOrVertical = False Then Continue For

            Dim x, y As Integer
            For x = seg.StartPoint.X To seg.EndPoint.X Step If(seg.StartPoint.X < seg.EndPoint.X, 1, -1) 'one of these fors will run once since it is horizontal or vertical
                For y = seg.StartPoint.Y To seg.EndPoint.Y Step If(seg.StartPoint.Y < seg.EndPoint.Y, 1, -1)
                    If Map.ContainsKey(x) = False Then Map.Add(x, New Dictionary(Of Integer, Integer))
                    If Map(x).ContainsKey(y) = False Then Map(x).Add(y, 0)
                    Map(x)(y) += 1
                Next
            Next
        Next
        'Day05_VisualizeMap(Map)
        Return Map.Sum(Function(f) f.Value.LongCount(Function(g) g.Value >= 2))
    End Function

    Function Day05_Part2(input As List(Of Day05_Segment)) As Integer
        'part1 could be reused, but it was faster to copy it, and to find out that there is a better universal method, that could also solve part1
        Dim Map As New Dictionary(Of Integer, Dictionary(Of Integer, Integer))

        For Each seg In input
            Dim length = 0
            If seg.isHorizontalOrVertical Then
                length = Math.Abs(seg.EndPoint.X - seg.StartPoint.X) + Math.Abs(seg.EndPoint.Y - seg.StartPoint.Y) 'one of them will be 0
            Else
                length = Math.Abs(seg.EndPoint.X - seg.StartPoint.X) 'theoretically each is diagonal
            End If
            Dim x, y As Integer
            For i = 0 To length
                x = seg.StartPoint.X + Math.Sign(seg.EndPoint.X - seg.StartPoint.X) * i
                y = seg.StartPoint.Y + Math.Sign(seg.EndPoint.Y - seg.StartPoint.Y) * i


                If Map.ContainsKey(x) = False Then Map.Add(x, New Dictionary(Of Integer, Integer))
                If Map(x).ContainsKey(y) = False Then Map(x).Add(y, 0)
                Map(x)(y) += 1
            Next
        Next
        'Day05_VisualizeMap(Map)
        Return Map.Sum(Function(f) f.Value.LongCount(Function(g) g.Value >= 2))
    End Function

    Sub Day05_VisualizeMap(Map As Dictionary(Of Integer, Dictionary(Of Integer, Integer)))
        Console.WriteLine()
        Dim MinY = Map.Min(Function(f) f.Value.Keys.Min)
        Dim MaxY = Map.Max(Function(f) f.Value.Keys.Max)

        For X = Map.Keys.Min To Map.Keys.Max
            Dim s As String = ""
            For Y = MinY To MaxY
                If Map.ContainsKey(X) = True AndAlso Map(X).ContainsKey(Y) Then
                    s += Map(X)(Y).ToString.First
                Else
                    s += "."c
                End If
            Next
            Console.WriteLine(s)
        Next

    End Sub
End Module
