let rec fib a b =
    if a + b < 4000000 then
        let current = a + b
        let rest = fib b current
        current :: rest
    else
        []

List.sum (List.filter (fun x -> x % 2 = 0) (fib 0 1))