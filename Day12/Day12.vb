Module Day12
    Sub Day12_main()
        Dim input = Day12_ReadInput("Day12\Day12_input.txt")

        'part1
        Debug.Assert(Day12_Part1(Day12_ReadInput("Day12\Day12_test01.txt")) = 10)
        Debug.Assert(Day12_Part1(Day12_ReadInput("Day12\Day12_test02.txt")) = 19)
        Debug.Assert(Day12_Part1(Day12_ReadInput("Day12\Day12_test03.txt")) = 226)
        Console.WriteLine("Day12 Part1: " & Day12_Part1(input))

        'part2
        Debug.Assert(Day12_Part2(Day12_ReadInput("Day12\Day12_test01.txt")) = 36)
        Debug.Assert(Day12_Part2(Day12_ReadInput("Day12\Day12_test02.txt")) = 103)
        Debug.Assert(Day12_Part2(Day12_ReadInput("Day12\Day12_test03.txt")) = 3509)
        Console.WriteLine("Day12 Part2: " & Day12_Part2(input))

    End Sub
    Function Day12_ReadInput(inputpath As String) As Dictionary(Of String, List(Of String))
        Dim result As New Dictionary(Of String, List(Of String))
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            Dim pairs = sr.ReadLine.Split("-")
            If result.ContainsKey(pairs(0).Trim) = False Then result.Add(pairs(0).Trim, New List(Of String))
            If result.ContainsKey(pairs(1).Trim) = False Then result.Add(pairs(1).Trim, New List(Of String))
            result(pairs(0)).Add(pairs(1))
            result(pairs(1)).Add(pairs(0))
        End While
        For Each row In result
            row.Value.Sort()
        Next
        sr.Close()
        Return result
    End Function


    Function Day12_Part1(input As Dictionary(Of String, List(Of String))) As Integer
        Dim Seen As New List(Of String)
        Dim Routes As List(Of List(Of String)) = Day12_IterateThroughCaves1(input, "start", Seen)
        Return Routes.Count
    End Function

    Private Function Day12_IterateThroughCaves1(input As Dictionary(Of String, List(Of String)), CurrentPosition As String, seen As List(Of String)) As List(Of List(Of String))
        Dim newSeen As New List(Of String)(seen)
        newSeen.Add(CurrentPosition)

        Dim result As New List(Of List(Of String))

        If CurrentPosition = "end" Then
            result.Add(newSeen)
        Else
            For Each dest In input(CurrentPosition)
                If dest.ToLower = dest AndAlso seen.Contains(dest) Then Continue For

                result.AddRange(Day12_IterateThroughCaves1(input, dest, newSeen))
            Next
        End If
        Return result
    End Function

    Function Day12_Part2(input As Dictionary(Of String, List(Of String))) As Integer
        Dim Seen As New List(Of String)
        Dim Routes As List(Of List(Of String)) = Day12_IterateThroughCaves2(input, "start", Seen, "")
        'For Each row In Routes
        '    Console.WriteLine(String.Join(",", row))
        'Next
        Return Routes.Count
    End Function

    Private Function Day12_IterateThroughCaves2(input As Dictionary(Of String, List(Of String)), CurrentPosition As String, seen As List(Of String), JokerSmallCave As String) As List(Of List(Of String))
        Dim newSeen As New List(Of String)(seen)
        newSeen.Add(CurrentPosition)

        Dim result As New List(Of List(Of String))
        Dim newJoker As String


        If CurrentPosition = "end" Then
            result.Add(newSeen)
        Else
            For Each dest In input(CurrentPosition)
                newJoker = JokerSmallCave
                If dest.ToLower = dest AndAlso seen.Contains(dest) Then
                    If dest <> "start" AndAlso dest <> "end" AndAlso JokerSmallCave = "" Then
                        newJoker = dest
                    Else
                        Continue For
                    End If
                End If

                result.AddRange(Day12_IterateThroughCaves2(input, dest, newSeen, newJoker))
            Next
        End If
        Return result
    End Function
End Module
