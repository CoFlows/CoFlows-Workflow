/// <info version="1.0.100">
///     <title>F# Query Test API</title>
///     <description>F# Query API with samples for permissions, documentation and function definitions</description>
///     <termsOfService url="https://www.coflo.ws"/>
///     <contact name="Arturo Rodriguez" url="https://www.coflo.ws" email="arturo@coflo.ws"/>
///     <license name="Apache 2.0" url="https://www.apache.org/licenses/LICENSE-2.0.html"/>
/// </info>

module FsQuery
    open FSharp.Interop.Dynamic
    open System
    open System.Collections
    open Python.Runtime 
    open QuantApp.Kernel
    
    /// <api name="getName">
    ///     <description>Function that returns a name</description>
    ///     <returns>returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="write"/>
    ///     </permissions>
    /// </api>
    let getName = "something"
        
    /// <api name="Add">
    ///     <description>Function that adds two numbers</description>
    ///     <returns>returns an integer</returns>
    ///     <param name="x" type="integer">First number to add</param>
    ///     <param name="y" type="integer">Second number to add</param>
    ///     <permissions>
    ///         <group id="$WID$" permission="read"/>
    ///     </permissions>
    /// </api>
    let Add x y = x + y

     /// <api name="Permission">
    ///     <description>Function that returns a permission</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="view"/>
    ///     </permissions>
    /// </api>
    let Permission() =
        let user = QuantApp.Kernel.User.ContextUser
        let permission = QuantApp.Kernel.User.PermissionContext("$WID$")
        match permission with
        | QuantApp.Kernel.AccessType.Write -> user.FirstName + " WRITE"
        | QuantApp.Kernel.AccessType.Read -> user.FirstName + " READ"
        | QuantApp.Kernel.AccessType.View -> user.FirstName + " VIEW"
        | _ -> user.FirstName + " DENIED"

    /// <api name="Cs">
    ///     <description>C# Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="view"/>
    ///     </permissions>
    /// </api>
    let Cs() =
    
        let csbase = Cs.Base.csBase()
        let age = csbase.getAge
        let age_in_5_years = csbase.Add(csbase.getAge |> int, 5)
        
        let result = "C# " + csbase.getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in: \n"

        let result =  csbase.getInterests |> Seq.fold(fun acc x -> acc + x.ToString() + " \n") result

        result

    /// <api name="Python">
    ///     <description>Python Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="view"/>
    ///     </permissions>
    /// </api>
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
        
    /// <api name="Scala">
    ///     <description>Scala Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="view"/>
    ///     </permissions>
    /// </api>
    let Scala() =
        let scalabase = JVM.Runtime.CreateInstance("scalabase.scalaBase")
        let age : double = scalabase?getAge() //important step to define type of getAge()
        
        let age_in_5_years : int = scalabase?Add(age |> int, 5)

        let result = "Scala " + scalabase?getName().ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in: \n"
        let result =  scalabase?getInterests() |> Seq.cast |> Seq.fold(fun acc x -> acc + x.ToString() + " \n") result

        result
        
    /// <api name="Java">
    ///     <description>Java Interop sample</description>
    ///     <returns> returns an string</returns>
    ///     <permissions>
    ///         <group id="$WID$" permission="view"/>
    ///     </permissions>
    /// </api>
    let Java() =
        let javabase = JVM.Runtime.CreateInstance("javabase.javaBase")
        let age : double = javabase?getAge //important step to define type of getAge()
        
        let age_in_5_years : int = javabase?Add(age |> int, 5)

        let result = "Java " + javabase?getName.ToString() + " will be " + age_in_5_years.ToString() + " in 5 years and is interested in: \n"
        let result =  javabase?getInterests() |> Seq.cast |> Seq.fold(fun acc x -> acc + x.ToString() + " \n") result

        result
    