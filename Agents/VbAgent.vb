Imports System
Imports QuantApp.Engine
Imports QuantApp.Kernel

Public Class VbAgent
    Private Shared workspaceID As String = "$WID$"

    Public Shared Sub Load(data() As object) 
    End Sub

    public Shared Sub Add(id As String, data As object)
    End Sub

    public Shared Sub Exchange(id As String, data As object) 
    End Sub

    public Shared Sub Remove(id As String, data As object)
    End Sub

    public Shared Function Body(data As object) As object
        Return data
    End Function

    public Shared Sub Job(datetime As DateTime, command As string)
    End Sub

    public Shared Function pkg() As FPKG
        Return new FPKG(
            workspaceID + "-VbAgent", 'ID
            workspaceID, 'Workflow ID
            "VB Agent", 'Name
            "VB Analytics Agent Sample", 'Description
            Nothing, 'MID
            Utils.SetFunction("Load", new Load(AddressOf Load)), 
            Utils.SetFunction("Add", new MCallback(AddressOf Add)), 
            Utils.SetFunction("Exchange", new MCallback(AddressOf Exchange)), 
            Utils.SetFunction("Remove", new MCallback(AddressOf Remove)), 
            Utils.SetFunction("Body", new Body(AddressOf Body)), 
            "0 * * ? * *", 'Cron Schedule
            Utils.SetFunction("Job", new Job(AddressOf Job))
            )
    End Function
End Class
