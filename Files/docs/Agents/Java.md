Java Agent
===
This is a generic example of Java agent following the generic structure within **CoFlows**.

Note: The Java <-> CoreCLR interop is achieved through [QuantApp.Kernel/JVM](https://github.com/QuantApp/CoFlows-CE/tree/master/QuantApp.Kernel/JVM "QAJVM").

    import app.quant.clr.*;

    class JavaAgent
    {
        public JavaAgent(){}

        private static String defaultID = "xxx";
        public static Object pkg()
        {
            CLRObject Utils = CLRRuntime.GetClass("QuantApp.Engine.Utils");

            CLRObject M = CLRRuntime.GetClass("QuantApp.Kernel.M");

            return CLRRuntime.CreateInstance("QuantApp.Engine.FPKG",
                defaultID, //ID
                "Hello_World_WorkSpace", //Workspace ID  
                "Hello Java Agent", //Name
                "Hello Java Analytics Agent Sample", //Description
                "xxx-MID", //JS Listener

                Utils.Invoke("SetFunction", "Load", CLRRuntime.CreateDelegate("QuantApp.Engine.Load", (x) -> { 
                    System.out.println("Java Agent Load");
                    return 0;
                })),

                Utils.Invoke("SetFunction", "Add", CLRRuntime.CreateDelegate("QuantApp.Kernel.MCallback", (x) -> { 
                    System.out.println("Java Agent Add: " + entry);
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

                    System.out.println("Java Agent Remove");
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