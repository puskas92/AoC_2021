Module Day20
    Sub Day20_main()
        Dim input = Day20_ReadInput("Day20\Day20_input.txt")

        'part1
        Debug.Assert(Day20_Part1(Day20_ReadInput("Day20\Day20_test01.txt"), 2) = 35)
        Console.WriteLine("Day20 Part1: " & Day20_Part1(input, 2))

        'part2
        Debug.Assert(Day20_Part1(Day20_ReadInput("Day20\Day20_test01.txt"), 50) = 3351)
        Console.WriteLine("Day20 Part2: " & Day20_Part1(input, 50))

    End Sub
    Function Day20_ReadInput(inputpath As String) As Day20_Input
        Dim result As New Day20_Input
        Dim sr As New IO.StreamReader(inputpath)
        Dim line = sr.ReadLine
        result.EnhanceAlgorithm = line.ToList.ConvertAll(Function(f) (f = "#"c))
        line = sr.ReadLine
        While (Not sr.EndOfStream)
            result.Image.Add(sr.ReadLine.ToList.ConvertAll(Function(f) (f = "#"c)))
        End While
        sr.Close()
        Return result
    End Function

    Class Day20_Input
        Public EnhanceAlgorithm As New List(Of Boolean)
        Public Image As New List(Of List(Of Boolean))
        Public Step1 As Integer = 0
        Public InfinitePixel As Boolean = False

        Public Sub MakeStep()
            Dim NextImage As New List(Of List(Of Boolean))

            Dim directions = New List(Of Drawing.Point)
            directions.Add(New Drawing.Point(-1, -1))
            directions.Add(New Drawing.Point(-1, 0))
            directions.Add(New Drawing.Point(-1, 1))
            directions.Add(New Drawing.Point(0, -1))
            directions.Add(New Drawing.Point(0, 0))
            directions.Add(New Drawing.Point(0, 1))
            directions.Add(New Drawing.Point(1, -1))
            directions.Add(New Drawing.Point(1, 0))
            directions.Add(New Drawing.Point(1, 1))

            For i = -2 To Image.Count + 1
                NextImage.Add(New List(Of Boolean))
                For j = -2 To Image(0).Count + 1
                    Dim s As String = ""
                    For Each dir1 In directions
                        Dim p As New Drawing.Point(i + dir1.X, j + dir1.Y)
                        If p.X < 0 OrElse p.X > Image.Count - 1 OrElse p.Y < 0 OrElse p.Y > Image(0).Count - 1 Then
                            s &= If(InfinitePixel, "1", "0")
                        Else
                            s &= If(Image(p.X)(p.Y), "1", "0")
                        End If
                    Next
                    NextImage.Last.Add(EnhanceAlgorithm(Convert.ToInt32(s, 2)))
                Next
            Next

            InfinitePixel = If(InfinitePixel, EnhanceAlgorithm(511), EnhanceAlgorithm(0))

            'decrease size, so it will not grow so fast - this is optional and seems to be not making much faster
            Dim changed = True
            While changed
                changed = False
                If NextImage.First.LongCount(Function(f) f = InfinitePixel) = NextImage.First.Count Then
                    changed = True
                    NextImage.RemoveAt(0)
                End If

                If NextImage.Last.LongCount(Function(f) f = InfinitePixel) = NextImage.Last.Count Then
                    changed = True
                    NextImage.Remove(NextImage.Last)
                End If

                If NextImage.Sum(Function(f) f.First = InfinitePixel) = NextImage.Count Then
                    changed = True
                    For Each row In NextImage
                        row.RemoveAt(0)
                    Next
                End If

                If NextImage.Sum(Function(f) f.Last = InfinitePixel) = NextImage.Count Then
                    changed = True
                    For Each row In NextImage
                        row.RemoveAt(row.Count - 1)
                    Next
                End If
            End While


            Image = NextImage
            Step1 += 1
        End Sub

    End Class

    Function Day20_Part1(input As Day20_Input, step1 As Integer) As Integer
        While input.Step1 < step1
            input.MakeStep()
            'Day05_VisualizeImage(input)
        End While
        Return input.Image.Sum(Function(f) f.LongCount(Function(g) g = True))
    End Function


    Sub Day05_VisualizeImage(input As Day20_Input)
        Console.WriteLine()

        For Each row In input.Image
            Dim s As String = ""
            For Each c In row
                s &= If(c, "#", ".")
            Next
            Console.WriteLine(s)
        Next

    End Sub
End Module
