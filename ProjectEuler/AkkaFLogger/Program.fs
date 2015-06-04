open Akka.FSharp
open Microsoft.Diagnostics.Tracing

type Message = 
    | Greet of string
    | Warning of string

[<EventSource(Name = "AkkaFs")>]
type ActorEventSource() = 
    inherit EventSource()
    static let mutable log = new ActorEventSource()
    static member Log 
        with get () = log
        and set (v) = log <- v
    
    [<Event(1)>]
    member x.Request(uri : string, methd : string) = x.WriteEvent(1, uri, methd)

[<EntryPoint>]
let main argv = 
    let system = System.create "MySistem" (Configuration.load())
    
    let logger = new ActorEventSource()

    // Use F# computation expression with tail-recursive loop
    // to create an actor message handler and return a reference
    let greeter = 
        spawn system "greeter" (fun mailbox -> 
            let rec loop() = 
                actor { 
                    let! msg = mailbox.Receive()
                    match msg with
                    | Greet who -> printf "Hello, %s!\n" who
                    | Warning msg -> logger.Request("xy", msg)
                    return! loop()
                }
            loop())
    greeter <! Greet("FSharp World")
    greeter <! Warning("f# warning!!!")
    System.Console.ReadLine() |> ignore
    0 // return an integer exit code
