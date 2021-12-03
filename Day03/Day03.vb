Module Day03

    Sub Day03_main()

        Dim testinput = Day03_ReadInput("Day03\Day03_test01.txt")
        Dim input = Day03_ReadInput("Day03\Day03_input.txt")

        'part1
        Debug.Assert(Day03_Part1(testinput) = 198)
        Console.WriteLine("Day03 Part1: " & Day03_Part1(input))

        'part2
        Debug.Assert(Day03_Part2(testinput) = 230)
        Console.WriteLine("Day03 Part2: " & Day03_Part2(input))

    End Sub
    Function Day03_ReadInput(inputpath As String) As List(Of List(Of Char))
        Dim input As New List(Of List(Of Char))

        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            Dim line = sr.ReadLine
            input.Add(line.ToString.ToList)
        End While

        Return input
    End Function


    Function Day03_Part1(input As List(Of List(Of Char))) As Integer
        Dim gammastring As String = ""
        Dim epsilonstring As String = ""
        For i = 0 To input(0).Count - 1
            Dim count As Integer = 0
            For Each inp In input
                If inp(i) = "1"c Then count += 1
            Next
            If count > (input.Count - count) Then
                gammastring += "1"c
                epsilonstring += "0"c
            Else
                gammastring += "0"c
                epsilonstring += "1"c
            End If
        Next

        Dim gamma = Convert.ToInt32(gammastring.ToString, 2)
        Dim epsilon = Convert.ToInt32(epsilonstring.ToString, 2)

        Return gamma * epsilon
    End Function

    Function Day03_Part2(input As List(Of List(Of Char))) As Integer
        Dim OxyString As String = ""
        Dim COString As String = ""

        Dim Oxysubinput As New List(Of List(Of Char))(input)
        Dim i As Integer = 0
        Do
            Dim count As Integer = 0
            For Each inp In Oxysubinput
                If inp(i) = "1"c Then count += 1
            Next
            If count >= (Oxysubinput.Count - count) Then
                Oxysubinput = Oxysubinput.FindAll(Function(f) f(i) = "1"c)
            Else
                Oxysubinput = Oxysubinput.FindAll(Function(f) f(i) = "0"c)
            End If
            i = i + 1
        Loop While Oxysubinput.Count > 1
        OxyString = String.Concat(Oxysubinput(0))

        Dim Cosubinput As New List(Of List(Of Char))(input)
        i = 0
        Do
            Dim count As Integer = 0
            For Each inp In Cosubinput
                If inp(i) = "1"c Then count += 1
            Next
            If count >= (Cosubinput.Count - count) Then
                Cosubinput = Cosubinput.FindAll(Function(f) f(i) = "0"c)
            Else
                Cosubinput = Cosubinput.FindAll(Function(f) f(i) = "1"c)
            End If
            i = i + 1
        Loop While Cosubinput.Count > 1
        COString = String.Concat(Cosubinput(0))

        Dim oxy = Convert.ToInt32(OxyString.ToString, 2)
        Dim co = Convert.ToInt32(COString.ToString, 2)

        Return oxy * co
    End Function
End Module
