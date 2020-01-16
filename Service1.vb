Imports System.Threading

'https://docs.microsoft.com/en-us/dotnet/framework/windows-services/walkthrough-creating-a-windows-service-application-in-the-component-designer

Public Class Service1

    Private oTimer As System.Threading.Timer

    Protected Overrides Sub OnStart(ByVal args() As String)
        Log($"{DateTime.Now} - OnStart")

        Dim now As DateTime = DateTime.Now
        Dim today As DateTime = now.Date

        Dim oCallback As New TimerCallback(AddressOf OnTimedEvent)
        'corre num intervalo de 1 mins
        oTimer = New System.Threading.Timer(oCallback, Nothing, 0, 60000 * 1)
    End Sub

    Protected Overrides Sub OnStop()
        Log($"{DateTime.Now} - OnStop")
    End Sub

    Private Sub OnTimedEvent(ByVal state As Object)
        Log($"{DateTime.Now} - OnTimedEvent")

        'verifica se está em execução
        If Not IsProcessRunning("notepad") Then
            'Second nao está, inicia
            StartProcess("notepad")
        End If

    End Sub

    Sub Log(message As String)
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter("c:\test.txt", True)
        file.WriteLine(message)
        file.Close()
    End Sub

    Function IsProcessRunning(name As String)

        Dim pname As Process() = Process.GetProcessesByName(name)

        If pname.Length = 0 Then
            Log("IsProcessRunning False")
            Return False
        Else
            Log("IsProcessRunning True")
            Return True
        End If

    End Function

    Sub StartProcess(name As String)
        System.Diagnostics.Process.Start(name)
    End Sub

End Class
