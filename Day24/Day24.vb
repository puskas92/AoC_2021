Module Day24
    Sub Day24_main()
        Dim input = Day24_ReadInput("Day24\Day24_input.txt")

        Debug.Assert(Day24_RunProgramWithInput(Day24_ReadInput("Day24\Day24_test01.txt"), {15}.ToList)("w"c) = 1)

        'see the solution in the attached excel
        'part1
        Console.WriteLine("Day24 Part1: " & Day24_Part1(input))

        'part2
        Console.WriteLine("Day24 Part2: " & Day24_Part2(input))

    End Sub
    Function Day24_ReadInput(inputpath As String) As List(Of Day24_ALU_Program_Row)
        Dim result As New List(Of Day24_ALU_Program_Row)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            result.Add(New Day24_ALU_Program_Row(sr.ReadLine))
        End While
        sr.Close()
        Return result
    End Function

    Public Class Day24_ALU_Program_Row
        Public Command As String
        Public Var1 As String
        Public Var2 As String

        Public Sub New(s As String)
            Dim s2 = s.Split(" ")
            Command = s2(0).Trim
            Var1 = s2(1).Trim
            If s2.Length = 3 Then
                Var2 = s2(2).Trim
            Else
                Var2 = ""
            End If
        End Sub
    End Class

    Function Day24_Part1(input As List(Of Day24_ALU_Program_Row)) As Int64
        Dim i = 95299897999897
        Dim subresult = Day24_RunProgramWithInput(input, i.ToString.ToList.ConvertAll(Function(f) Convert.ToInt32(f.ToString)))
        Debug.Assert(subresult("z"c) = 0)
        Return i
    End Function

    Function Day24_Part2(input As List(Of Day24_ALU_Program_Row)) As Int64
        Dim i = 31111121382151
        Dim subresult = Day24_RunProgramWithInput(input, i.ToString.ToList.ConvertAll(Function(f) Convert.ToInt32(f.ToString)))
        Debug.Assert(subresult("z"c) = 0)
        Return i
    End Function

    Function Day24_RunProgramWithInput(program As List(Of Day24_ALU_Program_Row), input As List(Of Integer)) As Dictionary(Of Char, Int64)
        Dim result As New Dictionary(Of Char, Int64) From {{"w"c, 0}, {"x"c, 0}, {"y"c, 0}, {"z"c, 0}}

        Dim inputcount As Integer = 0
        Dim programcount As Integer = 0
        For Each row In program
            programcount += 1
            Debug.Assert(Char.IsLetter(row.Var1.First))
            Select Case row.Command
                Case "inp"
                    result(row.Var1.First) = input(inputcount)
                    inputcount += 1
                Case "add"
                    result(row.Var1.First) += If(Char.IsLetter(row.Var2.First), result(row.Var2.First), Convert.ToInt32(row.Var2))
                Case "mul"
                    result(row.Var1.First) *= If(Char.IsLetter(row.Var2.First), result(row.Var2.First), Convert.ToInt32(row.Var2))
                Case "div"
                    Dim divider = If(Char.IsLetter(row.Var2.First), result(row.Var2.First), Convert.ToInt32(row.Var2))
                    If divider = 0 Then
                        result.Add("e"c, 1)
                        Return result
                        Exit Function
                    End If
                    result(row.Var1.First) = Math.Truncate(result(row.Var1.First) / divider)
                Case "mod"
                    Dim divider = If(Char.IsLetter(row.Var2.First), result(row.Var2.First), Convert.ToInt32(row.Var2))
                    If divider = 0 Then
                        result.Add("e"c, 2)
                        Return result
                        Exit Function
                    End If
                    result(row.Var1.First) = result(row.Var1.First) Mod divider
                Case "eql"
                    result(row.Var1.First) = If(result(row.Var1.First) = If(Char.IsLetter(row.Var2.First), result(row.Var2.First), Convert.ToInt32(row.Var2)), 1, 0)
            End Select
        Next

        Return result
    End Function


End Module
