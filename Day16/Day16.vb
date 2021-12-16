Module Day16
    Sub Day16_main()
        'part1
        Debug.Assert(New Day16_Packet("D2FE28", "h").Value = 2021)
        Debug.Assert(New Day16_Packet("38006F45291200", "h").SubPackets.Count = 2)
        Debug.Assert(New Day16_Packet("EE00D40C823060", "h").SubPackets.Count = 3)

        Debug.Assert(Day16_Part1(New Day16_Packet("8A004A801A8002F478", "h")) = 16)
        Debug.Assert(Day16_Part1(New Day16_Packet("620080001611562C8802118E34", "h")) = 12)
        Debug.Assert(Day16_Part1(New Day16_Packet("C0015000016115A2E0802F182340", "h")) = 23)
        Debug.Assert(Day16_Part1(New Day16_Packet("A0016C880162017C3686B18A3D4780", "h")) = 31)

        Dim input = Day16_ReadInput("Day16\Day16_input.txt")
        Console.WriteLine("Day16 Part1: " & Day16_Part1(input))

        'part2
        Debug.Assert(Day16_Part2(New Day16_Packet("C200B40A82", "h")) = 3)
        Debug.Assert(Day16_Part2(New Day16_Packet("04005AC33890", "h")) = 54)
        Debug.Assert(Day16_Part2(New Day16_Packet("880086C3E88112", "h")) = 7)
        Debug.Assert(Day16_Part2(New Day16_Packet("D8005AC2A8F0", "h")) = 1)
        Debug.Assert(Day16_Part2(New Day16_Packet("F600BC2D8F", "h")) = 0)
        Debug.Assert(Day16_Part2(New Day16_Packet("9C005AC2F8F0", "h")) = 0)
        Debug.Assert(Day16_Part2(New Day16_Packet("9C0141080250320F1802104A08", "h")) = 1)
        Console.WriteLine("Day16 Part2: " & Day16_Part2(input))

    End Sub
    Function Day16_ReadInput(inputpath As String) As Day16_Packet
        Dim sr As New IO.StreamReader(inputpath)
        Dim line = sr.ReadToEnd
        sr.Close()
        Return New Day16_Packet(line, "h")
    End Function

    Class Day16_Packet
        Public binaryString As String
        Public Version As Integer
        Public TypeId As Integer
        Public Value As Int64 = 0
        Public LengthTypeId As Integer = 0
        Public SubPackets As New List(Of Day16_Packet)

        Public Sub New(raw As String, s As String)
            If s = "h" Then
                binaryString = ""
                For Each c In raw
                    binaryString += Convert.ToString(Convert.ToInt32(c.ToString, 16), 2).PadLeft(4, "0")
                Next
            Else
                binaryString = raw
            End If
            Version = Convert.ToInt32(binaryString.Substring(0, 3), 2)
            TypeId = Convert.ToInt32(binaryString.Substring(3, 3), 2)

            If TypeId = 4 Then
                Dim numstring As String = ""
                Dim rawLiteral As String = binaryString.Substring(6, binaryString.Length - 6)
                For i = 0 To 4 'always add 4 zero to the end, that should work every time
                    rawLiteral &= "0"c
                Next
                Dim pointer As Integer = 0
                While pointer < rawLiteral.Length
                    numstring &= rawLiteral.Substring(pointer + 1, 4)
                    If rawLiteral(pointer) = "0"c Then Exit While
                    pointer += 5
                End While
                binaryString = binaryString.Substring(0, 6 + pointer + 5)
                Value = Convert.ToInt64(numstring, 2)
            Else
                LengthTypeId = Convert.ToInt32(binaryString.Substring(6, 1), 2)
                If LengthTypeId = 0 Then
                    'next 15 bits are a number that represents the total length in bits of the sub-packets contained by this packet.
                    Dim length = Convert.ToInt32(binaryString.Substring(7, 15), 2)
                    Dim leftover = binaryString.Substring(22, length)
                    Dim pointer = 0
                    Do
                        Try
                            Dim newpack = New Day16_Packet(leftover.Substring(pointer), "b")
                            SubPackets.Add(newpack)
                            pointer += newpack.binaryString.Length
                        Catch ex As Exception
                            Exit Do
                        End Try
                    Loop While pointer < leftover.Length
                    binaryString = binaryString.Substring(0, 22 + pointer)
                Else
                    'the next 11 bits are a number that represents the number of sub-packets immediately contained by this packet.
                    Dim numOfPackage = Convert.ToInt32(binaryString.Substring(7, 11), 2)
                    Dim leftover = binaryString.Substring(18)
                    Dim pointer = 0
                    For i = 1 To numOfPackage
                        Dim newpack = New Day16_Packet(leftover.Substring(pointer), "b")
                        SubPackets.Add(newpack)
                        pointer += newpack.binaryString.Length
                    Next
                    binaryString = binaryString.Substring(0, 18 + pointer)
                End If
            End If
        End Sub

        Public ReadOnly Property SumOfVersions As Integer
            Get
                Dim result As Integer = Version
                For Each subpacket In SubPackets
                    result += subpacket.SumOfVersions
                Next
                Return result
            End Get
        End Property

        Public ReadOnly Property Evaluate As Int64
            Get
                Dim result As Int64 = 0
                Select Case TypeId
                    Case 0 'sum
                        For Each subpack In SubPackets
                            result += subpack.Evaluate
                        Next
                    Case 1 'product
                        result = 1
                        For Each subpack In SubPackets
                            result *= subpack.Evaluate
                        Next
                    Case 2 'minimum
                        result = SubPackets.Min(Function(f) f.Evaluate)
                    Case 3 'maximum
                        result = SubPackets.Max(Function(f) f.Evaluate)
                    Case 4 'value
                        result = Value
                    Case 5 'greater than
                        Debug.Assert(SubPackets.Count = 2)
                        result = If(SubPackets(0).Evaluate > SubPackets(1).Evaluate, 1, 0)
                    Case 6 'less than
                        Debug.Assert(SubPackets.Count = 2)
                        result = If(SubPackets(0).Evaluate < SubPackets(1).Evaluate, 1, 0)
                    Case 7 'equal to
                        Debug.Assert(SubPackets.Count = 2)
                        result = If(SubPackets(0).Evaluate = SubPackets(1).Evaluate, 1, 0)
                    Case Else
                        Throw New NotImplementedException
                End Select
                Return result
            End Get
        End Property
    End Class


    Function Day16_Part1(input As Day16_Packet) As Integer
        Return input.SumOfVersions
    End Function

    Function Day16_Part2(input As Day16_Packet) As Int64
        Return input.Evaluate
    End Function
End Module
