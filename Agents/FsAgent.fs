module FsAgent

open System

open QuantApp.Kernel
open QuantApp.Engine

let workspaceID = "$WID$"
let pkg(): FPKG =
    {
        ID = workspaceID + "-FsAgent" |> Some
        WorkflowID = workspaceID |> Some
        Code = None
        Name = "F# Agent"
        Description = "F# Agent Sample" |> Some

        MID = None //MID
        Load = (fun data -> ()) |> Utils.Load("$ID$-Load") |> Some
        Add = (fun id data -> ()) |> Utils.Callback("$ID$-Add") |> Some
        Exchange = (fun id data -> ()) |> Utils.Callback("$ID$-Exchange") |> Some
        Remove = (fun id data -> ()) |> Utils.Callback("$ID$-Remove") |> Some

        Body = (fun data -> data) |> Utils.Body("$ID$-Body") |> Some

        ScheduleCommand = "0 * * ? * *" |> Some
        Job = (fun date execType -> ()) |> Utils.Job("$ID$-Job") |> Some
    }
    |> F.ToFPKG