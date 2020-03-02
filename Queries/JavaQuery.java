import app.quant.clr.*;
import java.util.*;

class JavaQuery
{
    public JavaQuery(){}

    public static String getName()
    {
        return "something";
    }

    public static int Add(int x, int y)
    {
        return x + y;
    }
    
    // C# Interop
    public static String Cs()
    {
        CLRObject csbase = CLRRuntime.CreateInstance("Cs.Base.csBase");

        Double age = (Double)csbase.GetProperty("getAge");
        String name = (String)csbase.GetProperty("getName");
        
        Integer age_in_5_years = (Integer)csbase.Invoke("Add", age.intValue(), 5);
        Iterable<Object> interests = (Iterable<Object>)csbase.GetProperty("getInterests");
        
        String result = "Cs " + name + " will be " + age_in_5_years + " in 5 years and is interested in: \n";
        
        for (Object interest : interests) 
            result += interest + "\n";

        return result;
    }
    
    // Python Interop
    public static Object Python()
    {
        return CLRRuntime.Python((x) -> {
            
            CLRObject pymod = CLRRuntime.PyImport("Base.pyBase.pybase");
            CLRObject pybase = (CLRObject)pymod.Invoke("pybase");
            
            Float age = (Float)pybase.GetProperty("getAge");
            String name = (String)pybase.GetProperty("getName");
            Iterable<Object> interests = (Iterable<Object>)pybase.Invoke("getInterests");
            
            Integer age_in_5_years = (Integer)pybase.Invoke("Add", age.intValue(), 5);
            
            String result = "Python " + name + " will be " + age_in_5_years + " in 5 years and is interested in: \n";
            
            for (Object interest : interests) 
                result += interest + "\n";

            return result;
        });
    }
}
