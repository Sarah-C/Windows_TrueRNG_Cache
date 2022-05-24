Imports System.IO


' In this example 1024 bytes are read each second from the cache that has built up TrueRNG generated output.
' This clearly demonstrates the inbuilt cache decreasing its available pool of random values.
' Once the cache is deminished, the acqusition of numbers slows as the program waits for the requested number
' of random numbers to be produced directly from the TrueRNG generator.

' TrueRNG.vb and ComPortInfo.vb can be easily used in your own programs to cache values in RAM, and read a portion out when required.
' TrueRNG.vb has several ways of producing decimal values, integers within user-specified ranges from the bytes received from the TrueRNG.

Public Class StoreTrueRNGOutputIntoFiles

    Public WithEvents secondTick As New Timer()
    Public WithEvents gatherTick As New Timer()

    Public rnd As New TrueRNG()

    Public randomNumbers(1024) As Byte

    Public totalBytesSaved As Integer = 0
    Public bytesSavedThisSecond As Integer = 0
    Public arraySize As Integer = randomNumbers.Length

    Public fileNumber As Integer = 0
    Public fileSize As Integer = 0
    Public maxFileSize As Integer = 1024 * 1024 * 2 ' 2MB files  (1024 bytes * 1024 kilobytes * 2 MB)
    Public fileStream As FileStream = Nothing

    Private Sub StoreTrueRNGOutputIntoFiles_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        getNewFileStream()
        secondTick.Interval = 1000
        gatherTick.Interval = 10
        rnd.start(5000)
        secondTick.Start()
        gatherTick.Start()
    End Sub

    Private Sub t_Tick(sender As Object, e As EventArgs) Handles secondTick.Tick
        label_cachePercent.Text = rnd.cacheAvailableBytesPercent().ToString("#.0") & " %"
        label_bytesPerSecond.Text = bytesSavedThisSecond.ToString("###,###")
        label_totalBytesWritten.Text = totalBytesSaved.ToString("###,###")
        label_filesWritten.Text = fileNumber.ToString("###,###")
        bytesSavedThisSecond = 0
    End Sub

    Private Sub gatherTick_Tick(sender As Object, e As EventArgs) Handles gatherTick.Tick
        If rnd.cacheAvailableBytesPercent() < 5.0 Then Exit Sub
        gatherTick.Stop()
        rnd.NextBytes(randomNumbers)
        fileStream.Write(randomNumbers, 0, arraySize)
        If fileSize > maxFileSize Then
            fileSize = 0
            fileNumber += 1
            getNewFileStream()
        End If
        totalBytesSaved += arraySize
        bytesSavedThisSecond += arraySize
        fileSize += arraySize
        gatherTick.Start()
    End Sub

    Public Sub getNewFileStream()
        If fileStream IsNot Nothing Then fileStream.Close()
        fileStream = New FileStream($"Int{fileNumber}.rnd", FileMode.Append)
    End Sub

    Private Sub ButtonStop_Click(sender As Object, e As EventArgs) Handles ButtonStop.Click
        ' Let the CLR close the file, and clean up.
        Application.Exit()
    End Sub


    ' An example of using the buffered TrueRNG class for other things...

    'Private Sub Form1_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    '    'Dim r As New Random(1234)
    '    'Dim trueR As New TrueRNGRandom()

    '    'For a As Integer = 0 to 20
    '    '    Debug.Print(trueR.Next(0,256))
    '    'Next
    '    Dim bytesWritten As Integer = 0
    '    Dim fileNumber As Integer = 0

    '    Dim randomNumbers(1024) As Byte
    '    Using rnd As New TrueRNG()
    '        rnd.start(5000)

    '        Do
    '            rnd.NextBytes(randomNumbers)



    '        Loop While True


    '        For i As Integer = 0 To 5000
    '            'Console.write(rnd.Next(0, 256) & ",")
    '            For ii As Integer = 0 To 10
    '                Dim a = rnd.Next(0, 256)
    '            Next
    '            Debug.Print($"{i}: Stored in cache: " & rnd.cacheAvailableBytesPercent())
    '        Next
    '    End Using
    'End Sub

End Class
