/// <info version="1.0.100">
///     <title>Javascript Query Test API</title>
///     <description>Javascript Query API with samples for permissions, documentation and function definitions</description>
///     <termsOfService url="https://www.coflo.ws"/>
///     <contact name="Arturo Rodriguez" url="https://www.coflo.ws" email="arturo@coflo.ws"/>
///     <license name="Apache 2.0" url="https://www.apache.org/licenses/LICENSE-2.0.html"/>
/// </info>

var qengine = importNamespace('QuantApp.Engine')
var qkernel = importNamespace('QuantApp.Kernel')


/// <api name="getName">
///     <description>Function that returns a name</description>
///     <returns>returns an string</returns>
///     <permissions>
///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="write"/>
///     </permissions>
/// </api>
let getName = 'something'

/// <api name="Add">
///     <description>Function that adds two numbers</description>
///     <returns>returns an integer</returns>
///     <param name="x" type="integer">First number to add</param>
///     <param name="y" type="integer">Second number to add</param>
///     <permissions>
///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="read"/>
///     </permissions>
/// </api>
let Add = function(x, y) {
        return x + y
    }

/// <api name="Permission">
///     <description>Function that returns a permission</description>
///     <returns> returns an string</returns>
///     <permissions>
///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
///     </permissions>
/// </api>
let Permission = function() {
    var user = qkernel.User.ContextUser
    var permission = qkernel.User.PermissionContext("9b926680-059a-4a57-8ea8-a1d6c623c760")
    switch(permission)
    {
        case qkernel.AccessType.Write:
            return user.FirstName + " WRITE"
        case qkernel.AccessType.Read:
            return user.FirstName + " READ"
        case qkernel.AccessType.View:
            return user.FirstName + " VIEW"
        default:
            return user.FirstName + " DENIED"
    }
}

var fbase = importNamespace('Fs.Base')

/// <api name="Fs">
///     <description>F# Interop sample</description>
///     <returns> returns an string</returns>
///     <permissions>
///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
///     </permissions>
/// </api>
let Fs = function() {
    var fsbase = fbase.FsBase()
    var age_in_5_years = fsbase.Add(fsbase.getAge, 5)
    
    var result = "F# " + fsbase.getName + " will be " + age_in_5_years + " in 5 years and is interested in:"
    var interests = fsbase.getInterests()
    for(var i = 0; i < interests.length; i++)
        result += System.Environment.NewLine + interests[i]
        
    return result
}

var cbase = importNamespace('Cs.Base')

/// <api name="Cs">
///     <description>C# Interop sample</description>
///     <returns> returns an string</returns>
///     <permissions>
///         <group id="9b926680-059a-4a57-8ea8-a1d6c623c760" permission="view"/>
///     </permissions>
/// </api>
let Cs = function() {
    var csbase = cbase.csBase()
    var age_in_5_years = csbase.Add(csbase.getAge, 5)
    
    var result = "C# " + csbase.getName + " will be " + age_in_5_years + " in 5 years and is interested in:"
    var interests = csbase.getInterests
    
    for(var i = 0; i < interests.Count; i++)
        result += System.Environment.NewLine + interests[i]
        
    return result
}