import scala.collection._
import app.quant.clr._
import app.quant.clr.scala.{SCLRObject => CLR}

import collection.JavaConverters._
   
class ScalaQuery {
    def getName = "something"

    /// <api name="Add">
    /// <summary> Function that adds two numbers </summary>
    /// <remarks> it only works for integers </remarks>
    /// <returns> returns an integer </returns>
    /// <param name="x">First number to add</param>
    /// <param name="y">Second number to add</param>
    /// </api>
    def Add(x:Int, y:Int) = x + y

    // Permission
    def Permission = {
    
        val userClass = CLR("QuantApp.Kernel.User")
        val user = userClass.GetContextUser[CLR]()
        val permission = userClass.PermissionContext[Int]("06e1da00-4c81-4a35-914b-81c548b07345")
        
        permission match {        
            case 2 => user.FirstName[String] + " WRITE"
            case 1 => user.FirstName[String] + " READ"
            case 0 => user.FirstName[String] + " VIEW"
            case _ => user.FirstName[String] + " DENIED"
        }
    }
     
    // C# Interop
    def Cs = {

        val csbase = CLR("Cs.Base.csBase")
        
        val age_in_5_years = csbase.Add[Integer](csbase.getAge[Double].asInstanceOf[Int], 5)
        var result = "Cs " + csbase.getName[String] + " will be " + age_in_5_years + " in 5 years and is interested in: \n"
  
        csbase.getInterests[Iterable[Any]].foreach(interest => result += interest + "\n")
        
        result
    }

    // F# Interop
    def Fs = {
        val csbase = CLR("Fs.Base.FsBase")
        
        val age_in_5_years = csbase.Add[Integer](csbase.getAge[Double].asInstanceOf[Int], 5)
        var result = "Fs " + csbase.getName[String] + " will be " + age_in_5_years + " in 5 years and is interested in: \n"
  
        csbase.getInterests[Iterable[Any]]().foreach(interest => result += interest + "\n")
        
        result
    }

    // Python Interop
    def Python = 
        CLRRuntime.Python(_ => {
            val pybase = CLR.PyImport("Base.pyBase.pybase").pybase[CLR]()
            
            val age_in_5_years = pybase.Add[Int](pybase.getAge[Float].asInstanceOf[Int], 5)
            var result = "Python " + pybase.getName[String] + " will be " + age_in_5_years + " in 5 years and is interested in: \n"
            
            pybase.getInterests[Iterable[Any]]().foreach(interest => result += interest + "\n")
            
            result
        })
}