using Python.Runtime;
using JVM;

using System;
using System.Linq;

/// <info version="1.0.100">
///     <title>CsQuery Test API</title>
///     <description>CsQuery Test API</description>
///     <termsOfService url="https://www.coflo.ws"/>
///     <contact name="Arturo Rodriguez" url="https://www.coflo.ws" email="arturo@coflo.ws"/>
///     <license name="Apache 2.0" url="https://www.apache.org/licenses/LICENSE-2.0.html"/>
/// </info>

public class CsQuery
{
    /// <api name="getName">
    ///     <description> Function that returns a name </description>
    ///     <returns> returns an string </returns>
    ///     <permissions>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" cost="0.2" currency="USD" type="hourly"/>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" cost="20" currency="USD" type="percall"/>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" cost="30" permission="write"/>
    ///     </permissions>
    /// </api>
    public static string getName()
    {
        return "something";
    }

    /// <api name="Add">
    ///     <description> Function that adds two numbers </description>
    ///     <returns> returns an integer </returns>
    ///     <param name="x" type="integer">First number to add</param>
    ///     <param name="y" type="integer">Second number to add</param>
    /// </api>
    public static int Add(int x, int y)
    {
        var indicatorInfoTable = QuantApp.Kernel.Database.DB["DefaultStrategy"].GetDataTable("CMIndicatorInfoList", null, null);
        var rows = indicatorInfoTable.Rows;
        foreach (var dr in rows)
        {
        }
        return x + y;
    }
    
    /// <api name="Permission">
    ///     <description> Function that returns a permission </description>
    ///     <returns> returns an string-- </returns>
    ///     <permissions>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" permission="view"/>
    ///     </permissions>
    /// </api>
    public static string Permission()
    {
        var user = QuantApp.Kernel.User.ContextUser;
        var permission = QuantApp.Kernel.User.PermissionContext("06e1da00-4c81-4a35-914b-81c548b07345");
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

    // F# Interop
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

    //Python Interop
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

    //Java Interop
    public static string Java()
    {
        dynamic javabase = JVM.Runtime.CreateInstance("javabase.javaBase");
        var age_in_5_years = javabase.Add((int)javabase.getAge, 5);
        
        var result = "Java " + javabase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in:";
        foreach(var interest in javabase.getInterests())
            result += System.Environment.NewLine + interest.ToString();

        return result;
    }

    //Scala Interop
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