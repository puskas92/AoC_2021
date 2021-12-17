Module Day17
    Sub Day17_main()
        Dim testinput = New Day17_Input("target area: x=20..30, y=-10..-5")
        Dim input = New Day17_Input("target area: x=192..251, y=-89..-59")

        'part1
        Debug.Assert(Day17_Part12(testinput).Equals(New Tuple(Of Integer, Integer)(45, 112)))
        Dim result = Day17_Part12(input)
        Console.WriteLine("Day17 Part1: " & result.Item1)

        'part2
        Console.WriteLine("Day17 Part2: " & result.Item2)

    End Sub

    Class Day17_Input
        Public X_min As Integer
        Public X_max As Integer
        Public Y_min As Integer
        Public Y_max As Integer

        Public Sub New(s As String)
            X_min = Convert.ToInt32(s.Split("=")(1).Split(".")(0).Trim)
            X_max = Convert.ToInt32(s.Split(".")(2).Split(",")(0).Trim)
            Y_min = Convert.ToInt32(s.Split("=")(2).Split(".")(0).Trim)
            Y_max = Convert.ToInt32(s.Split(".")(4).Trim)
        End Sub
    End Class

    Function Day17_Part12(input As Day17_Input) As Tuple(Of Integer, Integer)
        Dim t As Integer = 0
        Dim Vx_min As Double
        Dim Vx_max As Double
        Dim Vy_min As Double
        Dim Vy_max As Double
        Dim Vx As Integer
        Dim Vy As Integer
        Dim t_sim As Integer
        Dim PosX As Integer
        Dim PosY As Integer
        Dim hit As Boolean

        Dim MaxY As Integer = Integer.MinValue

        Dim shoots As New List(Of Tuple(Of Integer, Integer))
        For t = 1 To 1000 'hopefully big enough

            Dim new_Vx_min = (2 * input.X_min + t * t - t) / (2 * t)
            Dim new_Vx_max = (2 * input.X_max + t * t - t) / (2 * t)
            If t < new_Vx_max Then
                Vx_min = new_Vx_min
                Vx_max = new_Vx_max
            End If


            Vy_min = (2 * input.Y_min + t * t - t) / (2 * t)
            Vy_max = (2 * input.Y_max + t * t - t) / (2 * t)

            For Vx = Math.Ceiling(Vx_min) To Math.Floor(Vx_max)
                For Vy = Math.Ceiling(Vy_min) To Math.Floor(Vy_max)
                    Dim shoot As New Tuple(Of Integer, Integer)(Vx, Vy)
                    If shoots.Contains(shoot) Then
                        Continue For
                    Else
                        shoots.Add(shoot)
                    End If

                    hit = False
                    Dim localMaxY = Integer.MinValue
                    PosX = 0
                    PosY = 0
                    For t_sim = 1 To t + 1
                        If t_sim < Vx Then
                            PosX = PosX + Vx - (t_sim - 1)
                        End If
                        PosY = PosY + Vy - (t_sim - 1)
                        If PosY > localMaxY Then localMaxY = PosY
                        If PosX >= input.X_min AndAlso PosX <= input.X_max AndAlso PosY >= input.Y_min AndAlso PosY <= input.Y_max Then
                            hit = True
                            If localMaxY > MaxY Then MaxY = localMaxY
                            Exit For
                        End If
                    Next
                Next
            Next
        Next t

        Return New Tuple(Of Integer, Integer)(MaxY, shoots.Count)
    End Function
End Module
