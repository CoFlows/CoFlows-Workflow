//cs
using System;
using QuantApp.Engine;
using QuantApp.Kernel;

public class CsAgent
{
    private static string workspaceID = "$WID$";
    public static FPKG pkg()
    {
        return new FPKG(
            workspaceID + "-CsAgent", //ID 1
            workspaceID, //Workspace ID  
            "C# Agent", //Name
            "C# Agent Sample", //Description
            null, //MID
            Utils.SetFunction("Load", new Load((object[] data) => { })), 
            Utils.SetFunction("Add", new MCallback((string id, object data) => { })), 
            Utils.SetFunction("Exchange", new MCallback((string id, object data) => { })), 
            Utils.SetFunction("Remove", new MCallback((string id, object data) => { })), 
            Utils.SetFunction("Body", new Body((object data) => { return data; })), 

            "0 * * ? * *", //Cron Schedule
            Utils.SetFunction("Job", new Job((DateTime date, string command) => { }))
            );
    }
}