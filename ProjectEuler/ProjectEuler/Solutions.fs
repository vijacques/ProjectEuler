namespace ProjectEuler

module Solutions =
    // hello world
    let sayHello name = "Hello " + name + "!"
    
    // 1
    let sumMultiples numbers =
        List.filter (fun x -> x % 3 = 0 || x % 5 = 0) numbers
        |> List.sum
    let result1 = sumMultiples [1..999]

    // 2
    let rec fib a b =
        if a + b < 4000000 then
            let current = a + b
            let rest = fib b current
            current :: rest
        else
            []
    let sumEvenFib x y = List.sum (List.filter (fun x -> x % 2 = 0) (fib x y))
    let result2 = List.sum (List.filter (fun x -> x % 2 = 0) (fib 0 1))