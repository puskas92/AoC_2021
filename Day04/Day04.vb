Module Day04
    Sub Day04_main()
        Dim input = Day04_ReadInput("Day04\Day04_input.txt")

        'part1
        Debug.Assert(Day04_Part1(Day04_ReadInput("Day04\Day04_test01.txt")) = 4512)
        Console.WriteLine("Day04 Part1: " & Day04_Part1(input))

        'part2
        Debug.Assert(Day04_Part2(Day04_ReadInput("Day04\Day04_test01.txt")) = 1924)
        Console.WriteLine("Day04 Part2: " & Day04_Part2(input))

    End Sub
    Function Day04_ReadInput(inputpath As String) As Day04_BingoObject
        '7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1
        '
        '22 13 17 11  0
        '8  2 23  4 24
        '21  9 14 16  7
        '6 10  3 18  5
        '1 12 20 15 19
        Dim output As New Day04_BingoObject

        Dim sr As New IO.StreamReader(inputpath)
        Dim line = sr.ReadLine
        output.RandomNumbers = line.Split(",").ToList.ConvertAll(Function(f) Convert.ToInt32(f))
        output.Boards = New List(Of List(Of List(Of Tuple(Of Integer, Boolean))))
        While (Not sr.EndOfStream)
            Dim board As New List(Of List(Of Tuple(Of Integer, Boolean)))
            sr.ReadLine()

            For i = 0 To 4
                board.Add(New List(Of Tuple(Of Integer, Boolean)))
                For Each s In sr.ReadLine.Split(" ")
                    If s = "" Then Continue For
                    Dim t As New Tuple(Of Integer, Boolean)(Convert.ToInt32(s), False)
                    board.Last.Add(t)
                Next
            Next

            output.Boards.Add(board)
        End While
        sr.Close()
        Return output
    End Function

    Class Day04_BingoObject
        Public RandomNumbers As List(Of Integer)
        Public Boards As List(Of List(Of List(Of Tuple(Of Integer, Boolean))))
    End Class

    Function Day04_Part1(input As Day04_BingoObject) As Integer
        Dim winnerBoard As Integer = 0
        Dim result As Integer = 0
        For Each num In input.RandomNumbers
            'check numbers
            For Each board In input.Boards
                For i = 0 To 4
                    For j = 0 To 4
                        If board(i)(j).Item1 = num Then
                            board(i)(j) = New Tuple(Of Integer, Boolean)(num, True)
                        End If
                    Next
                Next
            Next

            'check if bingo
            Dim bingo As Boolean = True
            For Each board In input.Boards
                For i = 0 To 4
                    bingo = True
                    For j = 0 To 4
                        If board(i)(j).Item2 = False Then
                            bingo = False
                            Exit For
                        End If
                    Next
                    If bingo = True Then Exit For
                Next
                If bingo = True Then
                    winnerBoard = input.Boards.IndexOf(board)
                    Exit For
                End If

                For i = 0 To 4
                    bingo = True
                    For j = 0 To 4
                        If board(j)(i).Item2 = False Then
                            bingo = False
                            Exit For
                        End If
                    Next
                    If bingo = True Then Exit For
                Next
                If bingo = True Then
                    winnerBoard = input.Boards.IndexOf(board)
                    Exit For
                End If
            Next

            If bingo = True Then
                Dim sum = input.Boards(winnerBoard).Sum(Function(f) f.Sum(Function(g) If(g.Item2, 0, g.Item1)))
                result = num * sum
                Exit For
            End If
        Next

        Return result
    End Function

    Function Day04_Part2(input As Day04_BingoObject) As Integer
        'resetboards
        For Each board In input.Boards
            For i = 0 To 4
                For j = 0 To 4
                    board(i)(j) = New Tuple(Of Integer, Boolean)(board(i)(j).Item1, False)
                Next
            Next
        Next

        Dim winnerBoards As New List(Of List(Of List(Of Tuple(Of Integer, Boolean))))
        Dim result As Integer = 0
        For Each num In input.RandomNumbers
            'check numbers
            For Each board In input.Boards
                For i = 0 To 4
                    For j = 0 To 4
                        If board(i)(j).Item1 = num Then
                            board(i)(j) = New Tuple(Of Integer, Boolean)(num, True)
                        End If
                    Next
                Next
            Next

            'check if bingo
            Dim bingo As Boolean = True
            For Each board In input.Boards
                For i = 0 To 4
                    bingo = True
                    For j = 0 To 4
                        If board(i)(j).Item2 = False Then
                            bingo = False
                            Exit For
                        End If
                    Next
                    If bingo = True Then Exit For
                Next
                If bingo = True Then
                    winnerBoards.Add(board)

                Else
                    For i = 0 To 4
                        bingo = True
                        For j = 0 To 4
                            If board(j)(i).Item2 = False Then
                                bingo = False
                                Exit For
                            End If
                        Next
                        If bingo = True Then Exit For
                    Next
                    If bingo = True Then
                        winnerBoards.Add(board)
                    End If
                End If
            Next

            If winnerBoards.Count > 0 Then
                For Each winboard In winnerBoards
                    If input.Boards.Count = 1 Then
                        Dim sum = input.Boards.First.Sum(Function(f) f.Sum(Function(g) If(g.Item2, 0, g.Item1)))
                        result = num * sum
                        Return result
                        Exit For
                    Else
                        input.Boards.Remove(winboard)
                    End If
                Next
                winnerBoards.Clear()
            End If
            If result > 0 Then Exit For
        Next

        Return result
    End Function
End Module
