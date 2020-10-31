/*
 * The MIT License (MIT)
 * Copyright (c) Arturo Rodriguez All rights reserved.
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
 
using System;
using System.Collections.Generic;
using QuantApp.Engine;
using QuantApp.Kernel;
using Newtonsoft.Json.Linq;

public class CsAgent
{
    private static string workspaceID = "$WID$";
    public static FPKG pkg()
    {
        return new FPKG(
            workspaceID + "-CsAgent", //ID 1
            workspaceID, //Workflow ID  
            "C# Agent", //Name
            "C# Agent Sample", //Description
            null, //MID
            Utils.SetFunction("Load", new Load((object[] data) => { })), 
            Utils.SetFunction("Add", new MCallback((string id, object data) => { })), 
            Utils.SetFunction("Exchange", new MCallback((string id, object data) => { })), 
            Utils.SetFunction("Remove", new MCallback((string id, object data) => { })), 
            Utils.SetFunction("Body", new Body((object data) => {
                
                var cmd = JObject.Parse(data.ToString());
                if(cmd.ContainsKey("Data") && cmd["Data"].ToString() == "Initial Execution")
                    Console.WriteLine("     C# Initial Execute @ " + DateTime.Now);

                return data; 
                })), 

            "0 * * ? * *", //Cron Schedule
            Utils.SetFunction("Job", new Job((DateTime date, string command) => { }))
            );
    }
}