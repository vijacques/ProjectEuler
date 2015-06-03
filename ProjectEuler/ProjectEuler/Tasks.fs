module Tasks

open System
open System.Threading
open System.Threading.Tasks

//[<EntryPoint>]
//let main argv = 
let work = 
    async { 
        for i in 0..2 do
            printfn "Work loop is currently %i" i
            do! Async.Sleep 1000
    }
    
let rec sum x y = 
    match x with
    | 0 -> y
    | _ -> 
        let total = x + y
        sum (x - 1) total
    
let sumTask = 
    async { 
        do! Async.Sleep 5000
        return sum 1000 0
    }
    
let asyncWorkflow = 
    async { 
        let task = Async.StartAsTask work
        let! result = Async.AwaitTask task
        printfn "task1 completed"
    }
    
let asyncWorkflow2 = 
    async { 
        let task2 = Async.StartAsTask sumTask
        let! result2 = Async.AwaitTask task2
        printfn "task2  completed"
        return result2
    }
    
let job5 = 
    async { 
        let tcs = TaskCompletionSource<int>()
        Async.Start(async { 
                        do! Async.Sleep 1000
                        tcs.SetResult(sum 1000 0)
                    })
        return! (Async.AwaitTask tcs.Task)
    }
    
let cancellationSource = new CancellationTokenSource()
Async.Start(asyncWorkflow, cancellationSource.Token) |> ignore
//    cancellationSource.Cancel()
let finalResult2 = Async.RunSynchronously asyncWorkflow2
printfn "Task result is: %i" finalResult2
let finalResult3 = Async.RunSynchronously job5
printfn "tcs result is: %i" finalResult3
Console.ReadLine() |> ignore
0
