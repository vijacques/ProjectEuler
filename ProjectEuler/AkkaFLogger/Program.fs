module AkkaFLogger

open Akka.FSharp
open Microsoft.Diagnostics.Tracing
open System.Threading
open Akka.Actor
open Akka.Event

type Message = 
    | Greet of string

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
    inherit UntypedActor()
    override __.OnReceive msg = 
        match msg with
        | :? InitializeLogger -> ActorBase.Context.Sender.Tell(LoggerInitialized())
        | :? Debug -> AkkaEventSource.Log.Debug(msg.ToString())
        | :? Error -> AkkaEventSource.Log.Error(msg.ToString())
        | :? Info -> AkkaEventSource.Log.Info(msg.ToString())
        | :? Warning -> AkkaEventSource.Log.Warning(msg.ToString())
        | _ -> ignore msg

let config = """
akka {
    loggers = ["AkkaFLogger+LoggingActor, AkkaFLogger"]
    actor.debug.unhandled = on
    log-config-on-start = on
    loglevel = "DEBUG"
            
    actor {
        debug {
            autoreceive = on
            lifecycle = on
            fsm = on
            event-stream = on
        }
    }
}
"""

[<EntryPoint>]
let main argv = 
    let system = System.create "MySistem" (Configuration.parse config)
    
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
    
    let logger = system.ActorOf<LoggingActor>()
    logger <! new InitializeLogger(new LoggingBus())
    logger <! new Warning("abc", string.GetType(), "f# warning!!!")
    greeter <! Greet("FSharp World")
    system.Log.Warning("teste!@!")
    system.Log.Info("information 1")
    Thread.Sleep 1000
    //    System.Console.ReadLine() |> ignore
    0 // return an integer exit code
