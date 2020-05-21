//java

import app.quant.clr.*;

import java.time.*;
import java.time.format.*;

import javabase.*;

class JavaAgent
{
    public JavaAgent(){}

    private static String workspaceID = "$WID$";
    public static Object pkg()
    {
        CLRObject Utils = CLRRuntime.GetClass("QuantApp.Engine.Utils");

        CLRObject M = CLRRuntime.GetClass("QuantApp.Kernel.M");

        return CLRRuntime.CreateInstance("QuantApp.Engine.FPKG",
            workspaceID + "-JavaAgent", //ID
            workspaceID, //Workflow ID  
            "Java Agent", //Name
            "Java Agent Sample", //Description
            null, //MID

            Utils.Invoke("SetFunction", "Load", CLRRuntime.CreateDelegate("QuantApp.Engine.Load", (x) -> { 
                System.out.println("Java Agent Load");
                return 0;
            })),

            Utils.Invoke("SetFunction", "Add", CLRRuntime.CreateDelegate("QuantApp.Kernel.MCallback", (x) -> { 
                // System.out.println("Java Agent Add: " + entry);
                return 0;
            })),

            Utils.Invoke("SetFunction", "Exchange", CLRRuntime.CreateDelegate("QuantApp.Kernel.MCallback", (x) -> { 
                String id = (String)x[0];
                Object data = x[1];

                System.out.println("Java Agent Exchange");
                return 0;
            })),

            Utils.Invoke("SetFunction", "Remove", CLRRuntime.CreateDelegate("QuantApp.Kernel.MCallback", (x) -> { 
                String id = (String)x[0];
                Object data = x[1];

                // System.out.println("Java Agent Remove");
                return 0;
            })),

            Utils.Invoke("SetFunction", "Body", CLRRuntime.CreateDelegate("QuantApp.Engine.Body", (x) -> { 
                Object data = x[0];
                
                System.out.println("Java Agent Body: " + data);
                return data;
            })),

            "0 * * ? * *", //Cron Schedule
            Utils.Invoke("SetFunction", "Job", CLRRuntime.CreateDelegate("QuantApp.Engine.Job", (x) -> { 
                Object date = x[0];
                String command = (String)x[1];

                // System.out.println("Java Agent Job: " + date +  " --> " + command);
                return 0;
            }))
        );
    }
}