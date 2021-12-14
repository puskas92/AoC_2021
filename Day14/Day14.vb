Module Day14
    Sub Day14_main()
        Dim testinput = New Day14_Polymer("Day14\Day14_test01.txt")
        Dim input = New Day14_Polymer("Day14\Day14_input.txt")

        'part1
        Debug.Assert(Day14_Part1(testinput) = 1588)
        Console.WriteLine("Day14 Part1: " & Day14_Part1(input))

        'part2
        testinput = New Day14_Polymer("Day14\Day14_test01.txt")
        input = New Day14_Polymer("Day14\Day14_input.txt")
        Debug.Assert(Day14_Part2(testinput) = 2188189693529)
        Console.WriteLine("Day14 Part2: " & Day14_Part2(input))

    End Sub

    Class Day14_Polymer
        Public Template As String
        Public PairInsertion As Dictionary(Of String, Char)
        Public Step1 As Integer
        Public Letters As List(Of Char)

        Public Sub New(inputpath As String)
            PairInsertion = New Dictionary(Of String, Char)
            Letters = New List(Of Char)
            Step1 = 0
            Dim sr As New IO.StreamReader(inputpath)
            Template = sr.ReadLine.Trim
            sr.ReadLine()

            While (Not sr.EndOfStream)
                Dim line = sr.ReadLine
                PairInsertion.Add(line.Split("-")(0).Trim, line.Split(">")(1).Trim.First)

                If Letters.Contains(line.Split("-")(0).Trim.First) = False Then Letters.Add(line.Split("-")(0).Trim.First)
                If Letters.Contains(line.Split("-")(0).Trim.Last) = False Then Letters.Add(line.Split("-")(0).Trim.Last)
                If Letters.Contains(line.Split(">")(1).Trim.First) = False Then Letters.Add(line.Split(">")(1).Trim.First)
            End While
            sr.Close()
        End Sub

        Public Sub MakeStep()
            Step1 += 1
            Dim newTemplate As String = ""

            newTemplate &= Template(0)
            For i = 0 To Template.Length - 2
                Dim search As String = Template(i) & Template(i + 1)
                If PairInsertion.ContainsKey(search) Then
                    newTemplate &= PairInsertion(search) & Template(i + 1)
                Else
                    newTemplate &= Template(i + 1)
                End If
            Next

            Template = newTemplate
        End Sub

        Public Function MakeStepOnTemplate(template As String) As String

            Dim newTemplate As String = ""

            newTemplate &= template(0)
            For i = 0 To template.Length - 2
                Dim search As String = template(i) & template(i + 1)
                If PairInsertion.ContainsKey(search) Then
                    newTemplate &= PairInsertion(search) & template(i + 1)
                Else
                    newTemplate &= template(i + 1)
                End If
            Next

            Return newTemplate
        End Function

        Public Sub MakeToStep(i As Integer)
            If i < Step1 Then Throw New Exception
            For j = 1 To (i - Step1)
                MakeStep()
            Next
        End Sub


        Public ReadOnly Property Value As Int64
            Get
                Dim min As Int64 = Integer.MaxValue
                Dim max As Int64 = Integer.MinValue

                For Each char1 In Letters
                    Dim count = Template.LongCount(Function(f) f = char1)
                    If count > 0 Then
                        If count > max Then max = count
                        If count < min Then min = count
                    End If
                Next
                Return max - min
            End Get
        End Property
    End Class


    Function Day14_Part1(input As Day14_Polymer) As Integer
        input.MakeToStep(10)
        Return input.Value
    End Function

    Function Day14_Part2(input As Day14_Polymer) As Int64
        Dim PairMatrix As New Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of Char, Int64)))
        For Each pair In input.PairInsertion
            PairMatrix.Add(pair.Key, New Dictionary(Of Integer, Dictionary(Of Char, Int64)))
            PairMatrix(pair.Key).Add(1, New Dictionary(Of Char, Long))
            For Each c In input.Letters
                PairMatrix(pair.Key)(1).Add(c, If(input.PairInsertion(pair.Key) = c, 1, 0))
            Next
        Next

        'Dim result = MakeNStepWithInput(input.Template, 2, PairMatrix, input)

        Dim result As New Dictionary(Of Char, Int64)
        For Each c In input.Letters
            result.Add(c, 0)
        Next


        For i = 0 To input.Template.Length - 2
            Dim search As String = input.Template.Substring(i, 2)
            Dim subresult = MakeNStepWithInput(search, 40, PairMatrix, input)

            For Each c In input.Letters
                result(c) += subresult(c)
            Next
        Next

        For Each c In input.Template
            result(c) += 1
        Next

        Dim min As Int64 = result.Min(Function(f) f.Value)
        Dim max As Int64 = result.Max(Function(f) f.Value)
        Return max - min
    End Function

    Public Function MakeNStepWithInput(template As String, Step1 As Integer, PairMatrix As Dictionary(Of String, Dictionary(Of Integer, Dictionary(Of Char, Int64))), input As Day14_Polymer) As Dictionary(Of Char, Int64)
        If PairMatrix(template).ContainsKey(Step1) Then
            Return PairMatrix(template)(Step1)
        End If
        Dim result As New Dictionary(Of Char, Int64)
        For Each c In input.Letters
            result.Add(c, 0)
        Next

        Dim newtemplate = template.First & input.PairInsertion(template) & template.Last
        For i = 0 To newtemplate.Length - 2
            Dim search As String = newtemplate.Substring(i, 2)

            Dim subresult

            If PairMatrix.ContainsKey(search) AndAlso PairMatrix(search).ContainsKey(Step1 - 1) Then
                subresult = PairMatrix(search)(Step1 - 1)
            Else
                subresult = MakeNStepWithInput(search, Step1 - 1, PairMatrix, input)
            End If

            For Each c In input.Letters
                result(c) += subresult(c)
            Next
        Next

        result(input.PairInsertion(template)) += 1

        PairMatrix(template).Add(Step1, result)

        Return result
    End Function
End Module
