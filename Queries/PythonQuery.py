### <info version="1.0.100">
###     <title>C# Query Test API</title>
###     <description>C# Query API with samples for permissions, documentation and function definitions</description>
###     <termsOfService url="https://www.coflo.ws"/>
###     <contact name="Arturo Rodriguez" url="https://www.coflo.ws" email="arturo@coflo.ws"/>
###     <license name="Apache 2.0" url="https://www.apache.org/licenses/LICENSE-2.0.html"/>
### </info>

import QuantApp.Kernel as qak
import Cs.Base as csb

def getName():
    return "something"

### <api name="Add">
###     <description>Function that adds two numbers</description>
###     <returns>returns an integer</returns>
###     <param name="x" type="integer">First number to add</param>
###     <param name="y" type="integer">Second number to add</param>
###     <permissions>
###         <group id="$WID$" permission="read"/>
###     </permissions>
### </api>
def Add(x, y):
    return x + y


### <api name="Permission">
###     <description>Function that returns a permission</description>
###     <returns> returns an string</returns>
###     <permissions>
###         <group id="$WID$" permission="view"/>
###     </permissions>
### </api>
def Permission():
    quser = qak.User.ContextUser
    permission = qak.User.PermissionContext("06e1da00-4c81-4a35-914b-81c548b07345")
    if permission == qak.AccessType.Write:
        return quser.FirstName + " WRITE"
    elif permission == qak.AccessType.Read:
        return quser.FirstName + " READ"
    elif permission == qak.AccessType.View:
        return quser.FirstName + " VIEW"
    else:
        return quser.FirstName + " DENIED"


### <api name="Cs">
###     <description>C# Interop sample</description>
###     <returns> returns an string</returns>
###     <permissions>
###         <group id="$WID$" permission="view"/>
###     </permissions>
### </api>
def Cs():
    csbase = csb.csBase()
    
    age_in_5_years = csbase.Add(csbase.getAge, 5)
    result = "C# " + str(csbase.getName) + " will be " + str(age_in_5_years) + " in 5 years and is interested in:"
    for interest in csbase.getInterests:
        result = result + str(interest) + "\n"
    
    return result

### <api name="Java">
###     <description>Java Interop sample</description>
###     <returns> returns an string</returns>
###     <permissions>
###         <group id="$WID$" permission="view"/>
###     </permissions>
### </api>
def Java():
    javabase = qak.JVM.Runtime.CreateInstance("javabase.javaBase", None)

    age_in_5_years = javabase.Add(int(javabase.getAge), 5)
    result = "Java " + str(javabase.getName) + " will be " + str(age_in_5_years) + " in 5 years and is interested in: \n"
    
    for interest in javabase.getInterests():
        result = result + str(interest) + "\n"
    
    return result

### <api name="Scala">
###     <description>Scala Interop sample</description>
###     <returns> returns an string</returns>
###     <permissions>
###         <group id="$WID$" permission="view"/>
###     </permissions>
### </api>
def Scala():
    scalabase = qak.JVM.Runtime.CreateInstance("scalabase.scalaBase", None)
   
    age_in_5_years = scalabase.Add(int(scalabase.getAge()), 5)
    result = "Scala " + str(scalabase.getName()) + " will be " + str(age_in_5_years) + " in 5 years and is interested in: \n"
    
    for interest in scalabase.getInterests():
        result = result + str(interest) + "\n"
    
    return result