Module Day11
    Sub Day11_main()
        Dim testinput = Day11_ReadInput("Day11\Day11_test01.txt")
        Dim testinput2 = Day11_ReadInput("Day11\Day11_test02.txt")
        Dim input = Day11_ReadInput("Day11\Day11_input.txt")

        'part1
        Debug.Assert(Day11_Part1(testinput2, 2) = 9)
        Debug.Assert(Day11_Part1(testinput, 10) = 204)
        testinput = Day11_ReadInput("Day11\Day11_test01.txt")
        Debug.Assert(Day11_Part1(testinput, 100) = 1656)
        Console.WriteLine("Day11 Part1: " & Day11_Part1(input, 100))

        'part2
        Debug.Assert(Day11_Part2(testinput, 100) = 195)
        Console.WriteLine("Day11 Part2: " & Day11_Part2(input, 100))

    End Sub
    Function Day11_ReadInput(inputpath As String) As Byte(,)
        Dim t As New List(Of String)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            t.Add(sr.ReadLine)
        End While
        sr.Close()

        Dim output(t.Count - 1, t.Count - 1) As Byte
        For i = 0 To t.Count - 1
            For j = 0 To t.Count - 1
                output(i, j) = Convert.ToByte(t(i)(j).ToString)
            Next
        Next

        Return output
    End Function


    Function Day11_Part1(input As Byte(,), steps As Integer) As Integer
        Dim flashes As Integer = 0

        Dim directions As New List(Of Drawing.Point)
        directions.Add(New Drawing.Point(-1, -1))
        directions.Add(New Drawing.Point(-1, 0))
        directions.Add(New Drawing.Point(-1, 1))
        directions.Add(New Drawing.Point(0, -1))
        directions.Add(New Drawing.Point(0, 1))
        directions.Add(New Drawing.Point(1, -1))
        directions.Add(New Drawing.Point(1, 0))
        directions.Add(New Drawing.Point(1, 1))

        For i = 1 To steps
            'add one
            For j = 0 To input.GetUpperBound(0)
                For k = 0 To input.GetUpperBound(1)
                    input(j, k) += 1
                Next
            Next

            'flashes
            Dim flashed As Boolean = False
            Do
                flashed = False
                For j = 0 To input.GetUpperBound(0)
                    For k = 0 To input.GetUpperBound(1)
                        If input(j, k) > 9 Then
                            flashes += 1
                            flashed = True
                            input(j, k) = 0
                            For Each dir1 In directions
                                Dim x = j + dir1.X
                                Dim y = k + dir1.Y
                                If x >= 0 AndAlso x <= input.GetUpperBound(0) AndAlso y >= 0 AndAlso y <= input.GetUpperBound(1) AndAlso input(x, y) > 0 Then input(x, y) += 1
                            Next
                        End If
                    Next
                Next
            Loop While flashed

            'Console.WriteLine("Step: " & i)
            'Day11_PrintToConsole(input)
        Next
        Return flashes
    End Function

    Function Day11_Part2(input As Byte(,), steps As Integer) As Integer


        Dim directions As New List(Of Drawing.Point)
        directions.Add(New Drawing.Point(-1, -1))
        directions.Add(New Drawing.Point(-1, 0))
        directions.Add(New Drawing.Point(-1, 1))
        directions.Add(New Drawing.Point(0, -1))
        directions.Add(New Drawing.Point(0, 1))
        directions.Add(New Drawing.Point(1, -1))
        directions.Add(New Drawing.Point(1, 0))
        directions.Add(New Drawing.Point(1, 1))

        Dim allflashed As Boolean
        Do
            allflashed = False

            Dim flashes As Integer = 0

            steps += 1

            'add one
            For j = 0 To input.GetUpperBound(0)
                For k = 0 To input.GetUpperBound(1)
                    input(j, k) += 1
                Next
            Next

            'flashes
            Dim flashed As Boolean = False
            Do
                flashed = False
                For j = 0 To input.GetUpperBound(0)
                    For k = 0 To input.GetUpperBound(1)
                        If input(j, k) > 9 Then
                            flashes += 1
                            flashed = True
                            input(j, k) = 0
                            For Each dir1 In directions
                                Dim x = j + dir1.X
                                Dim y = k + dir1.Y
                                If x >= 0 AndAlso x <= input.GetUpperBound(0) AndAlso y >= 0 AndAlso y <= input.GetUpperBound(1) AndAlso input(x, y) > 0 Then input(x, y) += 1
                            Next
                        End If
                    Next
                Next
            Loop While flashed

            allflashed = (flashes = (input.GetUpperBound(0) + 1) * (input.GetUpperBound(1) + 1))
            'Console.WriteLine("Step: " & i)
            'Day11_PrintToConsole(input)
        Loop Until allflashed

        Return steps
    End Function

    Sub Day11_PrintToConsole(input As Byte(,))
        Dim s As String = ""
        For j = 0 To input.GetUpperBound(0)
            For k = 0 To input.GetUpperBound(1)
                s &= input(j, k)
            Next
            s &= vbCrLf
        Next
        Console.WriteLine(s)
    End Sub
End Module
