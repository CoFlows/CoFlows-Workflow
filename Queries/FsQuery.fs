module FsQuery
    open FSharp.Interop.Dynamic
    open System
    open System.Collections
    open Python.Runtime 
    open QuantApp.Kernel
    
    let getName = "something"
    let Add x y = x + y

    //Permission
    let Permission() =
        let permission = QuantApp.Kernel.Group.FindGroup("06e1da00-4c81-4a35-914b-81c548b07345").PermissionContext()
        match permission with
        | QuantApp.Kernel.AccessType.Write -> "WRITE"
        | QuantApp.Kernel.AccessType.Read -> "READ"
        | QuantApp.Kernel.AccessType.View -> "VIEW"
        | _ -> "DENIED"

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
    