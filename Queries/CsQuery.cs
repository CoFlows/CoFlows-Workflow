using Python.Runtime;
using JVM;

public class CsQuery
{   
    public static string getName()
    {
        return "something";
    }

    public static int Add(int x, int y)
    {
        return x + y;
    }

    //F# Interop
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
            dynamic pymodule = Py.Import("Base.pybase");
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