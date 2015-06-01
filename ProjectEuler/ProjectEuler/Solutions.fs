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

    // 6
    let sumSquares x =
        let numbers = [1..x]
        List.map (fun x -> x * x) numbers
        |> List.sum
    let squareSum x =
        let numbers = [1..x]
        let sum = (List.sum numbers)
        sum * sum
    let squareSumMinusSumSquares x = squareSum x - sumSquares x
    let result6 = squareSumMinusSumSquares 100

    // 14
    let sequence n =
        n
        |> Seq.unfold(fun x -> match x with
                                | x when x = 0 -> None
                                | x when x = 1 -> Some(1, 0)
                                | x when x % 2 = 0 -> Some(x, x/2)
                                | _ -> Some(x, 3*x+1))
    let maxSizeSequence x =
        List.map sequence x
        |> Seq.map (Seq.length)
        |> Seq.max
    let result14 = maxSizeSequence [1..99999]  //999999 freezes

    // 16
    let nPower x =
        2I ** x
    let nPowerSum x =
        (nPower x).ToString()
        |> Seq.map (fun x -> int32(x.ToString()))
        |> Seq.sum
    let result16 = nPowerSum 1000

    // 20
    let rec factorial n =
        if n = 0I || n = 1I then 1I
        else n * factorial(n-1I)
    let factorialSum x =
        (factorial x).ToString()
        |> Seq.map (fun x -> int32(x.ToString()))
        |> Seq.sum
    let result20 = factorialSum 100I

    // 25
    let rec fibLength (a:bigint) (b:bigint) =
        if (a + b).ToString().Length = 1000 then
            []
        else
            let current = a + b
            let rest = fibLength b current
            current :: rest
    let add x y = x + y
    let fibLength4 x y =
        fibLength x y
        |> List.length
        |> add 2 // index starts at 0 and there is one 1 missing
    let result25 = fibLength4 0I 1I