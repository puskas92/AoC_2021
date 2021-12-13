Module Day13
    Sub Day13_main()
        Dim testinput = New Day13_Input("Day13\Day13_test01.txt")
        Dim input = New Day13_Input("Day13\Day13_input.txt")

        'part1
        Debug.Assert(Day13_Part1(testinput) = 17)
        Console.WriteLine("Day13 Part1: " & Day13_Part1(input))

        'part2
        'Day13_Part2(testinput)
        Console.WriteLine("Day13 Part2: ")
        Day13_Part2(input)

    End Sub


    Class Day13_Input
        Public Dots As List(Of Drawing.Point)
        Public FoldInstruction As Queue(Of Tuple(Of Char, Integer))

        Public Sub New(inputpath As String)
            Dots = New List(Of Drawing.Point)
            FoldInstruction = New Queue(Of Tuple(Of Char, Integer))

            Dim sr As New IO.StreamReader(inputpath)
            Dim line As String = ""
            While (Not sr.EndOfStream)
                line = sr.ReadLine
                If line = "" Then Exit While
                Dots.Add(New Drawing.Point(Convert.ToInt32(line.Split(",")(0).Trim), Convert.ToInt32(line.Split(",")(1).Trim)))
            End While
            While (Not sr.EndOfStream)
                line = sr.ReadLine
                FoldInstruction.Enqueue(New Tuple(Of Char, Integer)(line.Split("=")(0).Split("g")(1).Trim.First, Convert.ToInt32(line.Split("=")(1).Trim)))
            End While
            sr.Close()
        End Sub

        Public Sub FoldOnce()
            Dim fold = FoldInstruction.Dequeue
            Dim afterFoldDots As New List(Of Drawing.Point)

            For i = 0 To Dots.Count - 1
                Dim dot = Dots(i)
                If fold.Item1 = "x"c Then
                    If dot.X > fold.Item2 Then
                        Dim newPoint As New Drawing.Point((fold.Item2 - dot.X) + fold.Item2, dot.Y)
                        If afterFoldDots.Contains(newPoint) = False Then afterFoldDots.Add(newPoint)
                    Else
                        If afterFoldDots.Contains(dot) = False Then afterFoldDots.Add(dot)
                    End If
                Else
                    If dot.Y > fold.Item2 Then
                        Dim newPoint As New Drawing.Point(dot.X, (fold.Item2 - dot.Y) + fold.Item2)
                        If afterFoldDots.Contains(newPoint) = False Then afterFoldDots.Add(newPoint)
                    Else
                        If afterFoldDots.Contains(dot) = False Then afterFoldDots.Add(dot)
                    End If
                End If
            Next

            Dots = afterFoldDots
        End Sub
    End Class

    Function Day13_Part1(input As Day13_Input) As Integer
        input.FoldOnce()
        Return input.Dots.Count
    End Function

    Sub Day13_Part2(input As Day13_Input)
        While input.FoldInstruction.Count > 0
            input.FoldOnce()
        End While

        Dim maxX = input.Dots.Max(Function(f) f.X)
        Dim maxY = input.Dots.Max(Function(f) f.Y)
        Dim s As String = ""
        For y = 0 To maxY
            For x = 0 To maxX
                s &= If(input.Dots.Contains(New Drawing.Point(x, y)), "#", " ")
            Next
            s &= vbCrLf
        Next
        Console.WriteLine(s)
    End Sub
End Module
