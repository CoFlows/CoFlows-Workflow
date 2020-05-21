//scala
import app.quant.clr._
import app.quant.clr.scala.{SCLRObject => CLR}

import java.time._
// import java.time.format._

class ScalaAgent {
    private val workspaceID = "$WID$"
    def pkg() = {
        val Utils = CLR("QuantApp.Engine.Utils")
        val M = CLR("QuantApp.Kernel.M")

        CLR("QuantApp.Engine.FPKG",
            workspaceID + "-ScalaAgent", //ID
            workspaceID, //Workflow ID  
            "Scala Agent", //Name
            "Scala Agent Sample", //Description
            null, //MID

            Utils.SetFunction("Load", CLR.Delegate[Array[AnyRef], Unit]("QuantApp.Engine.Load", data => { })),

            Utils.SetFunction("Add", CLR.Delegate[String, AnyRef, Unit]("QuantApp.Kernel.MCallback", (id, addedObject) => { })),

            Utils.SetFunction("Exchange", CLR.Delegate[String, AnyRef, Unit]("QuantApp.Kernel.MCallback", (id, data) => { })),

            Utils.SetFunction("Remove", CLR.Delegate[String, AnyRef, Unit]("QuantApp.Kernel.MCallback", (id, data) => { })),

            Utils.SetFunction("Body", CLR.Delegate[AnyRef, AnyRef]("QuantApp.Engine.Body", data => data)),

            "0 * * ? * *", //Cron Schedule
            Utils.SetFunction("Job", CLR.Delegate[LocalDateTime, AnyRef, Unit]("QuantApp.Engine.Job", (date, command) => { 
            }))
        )
    }
}