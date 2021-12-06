Module Day02
    Sub Day02_main()
        Dim testinput = Day02_ReadInput("Day02\Day02_test01.txt")
        Dim input = Day02_ReadInput("Day02\Day02_input.txt")
        'part1
        Debug.Assert(Day02_Part1(testinput) = 150)
        Console.WriteLine("Day02 Part1: " & Day02_Part1(input))

        'part2
        Debug.Assert(Day02_Part2(testinput) = 900)
        Console.WriteLine("Day02 Part2: " & Day02_Part2(input))

    End Sub
    Function Day02_ReadInput(inputpath As String) As List(Of Tuple(Of String, Integer))
        Dim result As New List(Of Tuple(Of String, Integer))
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            Dim line = sr.ReadLine
            Dim t As New Tuple(Of String, Integer)(line.Split(" ")(0), Convert.ToInt32(line.Split(" ")(1)))
            result.Add(t)
        End While
        sr.Close()
        Return result
    End Function


    Function Day02_Part1(input As List(Of Tuple(Of String, Integer))) As Integer
        Dim position As New Drawing.Point(0, 0) 'H, V

        For Each command In input
            Select Case command.Item1
                Case "forward"
                    position.X += command.Item2
                Case "up"
                    position.Y -= command.Item2
                Case "down"
                    position.Y += command.Item2
            End Select
        Next

        Return position.X * position.Y
    End Function

    Function Day02_Part2(input As List(Of Tuple(Of String, Integer))) As Integer
        Dim position As New Drawing.Point(0, 0) 'H, V
        Dim aim As Integer = 0

        For Each command In input
            Select Case command.Item1
                Case "forward"
                    position.X += command.Item2
                    position.Y += (aim * command.Item2)
                Case "up"
                    aim -= command.Item2
                Case "down"
                    aim += command.Item2
            End Select
        Next

        Return position.X * position.Y
    End Function
End Module
