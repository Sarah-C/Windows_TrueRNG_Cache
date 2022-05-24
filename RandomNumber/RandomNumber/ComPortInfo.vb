Imports System.Management

Public Class COMPortInfo

    Public name() As String
    Public description() As String

    Public nameList As New List(Of String)
    Public descriptionList As New List(Of String)

    Public Sub GetCOMPortsInfo(ByVal search As String)
        Dim objectQuery As String = String.Format("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0 AND Caption LIKE '%{0}%'", search)
        Dim comPortSearcher As New ManagementObjectSearcher("root\CIMV2", objectQuery)

        Using comPortSearcher
            Dim theCaption As String = Nothing
            For Each PnPItem As ManagementObject In comPortSearcher.Get()
                If PnPItem IsNot Nothing Then
                    Dim caption As Object = PnPItem("Caption")
                    Console.WriteLine(caption)
                    If caption IsNot Nothing Then
                        theCaption = CStr(caption)
                        nameList.Add(theCaption.Substring(theCaption.LastIndexOf("(COM")).Replace("(", String.Empty).Replace(")", String.Empty))
                        descriptionList.Add(CStr(PnPItem("Description")))
                    End If
                End If
            Next
        End Using

        name = nameList.ToArray()
        description = descriptionList.ToArray()
    End Sub

End Class