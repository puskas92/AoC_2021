Imports System.Collections
Imports System.Diagnostics.CodeAnalysis

Module Day15
    Sub Day15_main()
        Dim testinput = Day15_ReadInput("Day15\Day15_test01.txt")
        Dim input = Day15_ReadInput("Day15\Day15_input.txt")

        'part1
        Debug.Assert(Day15_Part1(testinput) = 40)
        Console.WriteLine("Day15 Part1: " & Day15_Part1(input))

        'part2
        Debug.Assert(Day15_Part2(testinput) = 315)
        Console.WriteLine("Day15 Part2: " & Day15_Part2(input))

    End Sub
    Function Day15_ReadInput(inputpath As String) As List(Of List(Of Byte))
        Dim result As New List(Of List(Of Byte))

        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            Dim line = sr.ReadLine
            result.Add(line.Trim.ToList.ConvertAll(Function(f) Convert.ToByte(f.ToString)))
        End While
        sr.Close()
        Return result
    End Function


    Function Day15_Part1(input As List(Of List(Of Byte))) As Integer
        Dim direction As New List(Of Drawing.Point)
        direction.Add(New Drawing.Point(1, 0))
        direction.Add(New Drawing.Point(0, 1))
        direction.Add(New Drawing.Point(-1, 0))
        direction.Add(New Drawing.Point(0, -1))

        Dim maxX = input.Count - 1
        Dim maxY = input(0).Count - 1

        Dim currentPos As New Drawing.Point(0, 0)
        Dim toCheckList As New SortedSet(Of Day15_MapElement)(New Day15_MapElementComparer)
        toCheckList.Add(New Day15_MapElement(currentPos, 0)) 'input(0)(0)))

        Dim map As New Dictionary(Of Integer, Dictionary(Of Integer, Day15_MapElement))

        While toCheckList.Count > 0
            Dim toCheck = toCheckList.First
            toCheckList.Remove(toCheck)

            If map.ContainsKey(toCheck.Position.X) = False Then map.Add(toCheck.Position.X, New Dictionary(Of Integer, Day15_MapElement))
            If map(toCheck.Position.X).ContainsKey(toCheck.Position.Y) = False Then map(toCheck.Position.X).Add(toCheck.Position.Y, toCheck)

            For Each dir1 In direction
                Dim newpos As New Drawing.Point(toCheck.Position.X + dir1.X, toCheck.Position.Y + dir1.Y)
                If newpos.X < 0 OrElse newpos.X > maxX OrElse newpos.Y < 0 OrElse newpos.Y > maxY Then Continue For

                Dim distance = toCheck.Distance + input(newpos.X)(newpos.Y)

                If map.ContainsKey(newpos.X) AndAlso map(newpos.X).ContainsKey(newpos.Y) AndAlso map(newpos.X)(newpos.Y).Distance <= distance Then Continue For

                toCheckList.Add(New Day15_MapElement(newpos, distance))
            Next
        End While

        Return map(maxX)(maxY).Distance

    End Function

    Class Day15_MapElement
        Public Position As Drawing.Point
        Public Distance As Integer
        'Public Route As List(Of Drawing.Point)

        Public Sub New(pos As Drawing.Point, dist As Integer)
            Position = pos
            Distance = dist
        End Sub

    End Class

    Class Day15_MapElementComparer
        Implements IComparer(Of Day15_MapElement)

        Public Function Compare(<AllowNull> x As Day15_MapElement, <AllowNull> y As Day15_MapElement) As Integer Implements IComparer(Of Day15_MapElement).Compare
            If x.Position.X = y.Position.X AndAlso x.Position.Y = y.Position.Y AndAlso x.Distance = y.Distance Then Return 0
            Dim distComp = x.Distance.CompareTo(y.Distance)
            If distComp = 0 Then
                Dim manhattan = (y.Position.X + y.Position.Y).CompareTo(x.Position.X + x.Position.Y)
                If manhattan = 0 Then
                    Return (y.Position.X).CompareTo(x.Position.X)
                Else
                    Return manhattan
                End If
            Else
                Return distComp
            End If
        End Function
    End Class


    Function Day15_Part2(input As List(Of List(Of Byte))) As Integer
        Dim newInput As New List(Of List(Of Byte))
        For i = 0 To 4
            For k = 0 To input.Count - 1
                newInput.Add(New List(Of Byte))

                For j = 0 To 4
                    For l = 0 To input(k).Count - 1
                        Dim value = (input(k)(l) + i + j)
                        If value > 9 Then value = value - 9
                        If value > 9 Then value = value - 9
                        newInput.Last.Add(value)
                    Next
                Next
            Next
        Next

        Return Day15_Part1(newInput)
    End Function
End Module
