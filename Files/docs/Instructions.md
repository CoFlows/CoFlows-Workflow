# Instructions

## Notes:
* If ID in package is empty then build.sh will auto generate an ID
* build.sh will add new entries to the Files, Queries, Agents and Base sections of the package.json. It will not delete entries, this needs to be done manually.
* $WID$ is code to populate the Workflow ID in queries and agents. NOTE: Files will be overwritten once with the replacements.
  

## Steps

### Download base project
Download the github repo

    git clone https://github.com/CoFlows/CoFlows-Workflow.git

### Build
Build the workflow

    sh build.sh local

This steps populats the Base, Agents and Queries sections of the **package.json** file. It also pulls all dependencies declared in the nuget, jars and pip sections of the **package.json** entries.

### Execute
Run the server in local mode

    sh server.sh localhost.json



