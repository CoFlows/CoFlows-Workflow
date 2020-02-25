var qengine = importNamespace('QuantApp.Engine')

let workspaceID = '$WID$'

let pkg = new qengine.FPKG(
    workspaceID + '-JavascriptAgent', //ID
    workspaceID, //Workspace ID
    'Javascript Agent', //Name
    'Javascript Agent Sample', //Description
    null, //MID
    jsWrapper.Load('Load', function(data){ }),
    jsWrapper.Callback('Add', function(id, data){ }), 
    jsWrapper.Callback('Exchange', function(id, data){ }), 
    jsWrapper.Callback('Remove', function(id, data){ }), 
    jsWrapper.Body('Body', function(data){ return data }), 
    '0 * * ? * *', //Cron Schedule
    jsWrapper.Job('Job', function(date, data){ })
    )