/// <info version="1.0.100">
///     <title>Java Query Test API</title>
///     <description>Java Query API with samples for permissions, documentation and function definitions</description>
///     <termsOfService url="https://www.coflo.ws"/>
///     <contact name="Arturo Rodriguez" url="https://www.coflo.ws" email="arturo@coflo.ws"/>
///     <license name="Apache 2.0" url="https://www.apache.org/licenses/LICENSE-2.0.html"/>
/// </info>

import app.quant.clr.*;
import java.util.*;

class JavaQuery
{
    public JavaQuery(){}

    /// <api name="getName">
    ///     <description>Function that returns a name</description>
    ///     <returns>returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="write"/>
    ///     </permissions>
    /// </api>
    public static String getName()
    {
        return "something";
    }

    /// <api name="Add">
    ///     <description>Function that adds two numbers</description>
    ///     <returns>returns an integer</returns>
    ///     <param name="x" type="integer">First number to add</param>
    ///     <param name="y" type="integer">Second number to add</param>
    ///     <permissions>
    ///         <group id="$WID$" permission="read"/>
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
    ///         <group id="$WID$" permission="view"/>
    ///     </permissions>
    /// </api>
    public static String Permission()
    {
        CLRObject userClass = CLRRuntime.GetClass("QuantApp.Kernel.User");
        CLRObject user = (CLRObject)userClass.Invoke("GetContextUser");
        int permission = (int)userClass.Invoke("PermissionContext", "$WID$");
        
        switch(permission)
        {
            case 2:
                return user.GetProperty("FirstName") + " WRITE";
            case 1:
                return user.GetProperty("FirstName") + " READ";
            case 0:
                return user.GetProperty("FirstName") + " VIEW";
            default:
                return user.GetProperty("FirstName") + " DENIED";
        }
    }
    
    /// <api name="Cs">
    ///     <description>C# Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="view"/>
    ///     </permissions>
    /// </api>
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
    
    /// <api name="Python">
    ///     <description>Python Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="view"/>
    ///     </permissions>
    /// </api>
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
