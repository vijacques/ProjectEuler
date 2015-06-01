let sumMultiples numbers =
    List.filter (fun x -> x % 3 = 0 || x % 5 = 0) numbers
    |> List.sum
let result1 = sumMultiples [1..999]