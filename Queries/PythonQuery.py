def getName():
    return "something"

def Add(x, y):
    return x + y

# C# Interop
# import clr
import Cs.Base as csb

def Cs():
    csbase = csb.csBase()
    
    age_in_5_years = csbase.Add(csbase.getAge, 5)
    result = "C# " + str(csbase.getName) + " will be " + str(age_in_5_years) + " in 5 years and is interested in:"
    for interest in csbase.getInterests:
        result = result + str(interest) + "\n"
    
    return result


import QuantApp.Kernel as qak

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