Imports System.Threading
Imports System.IO.Ports

Public Class TrueRNG
    Implements IDisposable

    Private Const MBIG As Integer = Int32.MaxValue

    Public cacheSize As Integer = 500000

    Private _continueThreadCollectingValues As Boolean = True
    Private SP As New SerialPort()
    Private FIFO As New Queue(Of Byte)
    Private readThread As Thread = Nothing

    Public comPortInfo As COMPortInfo = Nothing
    Public availableTrueRNGComPorts As List(Of String)
    Public Event ready()
    Public Event timeOut()

    Public Sub New()
        comPortInfo = New COMPortInfo()
        ' If the driver isn't installed it appears as something generic like "USB serial device"
        comPortInfo.GetCOMPortsInfo("TrueRNG")
        availableTrueRNGComPorts = comPortInfo.nameList
    End Sub

    Public Sub start()
        doStart()
    End Sub

    Public Sub start(ByVal warmingUpDelayMilliseconds As Integer)
        doStart()
        Threading.Thread.Sleep(warmingUpDelayMilliseconds)
    End Sub

    Public Sub start(ByVal comPortName As String)
        doStart(comPortName)
    End Sub

    Public Sub start(ByVal comPortName As String, ByVal warmingUpDelayMilliseconds As Integer)
        doStart(comPortName)
        Threading.Thread.Sleep(warmingUpDelayMilliseconds)
    End Sub

    Private Sub doStart(Optional comPort As String = Nothing)
        If availableTrueRNGComPorts.Count = 0 Then Throw New IndexOutOfRangeException("There are no TrueRNG devices available.") : Exit Sub
        If comPort IsNot Nothing Then run(comPort) Else run(availableTrueRNGComPorts(0))
    End Sub

    Private Sub run(ByVal portName As String)
        readThread = New Thread(AddressOf Read)
        SP = New SerialPort()
        SP.PortName = availableTrueRNGComPorts(0)
        SP.DtrEnable = True
        SP.ReadTimeout = 500
        SP.WriteTimeout = 500
        readThread.IsBackground = True
        readThread.Start()
    End Sub

    Public Sub dispose() Implements IDisposable.Dispose
        _continueThreadCollectingValues = False
    End Sub

    Private Sub Read()
        Dim bytes(10000) As Byte
        SP.Open()
        While _continueThreadCollectingValues
            Try
                Dim bytesToRead As Integer = SP.BytesToRead()
                If bytesToRead > bytes.Count Then bytesToRead = bytes.Count
                SP.Read(bytes, 0, bytesToRead)
                If FIFO.Count < cacheSize Then
                    For a As Integer = 0 To bytesToRead - 1
                        FIFO.Enqueue(bytes(a))
                    Next
                End If
                RaiseEvent ready()
                Threading.Thread.Sleep(10)
            Catch generatedExceptionName As TimeoutException
                RaiseEvent timeOut()
            End Try
        End While
        SP.Close()
    End Sub


