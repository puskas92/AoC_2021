Imports System.Diagnostics.CodeAnalysis

Module Day19
    Sub Day19_main()
        Dim input = Day19_ReadInput("Day19\Day19_input.txt")

        'part1
        Debug.Assert(Day19_Part12(Day19_ReadInput("Day19\Day19_test01.txt")).Equals(New Tuple(Of Integer, Integer)(79, 3621)))
        Dim result = Day19_Part12(input)
        Console.WriteLine("Day19 Part1: " & result.Item1)
        Console.WriteLine("Day19 Part2: " & result.Item2)

    End Sub
    Function Day19_ReadInput(inputpath As String) As List(Of Day19_Scanners)
        Dim result As New List(Of Day19_Scanners)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            Dim scanner As New Day19_Scanners
            Dim line As String
            line = sr.ReadLine
            scanner.Number = Convert.ToInt32(line.Split("r")(1).Split("-")(0).Trim)
            While True
                line = sr.ReadLine
                If line = "" Then Exit While

                scanner.Beacons.Add(New Day19_3DPoint(Convert.ToInt32(line.Split(",")(0)), Convert.ToInt32(line.Split(",")(1)), Convert.ToInt32(line.Split(",")(2))))
            End While
            result.Add(scanner)
        End While
        sr.Close()
        Return result
    End Function

    Class Day19_Scanners
        Public Number As Integer
        Public Beacons As New List(Of Day19_3DPoint)
        Public Position As New Day19_3DPoint
        Public Orientation As Integer = 0


        Private _ManhattanAndVectorMatrix As List(Of Tuple(Of Int64, Integer, Integer)) 'distance, index1, index2
        Private _ManhattanCalculatedIndex As Integer = 0
        Public ReadOnly Property ManhattanAndVectorMatrix As List(Of Tuple(Of Int64, Integer, Integer))
            Get
                If _ManhattanAndVectorMatrix IsNot Nothing AndAlso _ManhattanCalculatedIndex = Beacons.Count Then Return _ManhattanAndVectorMatrix
                If _ManhattanAndVectorMatrix Is Nothing Then _ManhattanAndVectorMatrix = New List(Of Tuple(Of Int64, Integer, Integer))
                For i = 0 To Beacons.Count - 2
                    For j = Math.Max(i + 1, _ManhattanCalculatedIndex) To Beacons.Count - 1
                        Dim value As Int64
                        value = Day19_3DPoint.Distance(Beacons(i), Beacons(j))
                        Dim vector = Beacons(i) - Beacons(j)
                        value *= If(vector.X <> 0, vector.X, 1)
                        value *= If(vector.Y <> 0, vector.Y, 1)
                        value *= If(vector.Z <> 0, vector.Z, 1)
                        value = Math.Abs(value)
                        _ManhattanAndVectorMatrix.Add(New Tuple(Of Int64, Integer, Integer)(value, i, j))
                    Next
                Next
                _ManhattanCalculatedIndex = Beacons.Count
                _ManhattanAndVectorMatrix.Sort(Function(f, g) f.Item1.CompareTo(g.Item1))
                Return _ManhattanAndVectorMatrix
            End Get
        End Property

        Public Function PointInPosAndOrientation(p As Day19_3DPoint) As Day19_3DPoint
            Dim result = New Day19_3DPoint(p.X, p.Y, p.Z)

            Dim Xrot = Orientation Mod 4
            Dim Yrot = Math.Floor(Orientation / 4) Mod 4
            Dim Zrot = Math.Floor(Orientation / 16) Mod 4
            For i = 1 To Xrot
                result = New Day19_3DPoint(result.X, -result.Z, result.Y)
            Next
            For i = 1 To Yrot
                result = New Day19_3DPoint(result.Z, result.Y, -result.X)
            Next
            For i = 1 To Zrot
                result = New Day19_3DPoint(-result.Y, result.X, result.Z)
            Next

            result += Position

            Return result
        End Function
    End Class

    Class Day19_3DPoint
        Public X As Integer
        Public Y As Integer
        Public Z As Integer

        Public Sub New()
            X = 0
            Y = 0
            Z = 0
        End Sub

        Public Sub New(x As Integer, y As Integer, z As Integer)
            Me.X = x
            Me.Y = y
            Me.Z = z
        End Sub

        Public Overrides Function ToString() As String
            Return X.ToString() & "," & Y.ToString() & "," & Z.ToString()
        End Function

        Public Shared Function Distance(a As Day19_3DPoint, b As Day19_3DPoint)
            Return Math.Abs(VectorLength(a - b))
        End Function

        Public Shared Function VectorLength(a As Day19_3DPoint)
            Return Math.Abs(a.X) + Math.Abs(a.Y) + Math.Abs(a.Z)
        End Function

        Public Overloads Shared Operator -(a As Day19_3DPoint, b As Day19_3DPoint) As Day19_3DPoint
            Return New Day19_3DPoint(a.X - b.X, a.Y - b.Y, a.Z - b.Z)
        End Operator

        Public Overloads Shared Operator +(a As Day19_3DPoint, b As Day19_3DPoint) As Day19_3DPoint
            Return New Day19_3DPoint(a.X + b.X, a.Y + b.Y, a.Z + b.Z)
        End Operator

        Public Overloads Shared Operator =(a As Day19_3DPoint, b As Day19_3DPoint) As Boolean
            Return (a.X = b.X) AndAlso (a.Y = b.Y) AndAlso (a.Z = b.Z)
        End Operator

        Public Overloads Shared Operator <>(a As Day19_3DPoint, b As Day19_3DPoint) As Boolean
            Return Not (a = b)
        End Operator
    End Class
    Function Day19_Part12(OriginalInput As List(Of Day19_Scanners)) As Tuple(Of Integer, Integer)
        Dim inputCopy = New List(Of Day19_Scanners)(OriginalInput)
        Dim picture As New Day19_Scanners
        picture.Beacons.AddRange(inputCopy(0).Beacons)
        inputCopy.Remove(inputCopy(0))
        While True
            If inputCopy.Count = 0 Then Exit While
            Dim found As Boolean = False

            'picture.RedoManhattan()
            Dim intersect As New List(Of List(Of Tuple(Of Integer, Int64, Integer, Integer, Integer, Integer)))

            For j = 0 To inputCopy.Count - 1
                intersect.Add(New List(Of Tuple(Of Integer, Int64, Integer, Integer, Integer, Integer)))
                For Each mh In picture.ManhattanAndVectorMatrix
                    Dim a = inputCopy(j).ManhattanAndVectorMatrix.Find(Function(f) f.Item1 = mh.Item1)
                    If a IsNot Nothing Then
                        intersect(j).Add(New Tuple(Of Integer, Int64, Integer, Integer, Integer, Integer)(j, mh.Item1, mh.Item2, mh.Item3, a.Item2, a.Item3))
                    End If
                Next
            Next

            intersect.Sort(Function(f, g) g.Count.CompareTo(f.Count)) 'order lists based on intersect size
            Dim bestOverlapScannerIntersect = intersect.First
            Dim bestOverlapScanner = inputCopy(bestOverlapScannerIntersect.First.Item1)

            found = False
            Dim i As Integer
            For Each inter In bestOverlapScannerIntersect
                For i = 0 To 63
                    bestOverlapScanner.Position = New Day19_3DPoint(0, 0, 0)
                    bestOverlapScanner.Orientation = i
                    Dim firstintersectpoint = bestOverlapScanner.Beacons(inter.Item5)

                    bestOverlapScanner.Position = picture.Beacons(inter.Item3) - bestOverlapScanner.PointInPosAndOrientation(firstintersectpoint)

                    If bestOverlapScanner.PointInPosAndOrientation(firstintersectpoint) = picture.Beacons(inter.Item3) Then 'should be always true
                        If bestOverlapScanner.PointInPosAndOrientation(bestOverlapScanner.Beacons(inter.Item6)) = picture.Beacons(inter.Item4) Then
                            Dim match As Integer = 0
                            For Each p In bestOverlapScanner.Beacons
                                Dim point = bestOverlapScanner.PointInPosAndOrientation(p)
                                For Each p2 In picture.Beacons
                                    If point = p2 Then
                                        match += 1
                                        Exit For
                                    End If
                                Next
                            Next
                            If match >= 12 Then Exit For
                        End If
                    End If

                    bestOverlapScanner.Position = New Day19_3DPoint(0, 0, 0)
                    bestOverlapScanner.Position = picture.Beacons(inter.Item4) - bestOverlapScanner.PointInPosAndOrientation(firstintersectpoint)

                    If bestOverlapScanner.PointInPosAndOrientation(firstintersectpoint) = picture.Beacons(inter.Item4) Then 'should be always true
                        If bestOverlapScanner.PointInPosAndOrientation(bestOverlapScanner.Beacons(inter.Item6)) = picture.Beacons(inter.Item3) Then
                            Dim match As Integer = 0
                            For Each p In bestOverlapScanner.Beacons
                                Dim point = bestOverlapScanner.PointInPosAndOrientation(p)
                                For Each p2 In picture.Beacons
                                    If point = p2 Then
                                        match += 1
                                        Exit For
                                    End If
                                Next
                            Next
                            If match >= 12 Then Exit For
                        End If
                    End If
                Next
                If i <= 63 Then
                    found = True
                    Exit For
                End If
            Next

            Debug.Assert(found = True)

            For Each p In bestOverlapScanner.Beacons
                Dim point = bestOverlapScanner.PointInPosAndOrientation(p)
                found = False
                For Each p2 In picture.Beacons
                    If point = p2 Then
                        found = True
                        Exit For
                    End If
                Next
                If found = False Then picture.Beacons.Add(point)
            Next
            OriginalInput(bestOverlapScanner.Number).Position = bestOverlapScanner.Position
            OriginalInput(bestOverlapScanner.Number).Orientation = bestOverlapScanner.Orientation
            inputCopy.Remove(bestOverlapScanner)
        End While

        Dim result1 = picture.Beacons.Count

        Dim ManhattanMatrix = New List(Of Tuple(Of Int64, Integer, Integer))
        For i = 0 To OriginalInput.Count - 2
            For j = i + 1 To OriginalInput.Count - 1
                Dim value As Int64
                value = Day19_3DPoint.Distance(OriginalInput(i).Position, OriginalInput(j).Position)
                ManhattanMatrix.Add(New Tuple(Of Int64, Integer, Integer)(value, i, j))
            Next
        Next
        Dim result2 = ManhattanMatrix.Max(Function(f) f.Item1)
        Return New Tuple(Of Integer, Integer)(result1, result2)
    End Function
End Module
