/// <info version="1.0.100">
///     <title>C# Query Test API</title>
///     <description>C# Query API with samples for permissions, documentation and function definitions</description>
///     <termsOfService url="https://www.coflo.ws"/>
///     <contact name="Arturo Rodriguez" url="https://www.coflo.ws" email="arturo@coflo.ws"/>
///     <license name="Apache 2.0" url="https://www.apache.org/licenses/LICENSE-2.0.html"/>
/// </info>

using Python.Runtime;
using JVM;

using System;
using System.Linq;

public class CsQuery
{
    /// <api name="getName">
    ///     <description>Function that returns a name</description>
    ///     <returns>returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="write"/>
    ///     </permissions>
    /// </api>
    public static string getName()
    {
        return "something";
    }

    /// <api name="Add">
    ///     <description>Function that adds two numbers</description>
    ///     <returns>returns an integer</returns>
    ///     <param name="x" type="integer">First number to add</param>
    ///     <param name="y" type="integer">Second number to add</param>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="read"/>
    ///     </permissions>
    /// </api>
    public static int Add(int x, int y)
    {
        return x + y;
    }
    
    /// <api name="Permission">
    ///     <description>Function that returns a permission</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
    public static string Permission()
    {
        var user = QuantApp.Kernel.User.ContextUser;
        var permission = QuantApp.Kernel.User.PermissionContext("9b926680-059a-4a57-8ea8-a1d6c623c760");
        switch(permission)
        {
            case QuantApp.Kernel.AccessType.Write:
                return user.FirstName + " WRITE";
            case QuantApp.Kernel.AccessType.Read:
                return user.FirstName + " READ";
            case QuantApp.Kernel.AccessType.View:
                return user.FirstName + " VIEW";
            default:
                return user.FirstName + " DENIED";
        }
    }

    /// <api name="Fs">
    ///     <description>F# Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
    public static string Fs()
    {
        var fsbase = new Fs.Base.FsBase();
        var age = fsbase.getAge;
        var age_in_5_years = fsbase.Add((int)fsbase.getAge, 5);
        
        var result = "F# " + fsbase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
        foreach(var interest in fsbase.getInterests())
            result += System.Environment.NewLine + interest.ToString();

        return result;
    }

    /// <api name="Python">
    ///     <description>Python Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
    public static string Python()
    {
        using(Py.GIL())
        {
            dynamic pymodule = Py.Import("Base.pyBase.pybase");
            dynamic pybase = pymodule.pybase();
            var age_in_5_years = pybase.Add((int)pybase.getAge, 5);

            var result = "Python " + pybase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
            foreach(var interest in pybase.getInterests())
                result += System.Environment.NewLine + interest.ToString();

            return result;
        }
    }

    /// <api name="Java">
    ///     <description>Java Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
    public static string Java()
    {
        dynamic javabase = JVM.Runtime.CreateInstance("javabase.javaBase");
        var age_in_5_years = javabase.Add((int)javabase.getAge, 5);
        
        var result = "Java " + javabase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
        foreach(var interest in javabase.getInterests())
            result += System.Environment.NewLine + interest.ToString();

        return result;
    }

    /// <api name="Scala">
    ///     <description>Scala Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
    ///     </permissions>
    /// </api>
    public static string Scala()
    {
        dynamic scalabase = JVM.Runtime.CreateInstance("scalabase.scalaBase");
        var age_in_5_years = scalabase.Add((int)scalabase.getAge(), 5);

        var result = "Scala " + scalabase.getName().ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
        foreach(var interest in scalabase.getInterests())
            result += System.Environment.NewLine + interest.ToString();

        return result;
    }
}