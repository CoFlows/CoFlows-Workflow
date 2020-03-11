# CoFlows Documentation Samples

**CoFlows CE (Community Edition)** is a polyglot runtime that simplifies the development, hosting and deployment of powerful data-centric workflows. **CoFlows** enables developers to create rich **Web-APIs** with almost **zero boiler plate** and scheduled / reactive processes through a range of languages including CoreCLR (C#, F# and VB), JVM (Java and Scala), Python and Javascript. Furthermore, functions written in any of these languages can call each other within the same process with **full interop**.


A number of APIs are available for developers to use but a notable one is the **M** set which is a **NoSQL persistent and distributed list** in the **QuantApp.Kernel** environment. For more information please see [M](Files/docs/M.md "M").

## Setup  
Install the docker cli tools for Linux containers. Pull the docker public coflows/ce image.

    docker pull coflows/ce

Download a **CoFlows** package from a [sample repo from GitHub](https://github.com/QuantApp/CoFlows-DocSamples) or create your own package. If you create your own package from scratch please read [Workspace](Files/docs/Workspace.md "Workspace"). Ensure a file called _quantapp_config.json_ exists in the folder you are running **CoFlows** in.

    quantapp_config.json
    {
        "Database": { 
            "Connection": "mnt/database.db" ,
            "Type": "sqlite" // **sqlite** for a SQLite db and **mssql** for a MS SQL Server
        },
        "Workspace": "mnt/package.json",
        "Jupyter": false,
        "Server":{
            "Host": "localhost", //Set the host name
            "SecretKey": "26499e5e555e9957725f51cc4d400384", //User key used for Jypter Labs - No need to change
            "LetsEncrypt":{
                "Email": "",
                "Staging": false
            }
        },
        "Cloud":{
            "Host": "coflows.quant.app", //Set the cloud host name
            "SecretKey": "xxx", //Set your cloud secret key (login to CoFlows Cloud, then at the top right click on your name, profile and your secret key will appear)
            "SSL": true
        }
        "AzureContainerInstance": {
            "AuthFile":"mnt/Files/my.azureauth",
            "Dns": "coflows-container",
            "Region": "UKSouth",
            "Cores": 4,
            "Mem": 4,
            "Gpu": {
                "SKU": "", // Empty = no GPU or K80, P100, V100
                "Cores": 1 // 1, 2 or 4 
            }
        }
    }

GPU source https://docs.microsoft.com/en-us/azure/container-instances/container-instances-gpu

az account list-locations -o table

        DisplayName           Latitude    Longitude    Name
        --------------------  ----------  -----------  ------------------
        East Asia             22.267      114.188      eastasia
        Southeast Asia        1.283       103.833      southeastasia
        Central US            41.5908     -93.6208     centralus
        East US               37.3719     -79.8164     eastus
        East US 2             36.6681     -78.3889     eastus2
        West US               37.783      -122.417     westus
        North Central US      41.8819     -87.6278     northcentralus
        South Central US      29.4167     -98.5        southcentralus
        North Europe          53.3478     -6.2597      northeurope
        West Europe           52.3667     4.9          westeurope
        Japan West            34.6939     135.5022     japanwest
        Japan East            35.68       139.77       japaneast
        Brazil South          -23.55      -46.633      brazilsouth
        Australia East        -33.86      151.2094     australiaeast
        Australia Southeast   -37.8136    144.9631     australiasoutheast
        South India           12.9822     80.1636      southindia
        Central India         18.5822     73.9197      centralindia
        West India            19.088      72.868       westindia
        Canada Central        43.653      -79.383      canadacentral
        Canada East           46.817      -71.217      canadaeast
        UK South              50.941      -0.799       uksouth
        UK West               53.427      -3.084       ukwest
        West Central US       40.890      -110.234     westcentralus
        West US 2             47.233      -119.852     westus2
        Korea Central         37.5665     126.9780     koreacentral
        Korea South           35.1796     129.0756     koreasouth
        France Central        46.3772     2.3730       francecentral
        France South          43.8345     2.1972       francesouth
        Australia Central     -35.3075    149.1244     australiacentral
        Australia Central 2   -35.3075    149.1244     australiacentral2
        UAE Central           24.466667   54.366669    uaecentral
        UAE North             25.266666   55.316666    uaenorth
        South Africa North    -25.731340  28.218370    southafricanorth
        South Africa West     -34.075691  18.843266    southafricawest
        Switzerland North     47.451542   8.564572     switzerlandnorth
        Switzerland West      46.204391   6.143158     switzerlandwest
        Germany North         53.073635   8.806422     germanynorth
        Germany West Central  50.110924   8.682127     germanywestcentral
        Norway West           58.969975   5.733107     norwaywest
        Norway East           59.913868   10.752245    norwayeast


## Running  
To run the local server you first need a workspace. Either create your own or download a template. Then just execute the server.sh script of your workspace's _bin_ folder or type:  

    docker run -v $(pwd)/mnt:/app/mnt -p 80:80 -t coflows/ce

when logging in please use:  

    Username: root
    Password: 123

## GitHub Link
Link your **CoFlows** server to automatically pull changes to a project hosted on a GitHub repo by following the instructions of the following link [GitHub](Files/docs/GitLink.md "GitHub").


## CoFlows CLI

            Cloud:

                -Deploy a container to the cloud
                 unix: bin/cloud_deploy.sh cloud
                 win:  bin/bat/cloud_deploy.bat cloud

                -Buid the in the cloud
                 unix: bin/build.sh cloud
                 win:  bin/bat/build.bat cloud

                -Print logs of container in the cloud
                 unix: bin/cloud_log.sh
                 win: bin/bat/cloud_log.bat

                -Restart container in the cloud
                 unix: bin/cloud_restart.sh
                 win: bin/bat/cloud_restart.bat

                -Remove container in the cloud
                 unix: bin/cloud_remove.sh
                 win: bin/bat/cloud_remove.bat

                -Execute query in the cloud
                 unix: bin/query.sh local query_id function_name  parameters[0] ... parameters[n]
                 win:  bin/bat/query.bat local query_id function_name  parameters[0] ... parameters[n]

                -Execute query locally (custom quantapp_config.json file)
                 unix: bin/query_customg.sh {custom_quantapp_config.json} cloud query_id function_name  parameters[0] ... parameters[n]
                 win:  bin/bat/query_custom.bat {custom_quantapp_config.json} cloud query_id function_name  parameters[0] ... parameters[n]

            Local:

                -Buid the code locally
                 unix: bin/build.sh local
                 win:  bin/bat/build.bat local

                -Execute query locally
                 unix: bin/query.sh local query_id function_name  parameters[0] ... parameters[n]
                 win:  bin/bat/query.bat local query_id function_name  parameters[0] ... parameters[n]

                -Execute query locally (custom quantapp_config.json file)
                 unix: bin/query_customg.sh {custom_quantapp_config.json} local query_id function_name  parameters[0] ... parameters[n]
                 win:  bin/bat/query_custom.bat {custom_quantapp_config.json} local query_id function_name  parameters[0] ... parameters[n]


                -Server
                 unix: bin/server.sh
                 win: bin/bat/server.bat

            Azure Container Instances:

                -Deploy to an Azure Container Instance
                 unix: bin/azure_deploy.sh local
                 win:  bin/bat/azure_deploy.bat local

                -Remove an Azure Container Instance
                 unix: bin/azure_remove.sh
                 win:  bin/bat/azure_remove.bat

