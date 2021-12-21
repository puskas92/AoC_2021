Module Day21

    Public Sub Day21_main()
        Dim input As New Tuple(Of Byte, Byte)(5, 6)
        'part1
        Debug.Assert(Day21_Part1(4, 8) = 739785)
        Console.WriteLine("Day21 Part1: " & Day21_Part1(input.Item1, input.Item2))

        'part2
        Debug.Assert(Day21_Part2(4, 8) = 444356092776315)
        Console.WriteLine("Day21 Part2: " & Day21_Part2(input.Item1, input.Item2))

    End Sub


    Function Day21_Part1(P1start As Integer, P2start As Integer) As Integer
        Dim dice As Integer = 0
        Dim player As Integer = 0
        Dim P1Point As Integer = 0
        Dim P2Point As Integer = 0
        Dim P1Position As Integer = P1start
        Dim P2Position As Integer = P2start

        Dim diceroll As Integer = 0
        While True
            Dim point As Integer = 0
            For i = 1 To 3
                diceroll += 1
                dice += 1
                If dice = 101 Then dice = 1
                point += dice
            Next
            If player = 0 Then
                P1Position += point
                P1Position = P1Position Mod 10
                P1Point += If(P1Position = 0, 10, P1Position)
                If P1Point >= 1000 Then Exit While
            Else
                P2Position += point
                P2Position = P2Position Mod 10
                P2Point += If(P2Position = 0, 10, P2Position)
                If P2Point >= 1000 Then Exit While
            End If

            player += 1
            player = player Mod 2
        End While


        Dim result = diceroll * If(player = 0, P2Point, P1Point)
        Return result
    End Function

    Function Day21_Part2(P1start As Integer, P2start As Integer) As Int64
        Dim TimesWinCache As New Dictionary(Of Tuple(Of Byte, Byte, Byte, Byte, Byte), Tuple(Of UInt64, UInt64))

        Dim result = TimesWin(P1start, P2start, 0, 0, 0, TimesWinCache)
        Return Math.Max(result.Item1, result.Item2)
    End Function

    Private Function TimesWin(P1Position As Byte, P2Position As Byte, PlayerTurn As Byte, P1Point As Byte, P2Point As Byte, timesWinCache As Dictionary(Of Tuple(Of Byte, Byte, Byte, Byte, Byte), Tuple(Of UInt64, UInt64))) As Tuple(Of UInt64, UInt64)
        Dim P1Wins As UInt64 = 0
        Dim P2Wins As UInt64 = 0

        Dim key As Tuple(Of Byte, Byte, Byte, Byte, Byte)
        Dim key_reverse As Tuple(Of Byte, Byte, Byte, Byte, Byte)

        For i = 1 To 3
            For j = 1 To 3
                For k = 1 To 3
                    Dim point = i + j + k
                    If PlayerTurn = 0 Then
                        Dim newP1Position = P1Position + point
                        newP1Position = newP1Position Mod 10
                        Dim newP1Point = P1Point + If(newP1Position = 0, 10, newP1Position)
                        If newP1Point >= 21 Then
                            P1Wins += 1
                        Else
                            Dim subresult As Tuple(Of UInt64, UInt64)
                            key = New Tuple(Of Byte, Byte, Byte, Byte, Byte)(newP1Position, P2Position, 1, newP1Point, P2Point)
                            key_reverse = New Tuple(Of Byte, Byte, Byte, Byte, Byte)(P2Position, newP1Position, 0, P2Point, newP1Point)
                            If timesWinCache.ContainsKey(key) Then
                                subresult = timesWinCache(key)
                            ElseIf timesWinCache.ContainsKey(key_reverse) Then 'the problem is symmetric, so if the cache contains the reverse status, it can be used
                                Dim subsubresult = timesWinCache(key_reverse)
                                subresult = New Tuple(Of UInt64, UInt64)(subsubresult.Item2, subsubresult.Item1)
                            Else
                                subresult = TimesWin(newP1Position, P2Position, 1, newP1Point, P2Point, timesWinCache)
                            End If

                            P1Wins += subresult.Item1
                            P2Wins += subresult.Item2
                        End If
                    Else
                        Dim newP2Position = P2Position + point
                        newP2Position = newP2Position Mod 10
                        Dim newP2Point = P2Point + If(newP2Position = 0, 10, newP2Position)
                        If newP2Point >= 21 Then
                            P2Wins += 1
                        Else
                            Dim subresult As Tuple(Of UInt64, UInt64)
                            key = New Tuple(Of Byte, Byte, Byte, Byte, Byte)(P1Position, newP2Position, 0, P1Point, newP2Point)
                            key_reverse = New Tuple(Of Byte, Byte, Byte, Byte, Byte)(newP2Position, P1Position, 1, newP2Point, P1Point)
                            If timesWinCache.ContainsKey(key) Then
                                subresult = timesWinCache(key)
                            ElseIf timesWinCache.ContainsKey(key_reverse) Then 'the problem is symmetric, so if the cache contains the reverse status, it can be used
                                Dim subsubresult = timesWinCache(key_reverse)
                                subresult = New Tuple(Of UInt64, UInt64)(subsubresult.Item2, subsubresult.Item1)
                            Else
                                subresult = TimesWin(P1Position, newP2Position, 0, P1Point, newP2Point, timesWinCache)
                            End If

                            P1Wins += subresult.Item1
                            P2Wins += subresult.Item2
                        End If
                    End If
                Next
            Next
        Next

        Dim result As New Tuple(Of UInt64, UInt64)(P1Wins, P2Wins)
        key = New Tuple(Of Byte, Byte, Byte, Byte, Byte)(P1Position, P2Position, PlayerTurn, P1Point, P2Point)
        If timesWinCache.ContainsKey(key) = False Then timesWinCache.Add(key, result)
        Return result
    End Function
End Module
