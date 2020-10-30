module FsQuery
    open FSharp.Interop.Dynamic
    open System
    open System.Collections
    open Python.Runtime 
    open QuantApp.Kernel
    
    /// <api name="getName">
    ///     <description> Function that returns a name </description>
    ///     <returns> returns an string </returns>
    ///     <permissions>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" cost="0.2" currency="USD" type="hourly"/>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345" cost="20" currency="USD" type="percall"/>
    ///         <group id="06e1da00-4c81-4a35-914b-81c548b07345"/>
    ///     </permissions>
    /// </api>
    let getName = "something"
    
    let groups() =
        " ------------ 1 " + QuantApp.Kernel.User.ContextUser.FirstName |> Console.WriteLine
        let g = QuantApp.Kernel.Group.MasterGroups() |> Seq.toList
        " ------------ 2 " + QuantApp.Kernel.User.ContextUser.FirstName |> Console.WriteLine
        let dd = 
            g
            |> List.map(fun x -> 
                " ------------ 3 " + QuantApp.Kernel.User.ContextUser.FirstName |> Console.WriteLine
                {| 
                    Name = x.Name;
                    Permission = x.PermissionContext().ToString();
                    UserID = QuantApp.Kernel.User.ContextUser.FirstName
                |})
        " ------------ 4 " + QuantApp.Kernel.User.ContextUser.FirstName |> Console.WriteLine    
        dd
        
    /// <api name="Add">
    /// <summary> Function that adds two numbers </summary>
    /// <remarks> it only works for all numbers </remarks>
    /// <returns> returns an integer </returns>
    /// <param name="x">First number to add</param>
    /// <param name="y">Second number to add</param>
    /// </api>
    let Add x y = x + y

    //Permission
    let Permission() =
        let user = QuantApp.Kernel.User.ContextUser
        let permission = QuantApp.Kernel.User.PermissionContext("06e1da00-4c81-4a35-914b-81c548b07345")
        match permission with
        | QuantApp.Kernel.AccessType.Write -> user.FirstName + " WRITE"
        | QuantApp.Kernel.AccessType.Read -> user.FirstName + " READ"
        | QuantApp.Kernel.AccessType.View -> user.FirstName + " VIEW"
        | _ -> user.FirstName + " DENIED"

    // C# Interop
    let Cs() =
    
        let csbase = Cs.Base.csBase()
        let age = csbase.getAge
        let age_in_5_years = csbase.Add(csbase.getAge |> int, 5)
        
        let result = "C# " + csbase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in: \n"

        let result =  csbase.getInterests |> Seq.fold(fun acc x -> acc + x.ToString() + " \n") result

        result

    //Python Interop
    let Python() =
        using (Py.GIL()) ( fun _ ->
            let pymodule = Py.Import("Base.pyBase.pybase")
            let pybase = pymodule?pybase()            
            let age = pybase?getAge |> Py.T<double>  //important step to define type of getAge()
            let age_in_5_years = pybase?Add(age, 5) |> Py.T<int>

            let result = "Python " + (pybase?getName |> Py.T<string>) + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in: \n"
            let result =  pybase?getInterests() |> Seq.cast |> Seq.fold(fun acc x -> acc + x.ToString() + " \n") result
            
            result    
        )
        
    //Scala Interop
    let Scala() =
        let scalabase = JVM.Runtime.CreateInstance("scalabase.scalaBase")
        let age : double = scalabase?getAge() //important step to define type of getAge()
        
        let age_in_5_years : int = scalabase?Add(age |> int, 5)

        let result = "Scala " + scalabase?getName().ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in: \n"
        let result =  scalabase?getInterests() |> Seq.cast |> Seq.fold(fun acc x -> acc + x.ToString() + " \n") result

        result
        
    //Java Interop
    let Java() =
        let javabase = JVM.Runtime.CreateInstance("javabase.javaBase")
        let age : double = javabase?getAge //important step to define type of getAge()
        
        let age_in_5_years : int = javabase?Add(age |> int, 5)

        let result = "Java " + javabase?getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in: \n"
        let result =  javabase?getInterests() |> Seq.cast |> Seq.fold(fun acc x -> acc + x.ToString() + " \n") result

        result
    