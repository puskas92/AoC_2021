Module Day22
    Sub Day22_main()
        Dim input = Day22_ReadInput("Day22\Day22_input.txt")

        'part1
        Debug.Assert(Day22_Part12(Day22_ReadInput("Day22\Day22_test01.txt")).Item1 = 39)
        Debug.Assert(Day22_Part12(Day22_ReadInput("Day22\Day22_test02.txt")).Item1 = 590784)

        'part2
        Debug.Assert(Day22_Part12(Day22_ReadInput("Day22\Day22_test03.txt")).Equals(New Tuple(Of UInt64, UInt64)(474140, 2758514936282235)))
        Dim result = Day22_Part12(input)
        Console.WriteLine("Day22 Part1: " & result.Item1)
        Console.WriteLine("Day22 Part2: " & result.Item2)

    End Sub
    Function Day22_ReadInput(inputpath As String) As List(Of Day22_cuboid)
        Dim result = New List(Of Day22_cuboid)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            result.Add(New Day22_cuboid(sr.ReadLine))
        End While
        sr.Close()
        Return result
    End Function


    Class Day22_cuboid
        Public StartPoint As Day19_3DPoint
        Public EndPoint As Day19_3DPoint
        Public State As Boolean

        Public Sub New(sp As Day19_3DPoint, ep As Day19_3DPoint, state As Boolean)
            StartPoint = sp
            EndPoint = ep
            Me.State = state
        End Sub

        Public Sub New(s As String)
            'on x=-20..26,y=-36..17,z=-47..7
            State = If(s.Split(" ")(0).Trim = "on", True, False)
            Dim s2 = s.Split(" ")(1).Split(",").ToList.ConvertAll(Function(f) f.Split("=")(1))
            StartPoint = New Day19_3DPoint(Convert.ToInt32(s2(0).Split(".")(0)), Convert.ToInt32(s2(1).Split(".")(0)), Convert.ToInt32(s2(2).Split(".")(0)))
            EndPoint = New Day19_3DPoint(Convert.ToInt32(s2(0).Split(".")(2)), Convert.ToInt32(s2(1).Split(".")(2)), Convert.ToInt32(s2(2).Split(".")(2)))

            Debug.Assert((EndPoint - StartPoint).X >= 0)
            Debug.Assert((EndPoint - StartPoint).Y >= 0)
            Debug.Assert((EndPoint - StartPoint).Z >= 0)
        End Sub

        Public ReadOnly Property isInside50 As Boolean
            Get
                Return StartPoint.X >= -50 AndAlso StartPoint.X <= 50 AndAlso EndPoint.X >= -50 AndAlso EndPoint.X <= 50 AndAlso StartPoint.Y >= -50 AndAlso StartPoint.Y <= 50 AndAlso EndPoint.Y >= -50 AndAlso EndPoint.Y <= 50 AndAlso StartPoint.Z >= -50 AndAlso StartPoint.Z <= 50 AndAlso EndPoint.Z >= -50 AndAlso EndPoint.Z <= 50
            End Get
        End Property

        Public ReadOnly Property Size As UInt64
            Get
                Dim v = EndPoint - StartPoint
                Dim result As UInt64 = 1
                result *= (v.X + 1)
                result *= (v.Y + 1)
                result *= (v.Z + 1)
                Return result
            End Get
        End Property

        Public Shared Function isIntersect(a As Day22_cuboid, b As Day22_cuboid) As Boolean
            If (a.StartPoint.X >= b.StartPoint.X AndAlso a.StartPoint.X <= b.EndPoint.X) OrElse (a.EndPoint.X >= b.StartPoint.X AndAlso a.EndPoint.X <= b.EndPoint.X) OrElse (b.StartPoint.X >= a.StartPoint.X AndAlso b.StartPoint.X <= a.EndPoint.X) OrElse (b.EndPoint.X >= a.StartPoint.X AndAlso b.EndPoint.X <= a.EndPoint.X) Then
                If (a.StartPoint.Y >= b.StartPoint.Y AndAlso a.StartPoint.Y <= b.EndPoint.Y) OrElse (a.EndPoint.Y >= b.StartPoint.Y AndAlso a.EndPoint.Y <= b.EndPoint.Y) OrElse (b.StartPoint.Y >= a.StartPoint.Y AndAlso b.StartPoint.Y <= a.EndPoint.Y) OrElse (b.EndPoint.Y >= a.StartPoint.Y AndAlso b.EndPoint.Y <= a.EndPoint.Y) Then
                    If (a.StartPoint.Z >= b.StartPoint.Z AndAlso a.StartPoint.Z <= b.EndPoint.Z) OrElse (a.EndPoint.Z >= b.StartPoint.Z AndAlso a.EndPoint.Z <= b.EndPoint.Z) OrElse (b.StartPoint.Z >= a.StartPoint.Z AndAlso b.StartPoint.Z <= a.EndPoint.Z) OrElse (b.EndPoint.Z >= a.StartPoint.Z AndAlso b.EndPoint.Z <= a.EndPoint.Z) Then
                        Return True
                        Exit Function
                    End If
                End If
            End If

            Return False
        End Function
    End Class

    Function Day22_Part12(input As List(Of Day22_cuboid)) As Tuple(Of UInt64, UInt64)
        Dim result As New List(Of Day22_cuboid)
        result.Add(input.Last)
        For i = input.Count - 2 To 0 Step -1
            Dim subresults = New List(Of Day22_cuboid)
            subresults.Add(input(i))

            For Each fixcuboid In result
                Dim changed = False
                Do
                    changed = False
                    For j = 0 To subresults.Count - 1
                        Dim toCheckCuboid = subresults(j)
                        If Day22_cuboid.isIntersect(toCheckCuboid, fixcuboid) Then
                            Dim x1, x2 As Integer
                            If fixcuboid.StartPoint.X <= toCheckCuboid.StartPoint.X AndAlso fixcuboid.EndPoint.X >= toCheckCuboid.EndPoint.X Then
                                'case 3 on my note
                                x1 = toCheckCuboid.StartPoint.X
                                x2 = toCheckCuboid.EndPoint.X
                            ElseIf fixcuboid.StartPoint.X >= toCheckCuboid.StartPoint.X AndAlso fixcuboid.StartPoint.X <= toCheckCuboid.EndPoint.X AndAlso fixcuboid.EndPoint.X >= toCheckCuboid.EndPoint.X Then
                                'case 1 on my note
                                x1 = fixcuboid.StartPoint.X
                                x2 = toCheckCuboid.EndPoint.X
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(toCheckCuboid.StartPoint.X, toCheckCuboid.StartPoint.Y, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(x1 - 1, toCheckCuboid.EndPoint.Y, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))
                            ElseIf fixcuboid.StartPoint.X <= toCheckCuboid.StartPoint.X AndAlso fixcuboid.EndPoint.X >= toCheckCuboid.StartPoint.X AndAlso fixcuboid.EndPoint.X <= toCheckCuboid.EndPoint.X Then
                                'case 2 on my note
                                x1 = toCheckCuboid.StartPoint.X
                                x2 = fixcuboid.EndPoint.X
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x2 + 1, toCheckCuboid.StartPoint.Y, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(toCheckCuboid.EndPoint.X, toCheckCuboid.EndPoint.Y, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))

                            ElseIf fixcuboid.StartPoint.X > toCheckCuboid.StartPoint.X AndAlso fixcuboid.StartPoint.X < toCheckCuboid.EndPoint.X AndAlso fixcuboid.EndPoint.X < toCheckCuboid.EndPoint.X AndAlso fixcuboid.EndPoint.X > toCheckCuboid.StartPoint.X Then
                                'case 4 on my note
                                x1 = fixcuboid.StartPoint.X
                                x2 = fixcuboid.EndPoint.X
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(toCheckCuboid.StartPoint.X, toCheckCuboid.StartPoint.Y, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(x1 - 1, toCheckCuboid.EndPoint.Y, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x2 + 1, toCheckCuboid.StartPoint.Y, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(toCheckCuboid.EndPoint.X, toCheckCuboid.EndPoint.Y, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))
                            Else
                                Throw New NotImplementedException
                            End If

                            Dim y1, y2 As Integer
                            If fixcuboid.StartPoint.Y <= toCheckCuboid.StartPoint.Y AndAlso fixcuboid.EndPoint.Y >= toCheckCuboid.EndPoint.Y Then
                                'case 3 on my note
                                y1 = toCheckCuboid.StartPoint.Y
                                y2 = toCheckCuboid.EndPoint.Y
                            ElseIf fixcuboid.StartPoint.Y >= toCheckCuboid.StartPoint.Y AndAlso fixcuboid.StartPoint.Y <= toCheckCuboid.EndPoint.Y AndAlso fixcuboid.EndPoint.Y >= toCheckCuboid.EndPoint.Y Then
                                'case 1 on my note
                                y1 = fixcuboid.StartPoint.Y
                                y2 = toCheckCuboid.EndPoint.Y
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x1, toCheckCuboid.StartPoint.Y, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(x2, y1 - 1, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))
                            ElseIf fixcuboid.StartPoint.Y <= toCheckCuboid.StartPoint.Y AndAlso fixcuboid.EndPoint.Y >= toCheckCuboid.StartPoint.Y AndAlso fixcuboid.EndPoint.Y <= toCheckCuboid.EndPoint.Y Then
                                'case 2 on my note
                                y1 = toCheckCuboid.StartPoint.Y
                                y2 = fixcuboid.EndPoint.Y
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x1, y2 + 1, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(x2, toCheckCuboid.EndPoint.Y, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))

                            ElseIf fixcuboid.StartPoint.Y > toCheckCuboid.StartPoint.Y AndAlso fixcuboid.StartPoint.Y < toCheckCuboid.EndPoint.Y AndAlso fixcuboid.EndPoint.Y < toCheckCuboid.EndPoint.Y AndAlso fixcuboid.EndPoint.Y > toCheckCuboid.StartPoint.Y Then
                                'case 4 on my note
                                y1 = fixcuboid.StartPoint.Y
                                y2 = fixcuboid.EndPoint.Y
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x1, toCheckCuboid.StartPoint.Y, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(x2, y1 - 1, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x1, y2 + 1, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(x2, toCheckCuboid.EndPoint.Y, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))
                            Else
                                Throw New NotImplementedException
                            End If

                            Dim z1, z2 As Integer
                            If fixcuboid.StartPoint.Z <= toCheckCuboid.StartPoint.Z AndAlso fixcuboid.EndPoint.Z >= toCheckCuboid.EndPoint.Z Then
                                'case 3 on my note
                                z1 = toCheckCuboid.StartPoint.Z
                                z2 = toCheckCuboid.EndPoint.Z
                            ElseIf fixcuboid.StartPoint.Z >= toCheckCuboid.StartPoint.Z AndAlso fixcuboid.StartPoint.Z <= toCheckCuboid.EndPoint.Z AndAlso fixcuboid.EndPoint.Z >= toCheckCuboid.EndPoint.Z Then
                                'case 1 on my note
                                z1 = fixcuboid.StartPoint.Z
                                z2 = toCheckCuboid.EndPoint.Z
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x1, y1, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(x2, y2, z1 - 1), toCheckCuboid.State))
                            ElseIf fixcuboid.StartPoint.Z <= toCheckCuboid.StartPoint.Z AndAlso fixcuboid.EndPoint.Z >= toCheckCuboid.StartPoint.Z AndAlso fixcuboid.EndPoint.Z <= toCheckCuboid.EndPoint.Z Then
                                'case 2 on my note
                                z1 = toCheckCuboid.StartPoint.Z
                                z2 = fixcuboid.EndPoint.Z
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x1, y1, z2 + 1), New Day19_3DPoint(x2, y2, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))

                            ElseIf fixcuboid.StartPoint.Z > toCheckCuboid.StartPoint.Z AndAlso fixcuboid.StartPoint.Z < toCheckCuboid.EndPoint.Z AndAlso fixcuboid.EndPoint.Z < toCheckCuboid.EndPoint.Z AndAlso fixcuboid.EndPoint.Z > toCheckCuboid.StartPoint.Z Then
                                'case 4 on my note
                                z1 = fixcuboid.StartPoint.Z
                                z2 = fixcuboid.EndPoint.Z
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x1, y1, toCheckCuboid.StartPoint.Z), New Day19_3DPoint(x2, y2, z1 - 1), toCheckCuboid.State))
                                subresults.Add(New Day22_cuboid(New Day19_3DPoint(x1, y1, z2 + 1), New Day19_3DPoint(x2, y2, toCheckCuboid.EndPoint.Z), toCheckCuboid.State))
                            Else
                                Throw New NotImplementedException
                            End If


                            changed = True

                            subresults.RemoveAt(j)
                            Exit For
                        End If
                    Next
                Loop While changed
            Next

            result.AddRange(subresults)
        Next


        Dim value1 As UInt64 = result.Sum(Function(f) If(f.State AndAlso f.isInside50, f.Size, 0))
        Dim value2 As UInt64 = 0
        For Each f In result
            value2 += If(f.State, f.Size, 0)
        Next
        Return New Tuple(Of ULong, ULong)(value1, value2)
    End Function


End Module