#Region " Outputs"

    Public Function cacheAvailableBytesPercent() As Decimal
        Return (CDec(FIFO.Count) / CDec(cacheSize)) * 100D
    End Function

    Public Function cacheAvailableBytes() As Decimal
        Return FIFO.Count
    End Function

    Public Function cacheAvailableIntegerPercent() As Decimal
        Return (CDec(FIFO.Count \ 4) / CDec(cacheSize \ 4)) * 100D
    End Function

    Public Function cacheAvailableInteger() As Decimal
        Return FIFO.Count \ 4
    End Function

    Public Function cacheAvailableDecimalPercent() As Decimal
        Return (CDec(FIFO.Count \ 4) / CDec(cacheSize \ 4)) * 100D
    End Function

    Public Function cacheAvailableDecimal() As Decimal
        Return FIFO.Count \ 4
    End Function

    Public Function getByte() As Byte
        Do
            If FIFO.Count > 1 Then Return FIFO.Dequeue()
            Threading.Thread.Sleep(10)
        Loop While True
        Return 0
    End Function

    Protected Overridable Function Sample() As Double
        Return (InternalSample() / MBIG)
    End Function

    Private Function InternalSample() As Integer
        Dim randVal As Integer = 0
        If FIFO.Count > 4 Then
            randVal += (FIFO.Dequeue() And 127) << 24
            randVal += FIFO.Dequeue() << 16
            randVal += FIFO.Dequeue() << 8
            randVal += FIFO.Dequeue()
        Else
            randVal += (getByte() And 127) << 24
            randVal += getByte() << 16
            randVal += getByte() << 8
            randVal += getByte()
        End If
        Return randVal
    End Function

    '      =====================================Next=====================================
    '      **Returns: An int [0..Int32.MaxValue)
    '      **Arguments: None
    '      **Exceptions: None.
    '      ==============================================================================
    Public Overridable Function [Next]() As Integer
        Return InternalSample()
    End Function

    Private Function GetSampleForLargeRange() As Double
        ' The distribution of double value returned by Sample 
        ' is not distributed well enough for a large range.
        ' If we use Sample for a range [Int32.MinValue..Int32.MaxValue)
        ' We will end up getting even numbers only.

        Dim result As Integer = InternalSample()
        ' Note we can't use addition here. The distribution will be bad if we do that.
        Dim negative As Boolean = If(InternalSample() Mod 2 = 0, True, False) ' decide the sign based on second sample
        If negative Then
            result = -result
        End If
        Dim d As Double = result
        d += (Int32.MaxValue - 1) ' get a number in range [0 .. 2 * Int32MaxValue - 1)
        d /= 2 * CUInt(Int32.MaxValue) - 1
        Return d
    End Function


    '      =====================================Next=====================================
    '      **Returns: An int [minvalue..maxvalue)
    '      **Arguments: minValue -- the least legal value for the Random number.
    '      **           maxValue -- One greater than the greatest legal return value.
    '      **Exceptions: None.
    '      ==============================================================================
    Public Overridable Function [Next](ByVal minValue As Integer, ByVal maxValue As Integer) As Integer
        If minValue > maxValue Then
            Throw New ArgumentOutOfRangeException("minValue", "Argument_MinMaxValue")
        End If

        Dim range As Long = CLng(maxValue) - minValue
        If range <= CLng(Int32.MaxValue) Then
            Return (CInt(Math.Truncate(Sample() * range)) + minValue)
        Else
            Return CInt(CLng(Math.Truncate(GetSampleForLargeRange() * range)) + minValue)
        End If
    End Function


    '      =====================================Next=====================================
    '      **Returns: An int [0..maxValue)
    '      **Arguments: maxValue -- One more than the greatest legal return value.
    '      **Exceptions: None.
    '      ==============================================================================
    Public Overridable Function [Next](ByVal maxValue As Integer) As Integer
        If maxValue < 0 Then
            Throw New ArgumentOutOfRangeException("maxValue", "ArgumentOutOfRange_MustBePositive")
        End If
        Return CInt(Math.Truncate(Sample() * maxValue))
    End Function


    '      =====================================Next=====================================
    '      **Returns: A double [0..1)
    '      **Arguments: None
    '      **Exceptions: None
    '      ==============================================================================
    Public Overridable Function NextDouble() As Double
        Return Sample()
    End Function


    '      /*==================================NextBytes===================================
    '      **Action:  Fills the byte array with random bytes [0..0x7f].  The entire array is filled.
    '      **Returns:Void
    '      **Arugments:  buffer -- the array to be filled.
    '      **Exceptions: None
    '      ==============================================================================*/
    Public Overridable Sub NextBytes(ByVal buffer() As Byte)
        If buffer Is Nothing Then
            Throw New ArgumentNullException("Buffer is not defined")
        End If
        For i As Integer = 0 To buffer.Length - 1
            buffer(i) = CByte(getByte())
        Next i
    End Sub

#End Region

End Class