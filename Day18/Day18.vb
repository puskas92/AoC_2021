Module Day18
    Sub Day18_main()
        'constructor test
        Debug.Assert(New Day18_SFNum("[1,2]").ToString = "[1,2]")
        Debug.Assert(New Day18_SFNum("[[1,2],3]").ToString = "[[1,2],3]")
        Debug.Assert(New Day18_SFNum("[9,[8,7]]").ToString = "[9,[8,7]]")
        Debug.Assert(New Day18_SFNum("[[1,9],[8,5]]").ToString = "[[1,9],[8,5]]")
        Debug.Assert(New Day18_SFNum("[[[[1,2],[3,4]],[[5,6],[7,8]]],9]").ToString = "[[[[1,2],[3,4]],[[5,6],[7,8]]],9]")
        Debug.Assert(New Day18_SFNum("[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]").ToString = "[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]")
        Debug.Assert(New Day18_SFNum("[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]").ToString = "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]")

        'explode test
        Debug.Assert(New Day18_SFNum("[[[[[9,8],1],2],3],4]").ToString = "[[[[0,9],2],3],4]")
        Debug.Assert(New Day18_SFNum("[7,[6,[5,[4,[3,2]]]]]").ToString = "[7,[6,[5,[7,0]]]]")
        Debug.Assert(New Day18_SFNum("[[6,[5,[4,[3,2]]]],1]").ToString = "[[6,[5,[7,0]]],3]")
        Debug.Assert(New Day18_SFNum("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]").ToString = "[[3,[2,[8,0]]],[9,[5,[7,0]]]]")

        'sum test
        Debug.Assert((New Day18_SFNum("[1,2]") + New Day18_SFNum("[[3,4],5]")).ToString = "[[1,2],[[3,4],5]]")

        'reduce test
        Debug.Assert((New Day18_SFNum("[[[[4,3],4],4],[7,[[8,4],9]]]") + New Day18_SFNum("[1,1]")).ToString = "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]")

        'sum test
        Debug.Assert(Day18_InputSum(Day18_ReadInput("Day18\Day18_test01.txt")).ToString = "[[[[1,1],[2,2]],[3,3]],[4,4]]")
        Debug.Assert(Day18_InputSum(Day18_ReadInput("Day18\Day18_test02.txt")).ToString = "[[[[3,0],[5,3]],[4,4]],[5,5]]")
        Debug.Assert(Day18_InputSum(Day18_ReadInput("Day18\Day18_test03.txt")).ToString = "[[[[5,0],[7,4]],[5,5]],[6,6]]")
        Debug.Assert(Day18_InputSum(Day18_ReadInput("Day18\Day18_test04.txt")).ToString = "[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]")
        Debug.Assert(Day18_InputSum(Day18_ReadInput("Day18\Day18_test05.txt")).ToString = "[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]")

        'magnitude test
        Debug.Assert(New Day18_SFNum("[9,1]").Magnitude = 29)
        Debug.Assert(New Day18_SFNum("[1,9]").Magnitude = 21)
        Debug.Assert(New Day18_SFNum("[[9,1],[1,9]]").Magnitude = 129)
        Debug.Assert(New Day18_SFNum("[[1,2],[[3,4],5]]").Magnitude = 143)
        Debug.Assert(New Day18_SFNum("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]").Magnitude = 1384)
        Debug.Assert(New Day18_SFNum("[[[[1,1],[2,2]],[3,3]],[4,4]]").Magnitude = 445)
        Debug.Assert(New Day18_SFNum("[[[[3,0],[5,3]],[4,4]],[5,5]]").Magnitude = 791)
        Debug.Assert(New Day18_SFNum("[[[[5,0],[7,4]],[5,5]],[6,6]]").Magnitude = 1137)
        Debug.Assert(New Day18_SFNum("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]").Magnitude = 3488)

        Dim input = Day18_ReadInput("Day18\Day18_input.txt")

        'part1
        Debug.Assert(Day18_Part1(Day18_ReadInput("Day18\Day18_test05.txt")) = 4140)
        Console.WriteLine("Day18 Part1: " & Day18_Part1(input))

        'part2
        Debug.Assert(Day18_Part2(Day18_ReadInput("Day18\Day18_test05.txt")) = 3993)
        Console.WriteLine("Day18 Part2: " & Day18_Part2(input))

    End Sub
    Function Day18_ReadInput(inputpath As String) As List(Of Day18_SFNum)
        Dim result As New List(Of Day18_SFNum)
        Dim sr As New IO.StreamReader(inputpath)
        While (Not sr.EndOfStream)
            Dim sf_num = New Day18_SFNum(sr.ReadLine)
            sf_num.Reduce()
            result.Add(sf_num)
        End While
        sr.Close()
        Return result
    End Function

    Class Day18_SFNum
        Public LeftNum As Day18_SFNum
        Public LeftReg As Byte
        Public isLeftRegular As Boolean

        Public RightNum As Day18_SFNum
        Public RightReg As Byte
        Public isRightRegular As Boolean

        Public Sub New(s As String, Optional firstlevel As Boolean = True)
            UpdateBasedOnString(s)

            If firstlevel Then Reduce()
        End Sub

        Private Sub UpdateBasedOnString(s As String)
            Dim bracketcount As Integer = 0
            Dim leftstring As String = ""
            Dim rightstring As String = ""
            Dim commafound As Boolean = False
            For Each c In s
                Select Case c
                    Case "["c
                        bracketcount += 1
                        If bracketcount > 1 Then
                            If commafound Then
                                rightstring &= c
                            Else
                                leftstring &= c
                            End If
                        End If
                    Case "]"c
                        bracketcount -= 1
                        If bracketcount > 0 Then
                            If commafound Then
                                rightstring &= c
                            Else
                                leftstring &= c
                            End If
                        End If
                    Case ","c
                        If bracketcount = 1 Then
                            commafound = True
                        Else
                            If commafound Then
                                rightstring &= c
                            Else
                                leftstring &= c
                            End If
                        End If
                    Case Else
                        If commafound Then
                            rightstring &= c
                        Else
                            leftstring &= c
                        End If
                End Select
            Next

            If leftstring.Contains("["c) Then
                Left = New Day18_SFNum(leftstring, False)
            Else
                Left = Convert.ToByte(leftstring)
            End If

            If rightstring.Contains("["c) Then
                Right = New Day18_SFNum(rightstring, False)
            Else
                Right = Convert.ToByte(rightstring)
            End If
        End Sub

        Public Sub New(left, right)
            Me.Left = left
            Me.Right = right
            ' Reduce()
        End Sub
        Public Property Left
            Get
                If isLeftRegular Then
                    Return LeftReg
                Else
                    Return LeftNum
                End If
            End Get
            Set(value)
                If value.GetType = GetType(Day18_SFNum) Then
                    isLeftRegular = False
                    LeftReg = 0
                    LeftNum = value
                Else
                    isLeftRegular = True
                    LeftReg = value
                    LeftNum = Nothing
                End If
            End Set
        End Property

        Public Property Right
            Get
                If isRightRegular Then
                    Return RightReg
                Else
                    Return RightNum
                End If
            End Get
            Set(value)
                If value.GetType = GetType(Day18_SFNum) Then
                    isRightRegular = False
                    RightReg = 0
                    RightNum = value
                Else
                    isRightRegular = True
                    RightReg = value
                    RightNum = Nothing
                End If
            End Set
        End Property

        Public Overrides Function ToString() As String
            Dim result As String = "["
            result &= Left.ToString()
            result &= ","
            result &= Right.ToString()
            result &= "]"
            Return result
        End Function

        Public Sub Reduce()
            Dim changed As Boolean = False
            Do
                changed = False
                If Me.Depth >= 4 Then
                    'explode
                    Dim s = Me.ToString

                    Dim bracketcount As Integer = 0
                    Dim leftpoint As Integer = 0
                    Dim rightpoint As Integer = 0
                    For i = 0 To s.Length - 1
                        Dim c = s(i)
                        Select Case c
                            Case "["c
                                bracketcount += 1
                                If bracketcount = 5 Then
                                    leftpoint = i
                                End If
                            Case "]"c
                                If bracketcount = 5 Then
                                    rightpoint = i
                                    Exit For
                                End If
                                bracketcount -= 1
                        End Select
                    Next
                    Dim substring = s.Substring(leftpoint, rightpoint - leftpoint + 1)
                    Dim leftExplodingNum = Convert.ToInt32(substring.Split(",")(0).Trim("["c))
                    Dim rightExplodingNum = Convert.ToInt32(substring.Split(",")(1).Trim("]"c))

                    Dim newstring As String = "0"
                    Dim valuefound As Boolean = False
                    Dim valuestart As Boolean = False
                    Dim value As String = ""
                    For i = leftpoint - 1 To 0 Step -1
                        Dim c = s(i)
                        If valuefound = False Then
                            If Char.IsDigit(c) Then
                                valuestart = True
                                value = c & value
                            Else
                                If valuestart = True Then
                                    newstring = c & (Convert.ToInt32(value) + leftExplodingNum).ToString & newstring
                                    valuefound = True
                                Else
                                    newstring = c & newstring
                                End If
                            End If
                        Else
                            newstring = c & newstring
                        End If
                    Next

                    valuefound = False
                    valuestart = False
                    value = ""
                    For i = rightpoint + 1 To s.Length - 1
                        Dim c = s(i)
                        If valuefound = False Then
                            If Char.IsDigit(c) Then
                                valuestart = True
                                value = value & c
                            Else
                                If valuestart = True Then
                                    newstring = newstring & (Convert.ToInt32(value) + rightExplodingNum).ToString & c
                                    valuefound = True
                                Else
                                    newstring = newstring & c
                                End If
                            End If
                        Else
                            newstring = newstring & c
                        End If
                    Next
                    UpdateBasedOnString(newstring)
                    changed = True
                Else
                    'split
                    changed = Split()
                End If
            Loop While changed
        End Sub

        Private Function Split() As Boolean
            If isLeftRegular Then
                If LeftReg >= 10 Then
                    Left = New Day18_SFNum(Math.Floor(LeftReg / 2), Math.Ceiling(LeftReg / 2))
                    Return True
                    Exit Function
                End If
            Else
                If LeftNum.Split = True Then
                    Return True
                    Exit Function
                End If
            End If
            If isRightRegular Then
                If RightReg >= 10 Then
                    Right = New Day18_SFNum(Math.Floor(RightReg / 2), Math.Ceiling(RightReg / 2))
                    Return True
                    Exit Function
                End If
            Else
                If RightNum.Split = True Then
                    Return True
                    Exit Function
                End If
            End If
            Return False
        End Function

        Public Overloads Shared Operator +(a As Day18_SFNum, b As Day18_SFNum) As Day18_SFNum
            Dim result = New Day18_SFNum(a, b)
            result.Reduce()
            Return result
        End Operator

        Public ReadOnly Property Magnitude As Integer
            Get
                Dim result As Integer = 0
                If isLeftRegular Then
                    result += LeftReg * 3
                Else
                    result += LeftNum.Magnitude * 3
                End If
                If isRightRegular Then
                    result += RightReg * 2
                Else
                    result += RightNum.Magnitude * 2
                End If

                Return result
            End Get
        End Property

        Public ReadOnly Property Depth As Integer
            Get
                Return Math.Max(LeftDepth, RightDepth)
            End Get
        End Property

        Public ReadOnly Property LeftDepth As Integer
            Get
                If isLeftRegular Then
                    Return 0
                Else
                    Return 1 + LeftNum.Depth
                End If
            End Get
        End Property

        Public ReadOnly Property RightDepth As Integer
            Get
                If isRightRegular Then
                    Return 0
                Else
                    Return 1 + RightNum.Depth
                End If
            End Get
        End Property
    End Class

    Function Day18_Part1(input As List(Of Day18_SFNum)) As Integer
        Dim SFresult As Day18_SFNum = Day18_InputSum(input)
        Return SFresult.Magnitude
    End Function

    Private Function Day18_InputSum(input As List(Of Day18_SFNum)) As Day18_SFNum
        Dim SFresult As Day18_SFNum = input(0)
        For i = 1 To input.Count - 1
            SFresult = SFresult + input(i)
            SFresult.Reduce()
        Next

        Return SFresult
    End Function

    Function Day18_Part2(input As List(Of Day18_SFNum)) As Integer
        Dim maxValue As Integer = Integer.MinValue
        For i = 0 To input.Count - 1
            For j = 0 To input.Count - 1
                If i = j Then Continue For

                Dim value = (input(i) + input(j)).Magnitude
                If value > maxValue Then maxValue = value
            Next
        Next
        Return maxValue
    End Function
End Module
