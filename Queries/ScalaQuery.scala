/// <info version="1.0.100">
///     <title>Scala Query Test API</title>
///     <description>Scala Query API with samples for permissions, documentation and function definitions</description>
///     <termsOfService url="https://www.coflo.ws"/>
///     <contact name="Arturo Rodriguez" url="https://www.coflo.ws" email="arturo@coflo.ws"/>
///     <license name="Apache 2.0" url="https://www.apache.org/licenses/LICENSE-2.0.html"/>
/// </info>

import scala.collection._
import app.quant.clr._
import app.quant.clr.scala.{SCLRObject => CLR}

import collection.JavaConverters._

class ScalaQuery {
    /// <api name="getName">
    ///     <description>Function that returns a name</description>
    ///     <returns>returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="write"/>
    ///     </permissions>
    /// </api>
    def getName = "something"

    /// <api name="Add">
    ///     <description>Function that adds two numbers</description>
    ///     <returns>returns an integer</returns>
    ///     <param name="x" type="integer">First number to add</param>
    ///     <param name="y" type="integer">Second number to add</param>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="read"/>
    ///     </permissions>
    /// </api>
    def Add(x:Int, y:Int) = x + y

    /// <api name="Permission">
    ///     <description>Function that returns a permission</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
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
     
    /// <api name="Cs">
    ///     <description>C# Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
    def Cs = {

        val csbase = CLR("Cs.Base.csBase")
        
        val age_in_5_years = csbase.Add[Integer](csbase.getAge[Double].asInstanceOf[Int], 5)
        var result = "Cs " + csbase.getName[String] + " will be " + age_in_5_years + " in 5 years and is interested in: \n"
  
        csbase.getInterests[Iterable[Any]].foreach(interest => result += interest + "\n")
        
        result
    }

    /// <api name="Fs">
    ///     <description>F# Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
    def Fs = {
        val csbase = CLR("Fs.Base.FsBase")
        
        val age_in_5_years = csbase.Add[Integer](csbase.getAge[Double].asInstanceOf[Int], 5)
        var result = "Fs " + csbase.getName[String] + " will be " + age_in_5_years + " in 5 years and is interested in: \n"
  
        csbase.getInterests[Iterable[Any]]().foreach(interest => result += interest + "\n")
        
        result
    }

    /// <api name="Python">
    ///     <description>Python Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
    def Python = 
        CLRRuntime.Python(_ => {
            val pybase = CLR.PyImport("Base.pyBase.pybase").pybase[CLR]()
            
            val age_in_5_years = pybase.Add[Int](pybase.getAge[Float].asInstanceOf[Int], 5)
            var result = "Python " + pybase.getName[String] + " will be " + age_in_5_years + " in 5 years and is interested in: \n"
            
            pybase.getInterests[Iterable[Any]]().foreach(interest => result += interest + "\n")
            
            result
        })
}