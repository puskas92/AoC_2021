Imports System.Collections
Imports System.Diagnostics.CodeAnalysis

Module Day23
    Sub Day23_main()
        Dim input = Day23_ReadInput("Day23\Day23_input.txt")
        Dim result = Day23_ReadInput("Day23\Day23_result.txt")

        'part1
        Debug.Assert(Day23_Part1(Day23_ReadInput("Day23\Day23_test01.txt"), result) = 12521)
        Console.WriteLine("Day23 Part1: " & Day23_Part1(input, result))

        Day23_main2()
    End Sub
    Function Day23_ReadInput(inputpath As String) As Day23_State
        Dim sr As New IO.StreamReader(inputpath)
        Dim Map(4, 12) As Char
        Dim i, j As Integer
        For i = 0 To 4
            For j = 0 To 12
                Map(i, j) = "#"c
            Next
        Next

            i = 0
        j = 0
        While (Not sr.EndOfStream)
            Dim line = sr.ReadLine
            j = 0
            For Each c In line.ToList
                Map(i, j) = If(c = " "c, "#"c, c)
                j += 1
            Next
            i += 1
        End While
        sr.Close()

        Dim result As New Day23_State
        result.Steps = 0
        result.Map = Map
        Return result
    End Function

    Class Day23_State
        Public Previous As Day23_State

        Public Map As Char(,)
        Public Steps As Integer = 0

        Public Sub New()
            Previous = Nothing
            ReDim Map(4, 12)
            Dim i, j As Integer
            For i = 0 To 4
                For j = 0 To 12
                    Map(i, j) = "#"c
                Next
            Next
        End Sub

        Public Sub New(copy As Day23_State)
            Previous = copy
            ReDim Map(4, 12)
            Dim i, j As Integer
            For i = 0 To 4
                For j = 0 To 12
                    Map(i, j) = copy.Map(i, j)
                Next
            Next
            Steps = copy.Steps
        End Sub

        Public Overrides Function ToString() As String
            Dim s As String = ""

            For j = 1 To 11
                s &= Map(1, j)
            Next
            s &= Map(2, 3)
            s &= Map(2, 5)
            s &= Map(2, 7)
            s &= Map(2, 9)
            s &= Map(3, 3)
            s &= Map(3, 5)
            s &= Map(3, 7)
            s &= Map(3, 9)
            Return s
        End Function

        'Public Overrides Function GetHashCode() As Integer
        '    Return Map.GetHashCode()
        'End Function

        'Public Function CalculateScore()
        '    Dim xvalue = Steps
        '    For i = 1 To 3
        '        For j = 1 To 11
        '            Dim xc = Map(i, j)
        '            Select Case xc
        '                Case "A"c
        '                    xvalue += 1 * Math.Abs(3 - j)
        '                    xvalue += 1 * If(j = 3, 0, (1 + If(i = 1, 0, i)))
        '                Case "B"c
        '                    xvalue += 10 * Math.Abs(5 - j)
        '                    xvalue += 10 * If(j = 5, 0, (1 + If(i = 1, 0, i)))
        '                Case "C"c
        '                    xvalue += 100 * Math.Abs(7 - j)
        '                    xvalue += 100 * If(j = 7, 0, (1 + If(i = 1, 0, i)))
        '                Case "D"c
        '                    xvalue += 1000 * Math.Abs(9 - j)
        '                    xvalue += 1000 * If(9 = 3, 0, (1 + If(i = 1, 0, i)))
        '            End Select
        '        Next
        '    Next
        '    Return xvalue
        'End Function
    End Class


    Class Day23_StateElementComparer
        Implements IComparer(Of Day23_State)

        Public Function Compare(<AllowNull> x As Day23_State, <AllowNull> y As Day23_State) As Integer Implements IComparer(Of Day23_State).Compare
            Dim xvalue = x.Steps
            Dim yvalue = y.Steps
            If x.ToString = y.ToString Then
                Return xvalue.CompareTo(yvalue)
            Else

                For i = 1 To 3
                    For j = 1 To 11
                        Dim xc = x.Map(i, j)
                        Dim yc = y.Map(i, j)
                        Select Case xc
                            Case "A"c
                                xvalue += 1 * Math.Abs(3 - j)
                                xvalue += 1 * If(j = 3, 0, (1 + If(i = 1, 0, i)))
                            Case "B"c
                                xvalue += 10 * Math.Abs(5 - j)
                                xvalue += 10 * If(j = 5, 0, (1 + If(i = 1, 0, i)))
                            Case "C"c
                                xvalue += 100 * Math.Abs(7 - j)
                                xvalue += 100 * If(j = 7, 0, (1 + If(i = 1, 0, i)))
                            Case "D"c
                                xvalue += 1000 * Math.Abs(9 - j)
                                xvalue += 1000 * If(9 = 3, 0, (1 + If(i = 1, 0, i)))
                        End Select

                        Select Case yc
                            Case "A"c
                                yvalue += 1 * Math.Abs(3 - j)
                                yvalue += 1 * If(j = 3, 0, (1 + If(i = 1, 0, i)))
                            Case "B"c
                                yvalue += 10 * Math.Abs(5 - j)
                                yvalue += 10 * If(j = 5, 0, (1 + If(i = 1, 0, i)))
                            Case "C"c
                                yvalue += 100 * Math.Abs(7 - j)
                                yvalue += 100 * If(j = 7, 0, (1 + If(i = 1, 0, i)))
                            Case "D"c
                                yvalue += 1000 * Math.Abs(9 - j)
                                yvalue += 1000 * If(9 = 3, 0, (1 + If(i = 1, 0, i)))
                        End Select
                    Next
                Next
                If xvalue = yvalue Then
                    If x.Steps = y.Steps Then
                        Return 1
                    Else
                        Return x.Steps.CompareTo(y.Steps)
                    End If
                Else
                    Return xvalue.CompareTo(yvalue)
                End If
            End If

            'Return x.Steps.CompareTo(y.Steps)

        End Function
    End Class

    Function Day23_Part1(input As Day23_State, result As Day23_State) As Integer
        'Dim toCheckList As New SortedSet(Of Day23_State)(New Day23_StateElementComparer)
        'Dim tochecklist As New List(Of Day23_State)
        'Dim tochecklist As New orderedBag
        ' Dim tochecklist As New System.Collections.Specializ
        Dim toCheckList As New C5.IntervalHeap(Of Day23_State)(New Day23_StateElementComparer)

        toCheckList.Add(input)

        Dim cache As New Dictionary(Of String, Integer)

        Dim direction As New List(Of Drawing.Point)
        direction.Add(New Drawing.Point(1, 0))
        direction.Add(New Drawing.Point(0, 1))
        direction.Add(New Drawing.Point(-1, 0))
        direction.Add(New Drawing.Point(0, -1))

        Dim part1result As Integer = Integer.MaxValue
        Dim resultstring = result.ToString

        While tochecklist.Count > 0
            'tochecklist.Sort(New Day23_StateElementComparer)
            'Dim min = tochecklist.Min(Function(f) f.CalculateScore)
            'Dim toCheck = tochecklist.Find(Function(f) f.CalculateScore = min)
            'toCheckList.Remove(toCheck)
            Dim toCheck = toCheckList.FindMin()
            toCheckList.DeleteMin()
            Dim currenthashstring = toCheck.ToString
            'tochecklist = tochecklist.FindAll(Function(f) f.Steps < 15300)
            'toCheckList.RemoveWhere(Function(f) f.ToString = currenthashstring)

            DrawState(toCheck)



            If cache.ContainsKey(currenthashstring) Then
                If cache(currenthashstring) <= toCheck.Steps Then
                    Continue While
                Else
                    cache(currenthashstring) = toCheck.Steps
                End If
            Else
                cache.Add(currenthashstring, toCheck.Steps)
            End If

            'check if finished
            If currenthashstring = resultstring Then
                'DrawSteps(toCheck)
                If toCheck.Steps < part1result Then part1result = toCheck.Steps
                'Continue While
                Exit While
            End If
            'get all movable objects
            Dim movables As New List(Of Drawing.Point)
            For i = 1 To 3
                For j = 1 To 11
                    Dim c = toCheck.Map(i, j)
                    If c = "A"c OrElse c = "B"c OrElse c = "C"c OrElse c = "D"c Then
                        For Each dir1 In direction
                            If (toCheck.Map(i + dir1.X, j + dir1.Y) = "."c) Then
                                movables.Add(New Drawing.Point(i, j))
                                Exit For
                            End If
                        Next
                    End If
                Next
            Next

            'calculate all possible positions of the movables and add to the checklist
            For Each movable In movables
                If movable.X = 1 Then ' in the hall --> check if it is possible to move it to the room
                    Select Case toCheck.Map(movable.X, movable.Y)
                        Case "A"c
                            If movable.Y <> 3 Then
                                Dim i As Integer = movable.Y
                                Do
                                    i += Math.Sign((3 - movable.Y))
                                    If toCheck.Map(1, i) <> "."c Then Continue For 'cannot reach the room, block in the way
                                Loop Until i = 3
                            End If

                            If toCheck.Map(2, 3) = "."c Then
                                If toCheck.Map(3, 3) = "."c Then
                                    Dim newstate = New Day23_State(toCheck)
                                    newstate.Map(3, 3) = "A"c
                                    newstate.Map(movable.X, movable.Y) = "."c
                                    newstate.Steps += 1 * (2 + Math.Abs(3 - movable.Y))
                                    Dim newstatehash = newstate.ToString
                                    If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                    DrawState(newstate)
                                    toCheckList.Add(newstate)
                                    Continue While 'this is trivial step, no need to check other steps now
                                ElseIf toCheck.Map(3, 3) = "A"c Then
                                        Dim newstate = New Day23_State(toCheck)
                                    newstate.Map(2, 3) = "A"c
                                    newstate.Map(movable.X, movable.Y) = "."c
                                    newstate.Steps += 1 * (1 + Math.Abs(3 - movable.Y))
                                    Dim newstatehash = newstate.ToString
                                    If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                    DrawState(newstate)
                                    toCheckList.Add(newstate)
                                    Continue While 'this is trivial step, no need to check other steps now
                                End If
                            End If
                        Case "B"c
                            If movable.Y <> 5 Then
                                Dim i As Integer = movable.Y
                                Do
                                    i += Math.Sign((5 - movable.Y))
                                    If toCheck.Map(1, i) <> "."c Then Continue For 'cannot reach the room, block in the way
                                Loop Until i = 5
                            End If
                            If toCheck.Map(2, 5) = "."c Then
                                If toCheck.Map(3, 5) = "."c Then
                                    Dim newstate = New Day23_State(toCheck)
                                    newstate.Map(3, 5) = "B"c
                                    newstate.Map(movable.X, movable.Y) = "."c
                                    newstate.Steps += 10 * (2 + Math.Abs(5 - movable.Y))
                                    Dim newstatehash = newstate.ToString
                                    If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                    DrawState(newstate)
                                    toCheckList.Add(newstate)
                                    Continue While 'this is trivial step, no need to check other steps now
                                ElseIf toCheck.Map(3, 5) = "B"c Then
                                    Dim newstate = New Day23_State(toCheck)
                                    newstate.Map(2, 5) = "B"c
                                    newstate.Map(movable.X, movable.Y) = "."c
                                    newstate.Steps += 10 * (1 + Math.Abs(5 - movable.Y))
                                    Dim newstatehash = newstate.ToString
                                    If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                    DrawState(newstate)
                                    toCheckList.Add(newstate)
                                    Continue While 'this is trivial step, no need to check other steps now
                                End If
                            End If
                        Case "C"c
                            If movable.Y <> 7 Then
                                Dim i As Integer = movable.Y
                                Do
                                    i += Math.Sign((7 - movable.Y))
                                    If toCheck.Map(1, i) <> "."c Then Continue For 'cannot reach the room, block in the way
                                Loop Until i = 7
                            End If
                            If toCheck.Map(2, 7) = "."c Then
                                If toCheck.Map(3, 7) = "."c Then
                                    Dim newstate = New Day23_State(toCheck)
                                    newstate.Map(3, 7) = "C"c
                                    newstate.Map(movable.X, movable.Y) = "."c
                                    newstate.Steps += 100 * (2 + Math.Abs(7 - movable.Y))
                                    Dim newstatehash = newstate.ToString
                                    If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                    DrawState(newstate)
                                    toCheckList.Add(newstate)
                                    Continue While 'this is trivial step, no need to check other steps now
                                ElseIf toCheck.Map(3, 7) = "C"c Then
                                    Dim newstate = New Day23_State(toCheck)
                                    newstate.Map(2, 7) = "C"c
                                    newstate.Map(movable.X, movable.Y) = "."c
                                    newstate.Steps += 100 * (1 + Math.Abs(7 - movable.Y))
                                    Dim newstatehash = newstate.ToString
                                    If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                    DrawState(newstate)
                                    toCheckList.Add(newstate)
                                    Continue While 'this is trivial step, no need to check other steps now
                                End If
                            End If
                        Case "D"c
                            If movable.Y <> 9 Then
                                Dim i As Integer = movable.Y
                                Do
                                    i += Math.Sign((9 - movable.Y))
                                    If toCheck.Map(1, i) <> "."c Then Continue For 'cannot reach the room, block in the way
                                Loop Until i = 9
                            End If
                            If toCheck.Map(2, 9) = "."c Then
                                If toCheck.Map(3, 9) = "."c Then
                                    Dim newstate = New Day23_State(toCheck)
                                    newstate.Map(3, 9) = "D"c
                                    newstate.Map(movable.X, movable.Y) = "."c
                                    newstate.Steps += 1000 * (2 + Math.Abs(9 - movable.Y))
                                    Dim newstatehash = newstate.ToString
                                    If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                    DrawState(newstate)
                                    toCheckList.Add(newstate)
                                    Continue While 'this is trivial step, no need to check other steps now
                                ElseIf toCheck.Map(3, 9) = "D"c Then
                                    Dim newstate = New Day23_State(toCheck)
                                    newstate.Map(2, 9) = "D"c
                                    newstate.Map(movable.X, movable.Y) = "."c
                                    newstate.Steps += 1000 * (1 + Math.Abs(9 - movable.Y))
                                    Dim newstatehash = newstate.ToString
                                    If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                    DrawState(newstate)
                                    toCheckList.Add(newstate)
                                    Continue While 'this is trivial step, no need to check other steps now
                                End If
                            End If
                    End Select
                Else 'in a room
                    Select Case toCheck.Map(movable.X, movable.Y)
                        Case "A"c
                            If movable.Y = 3 Then 'it might be already in good position
                                If movable.X = 3 OrElse toCheck.Map(3, 3) = "A"c Then Continue For
                            End If
                            If toCheck.Map(1, movable.Y) <> "."c Then Continue For 'something at the exit
                            Dim i As Integer = movable.Y
                            While i < 11 'right steps
                                i += 1
                                If toCheck.Map(1, i) <> "."c Then Exit While 'something blocks the way
                                If i = 3 OrElse i = 5 OrElse i = 7 OrElse i = 9 Then Continue While
                                Dim newstate = New Day23_State(toCheck)
                                newstate.Map(1, i) = "A"c
                                newstate.Map(movable.X, movable.Y) = "."c
                                newstate.Steps += 1 * (movable.X - 1 + Math.Abs(i - movable.Y))
                                Dim newstatehash = newstate.ToString
                                If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                DrawState(newstate)
                                toCheckList.Add(newstate)
                            End While
                            i = movable.Y
                            While i > 1 'left steps
                                i -= 1
                                If toCheck.Map(1, i) <> "."c Then Exit While 'something blocks the way
                                If i = 3 OrElse i = 5 OrElse i = 7 OrElse i = 9 Then Continue While
                                Dim newstate = New Day23_State(toCheck)
                                newstate.Map(1, i) = "A"c
                                newstate.Map(movable.X, movable.Y) = "."c
                                newstate.Steps += 1 * (movable.X - 1 + Math.Abs(i - movable.Y))
                                Dim newstatehash = newstate.ToString
                                If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                DrawState(newstate)
                                toCheckList.Add(newstate)
                            End While
                        Case "B"c
                            If movable.Y = 5 Then 'it might be already in good position
                                If movable.X = 3 OrElse toCheck.Map(3, 5) = "B"c Then Continue For
                            End If
                            If toCheck.Map(1, movable.Y) <> "."c Then Continue For 'something at the exit
                            Dim i As Integer = movable.Y
                            While i < 11 'right steps
                                i += 1
                                If toCheck.Map(1, i) <> "."c Then Exit While 'something blocks the way
                                If i = 3 OrElse i = 5 OrElse i = 7 OrElse i = 9 Then Continue While
                                Dim newstate = New Day23_State(toCheck)
                                newstate.Map(1, i) = "B"c
                                newstate.Map(movable.X, movable.Y) = "."c
                                newstate.Steps += 10 * (movable.X - 1 + Math.Abs(i - movable.Y))
                                Dim newstatehash = newstate.ToString
                                If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                DrawState(newstate)
                                toCheckList.Add(newstate)
                            End While
                            i = movable.Y
                            While i > 1 'left steps
                                i -= 1
                                If toCheck.Map(1, i) <> "."c Then Exit While 'something blocks the way
                                If i = 3 OrElse i = 5 OrElse i = 7 OrElse i = 9 Then Continue While
                                Dim newstate = New Day23_State(toCheck)
                                newstate.Map(1, i) = "B"c
                                newstate.Map(movable.X, movable.Y) = "."c
                                newstate.Steps += 10 * (movable.X - 1 + Math.Abs(i - movable.Y))
                                Dim newstatehash = newstate.ToString
                                If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                DrawState(newstate)
                                toCheckList.Add(newstate)
                            End While
                        Case "C"c
                            If movable.Y = 7 Then 'it might be already in good position
                                If movable.X = 3 OrElse toCheck.Map(3, 7) = "C"c Then Continue For
                            End If
                            If toCheck.Map(1, movable.Y) <> "."c Then Continue For 'something at the exit
                            Dim i As Integer = movable.Y
                            While i < 11 'right steps
                                i += 1
                                If toCheck.Map(1, i) <> "."c Then Exit While 'something blocks the way
                                If i = 3 OrElse i = 5 OrElse i = 7 OrElse i = 9 Then Continue While
                                Dim newstate = New Day23_State(toCheck)
                                newstate.Map(1, i) = "C"c
                                newstate.Map(movable.X, movable.Y) = "."c
                                newstate.Steps += 100 * (movable.X - 1 + Math.Abs(i - movable.Y))
                                Dim newstatehash = newstate.ToString
                                If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                DrawState(newstate)
                                toCheckList.Add(newstate)
                            End While
                            i = movable.Y
                            While i > 1 'left steps
                                i -= 1
                                If toCheck.Map(1, i) <> "."c Then Exit While 'something blocks the way
                                If i = 3 OrElse i = 5 OrElse i = 7 OrElse i = 9 Then Continue While
                                Dim newstate = New Day23_State(toCheck)
                                newstate.Map(1, i) = "C"c
                                newstate.Map(movable.X, movable.Y) = "."c
                                newstate.Steps += 100 * (movable.X - 1 + Math.Abs(i - movable.Y))
                                Dim newstatehash = newstate.ToString
                                If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                DrawState(newstate)
                                toCheckList.Add(newstate)
                            End While
                        Case "D"c
                            If movable.Y = 9 Then 'it might be already in good position
                                If movable.X = 3 OrElse toCheck.Map(3, 9) = "D"c Then Continue For
                            End If
                            If toCheck.Map(1, movable.Y) <> "."c Then Continue For 'something at the exit
                            Dim i As Integer = movable.Y
                            While i < 11 'right steps
                                i += 1
                                If toCheck.Map(1, i) <> "."c Then Exit While 'something blocks the way
                                If i = 3 OrElse i = 5 OrElse i = 7 OrElse i = 9 Then Continue While
                                Dim newstate = New Day23_State(toCheck)
                                newstate.Map(1, i) = "D"c
                                newstate.Map(movable.X, movable.Y) = "."c
                                newstate.Steps += 1000 * (movable.X - 1 + Math.Abs(i - movable.Y))
                                Dim newstatehash = newstate.ToString
                                If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                DrawState(newstate)
                                toCheckList.Add(newstate)
                            End While
                            i = movable.Y
                            While i > 1 'left steps
                                i -= 1
                                If toCheck.Map(1, i) <> "."c Then Exit While 'something blocks the way
                                If i = 3 OrElse i = 5 OrElse i = 7 OrElse i = 9 Then Continue While
                                Dim newstate = New Day23_State(toCheck)
                                newstate.Map(1, i) = "D"c
                                newstate.Map(movable.X, movable.Y) = "."c
                                newstate.Steps += 1000 * (movable.X - 1 + Math.Abs(i - movable.Y))
                                Dim newstatehash = newstate.ToString
                                If cache.ContainsKey(newstatehash) AndAlso cache(newstatehash) <= newstate.Steps Then Continue For
                                DrawState(newstate)
                                toCheckList.Add(newstate)
                            End While
                    End Select
                End If
            Next
        End While


        Return part1result
    End Function

    Function Day23_Part2(input As Day23_State, result As Day23_State) As Integer
        Return 0
    End Function

    Public Sub DrawState(input As Day23_State)
        'For i = 0 To 4
        '    Dim s As String = ""
        '    For j = 0 To 12
        '        s &= input.Map(i, j)
        '    Next
        '    Console.WriteLine(s)
        'Next
        'Console.WriteLine("Score: " & input.Steps)
    End Sub

    Public Sub DrawSteps(input As Day23_State)
        If input.Previous IsNot Nothing Then DrawSteps(input.Previous)
        For i = 0 To 4
            Dim s As String = ""
            For j = 0 To 12
                s &= input.Map(i, j)
            Next
            Console.WriteLine(s)
        Next
        Console.WriteLine("Score: " & input.Steps)
    End Sub
End Module
