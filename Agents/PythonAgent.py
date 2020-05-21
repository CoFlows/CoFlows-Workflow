import clr

import System
import QuantApp.Kernel as qak
import QuantApp.Engine as qae

workspaceID = "$WID$"

def Add(id, data):
    pass

def Exchange(id, data):
    pass

def Remove(id, data):
    pass
    
def Load(data):
    pass
    
def Body(data):
    return data

def Job(timestamp, data):
    pass

def pkg():
    return qae.FPKG(
    workspaceID + "-PythonAgent", #ID
    workspaceID, #Workflow ID
    "Python Agent", #Name
    "Python Agent Sample", #Description
    None, #M ID Listener
    qae.Utils.SetFunction("Load", qae.Load(Load)), 
    qae.Utils.SetFunction("Add", qak.MCallback(Add)), 
    qae.Utils.SetFunction("Exchange", qak.MCallback(Exchange)), 
    qae.Utils.SetFunction("Remove", qak.MCallback(Remove)), 
    qae.Utils.SetFunction("Body", qae.Body(Body)), 
    "0 * * ? * *", #Cron Schedule
    qae.Utils.SetFunction("Job", qae.Job(Job))
    )