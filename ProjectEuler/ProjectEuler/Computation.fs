module Computation

// logging
type LoggingBuild() =
    let log p = printfn "expression is %A" p

    member this.Bind(x, f) =
        log x
        f x

    member this.Return(x) =
        x

let logger = new LoggingBuild()

let loggedWorkflow =
    logger
        {
        let! x = 42
        let! y = 43
        let! z = x + y
        return z
        }

// safe division
let divideBy bottom top =
    if bottom = 0
    then None
    else Some(top/bottom)

type MaybeBuilder() =
    member this.Bind(x, f) =
        match x with
        | None -> None
        | Some a -> f a
        //Option.bind f x
    member this.Return(x) =
        Some x

let maybe = new MaybeBuilder()

let divideByWorkflow init x y z =
    maybe
        {
        let! a = init |> divideBy x        
        let! b = a |> divideBy y
        let! c = b |> divideBy z
        return c
        }

let good = divideByWorkflow 12 3 2 1
let bad = divideByWorkflow 12 3 0 1

// else chain
type OrElseBuilder() =
    member this.ReturnFrom(x) = x
    member this.Combine(a,b) =
        match a with
        | Some _ -> a
        | None -> b
    member this.Delay(f) = f()

let orElse = new OrElseBuilder()

let map1 = [ ("1","One"); ("2","Two") ] |> Map.ofList
let map2 = [ ("A","Alice"); ("B","Bob") ] |> Map.ofList
let map3 = [ ("CA","California"); ("NY","New York") ] |> Map.ofList

let multiLookup key = orElse {
    return! map1.TryFind key
    return! map2.TryFind key
    return! map3.TryFind key
    }

multiLookup "A" |> printfn "Result for A is %A" 
multiLookup "CA" |> printfn "Result for CA is %A" 
multiLookup "X" |> printfn "Result for X is %A" 

// asynchronous calls with callbacks
open System.Net

let req1 = HttpWebRequest.Create("http://tryfsharp.org")
let req2 = HttpWebRequest.Create("http://google.com")
let req3 = HttpWebRequest.Create("http://bing.com")

async {
    use! resp1 = req1.AsyncGetResponse()  
    printfn "Downloaded %O" resp1.ResponseUri

    use! resp2 = req2.AsyncGetResponse()  
    printfn "Downloaded %O" resp2.ResponseUri

    use! resp3 = req3.AsyncGetResponse()  
    printfn "Downloaded %O" resp3.ResponseUri

    } |> Async.RunSynchronously

// exercise
open System

let strToInt str = 
    match Int32.TryParse(str) with
    | true, x -> Some x
    | false, _ -> None

type IntBuilder() =
    member this.Bind(x, f) = Option.bind f x
    member this.Return(x) = Some x

let intb = new IntBuilder()

let stringAddWorkFlow x y z =
    intb
        {
        let! a = strToInt x
        let! b = strToInt y
        let! c = strToInt z
        return a + b + c
        }

let good10 = stringAddWorkFlow "3" "5" "2"
let bad10 = stringAddWorkFlow "3" "5" "B"

let strAdd str i =
    intb
        {
        let! a = strToInt str
        return a + i
        }

let (>>=) m f = Option.bind f m

let good6 = strToInt "1" >>= strAdd "2" >>= strAdd "3"
let bad6 = strToInt "1" >>= strAdd "xyz" >>= strAdd "3"