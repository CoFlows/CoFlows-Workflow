def getName():
    return "something"

def Add(x, y):
    return x + y




# C# Interop
import Cs.Base as csb

def Cs():
    csbase = csb.csBase()
    
    age_in_5_years = csbase.Add(csbase.getAge, 5)
    result = "C# " + str(csbase.getName) + " will be " + str(age_in_5_years) + " in 5 years and is interested in:"
    for interest in csbase.getInterests:
        result = result + str(interest) + "\n"
    
    return result


import QuantApp.Kernel as qak

# Permission
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

# Java Interop
def Java():
    javabase = qak.JVM.Runtime.CreateInstance("javabase.javaBase", None)

    age_in_5_years = javabase.Add(int(javabase.getAge), 5)
    result = "Java " + str(javabase.getName) + " will be " + str(age_in_5_years) + " in 5 years and is interested in: \n"
    
    for interest in javabase.getInterests():
        result = result + str(interest) + "\n"
    
    return result

# Scala Interop
def Scala():
    scalabase = qak.JVM.Runtime.CreateInstance("scalabase.scalaBase", None)
   
    age_in_5_years = scalabase.Add(int(scalabase.getAge()), 5)
    result = "Scala " + str(scalabase.getName()) + " will be " + str(age_in_5_years) + " in 5 years and is interested in: \n"
    
    for interest in scalabase.getInterests():
        result = result + str(interest) + "\n"
    
    return result