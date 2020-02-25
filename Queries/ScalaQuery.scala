import scala.collection._
import app.quant.clr._
import app.quant.clr.scala.{SCLRObject => CLR}

import collection.JavaConverters._
   
class ScalaQuery {
    def getName = "something"
    def Add(x:Int, y:Int) = x + y
     
    // C# Interop
    def Cs = {

        val csbase = CLR("csBase.csBase")
        
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
            val pybase = CLR.PyImport("pybase").pybase[CLR]()
            
            val age_in_5_years = pybase.Add[Int](pybase.getAge[Float].asInstanceOf[Int], 5)
            var result = "Python " + pybase.getName[String] + " will be " + age_in_5_years + " in 5 years and is interested in: \n"
            
            pybase.getInterests[Iterable[Any]]().foreach(interest => result += interest + "\n")
            
            result
        })
}