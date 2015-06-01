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