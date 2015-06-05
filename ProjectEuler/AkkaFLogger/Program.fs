module AkkaFLogger

open Akka.FSharp
open Microsoft.Diagnostics.Tracing
open System.Threading

type Message = 
    | Greet of string

type Event = 
    | Debug of string
    | Error of string
    | Info of string
    | Warning of string

[<EventSource(Name = "AkkaFs", Guid = "{06c2f7ff-69b6-4214-abd5-14ba1c5da651}")>]
[<Sealed>]
type AkkaEventSource() = 
    inherit EventSource()
    static let mutable log = new AkkaEventSource()
    
    static member Log 
        with get () = log
        and set (v) = log <- v
    
    [<Event(1)>]
    member x.Debug(message : string) = 
        if log.IsEnabled() then x.WriteEvent(1, message)
    
    [<Event(2)>]
    member x.Error(message : string) = 
        if log.IsEnabled() then x.WriteEvent(2, message)
    
    [<Event(3)>]
    member x.Info(message : string) = 
        if log.IsEnabled() then x.WriteEvent(3, message)
    
    [<Event(4)>]
    member x.Warning(message : string) = 
        if log.IsEnabled() then x.WriteEvent(4, message)

type LoggingActor() = 
    inherit Actor()
    override __.OnReceive msg = 
        match msg with
        | :? Akka.Event.Debug -> AkkaEventSource.Log.Debug(msg.ToString())
        | :? Akka.Event.Error -> AkkaEventSource.Log.Error(msg.ToString())
        | :? Akka.Event.Info -> AkkaEventSource.Log.Info(msg.ToString())
        | :? Akka.Event.Warning -> AkkaEventSource.Log.Warning(msg.ToString())

[<EntryPoint>]
let main argv = 
    let system = System.create "MySistem" (Configuration.load())
    
    //    let logger = system.ActorOf<LoggingActor>()

    // Use F# computation expression with tail-recursive loop
    // to create an actor message handler and return a reference
    let greeter = 
        spawn system "greeter" (fun mailbox -> 
            let rec loop() = 
                actor { 
                    let! msg = mailbox.Receive()
                    match msg with
                    | Greet who -> printf "Hello, %s!\n" who
                    return! loop()
                }
            loop())
    
    let logger = 
        spawn system "logger" (fun mailbox -> 
            let rec loop() = 
                actor { 
                    let! msg = mailbox.Receive()
                    match msg with
                    | Debug msg -> AkkaEventSource.Log.Debug(msg)
                    | Error msg -> AkkaEventSource.Log.Error(msg)
                    | Info msg -> AkkaEventSource.Log.Info(msg)
                    | Warning msg -> AkkaEventSource.Log.Warning(msg)
                    return! loop()
                }
            loop())
    
    greeter <! Greet("FSharp World")
    logger <! Warning("f# warning!!!")
    Thread.Sleep 1000
    //    System.Console.ReadLine() |> ignore
    0 // return an integer exit code
