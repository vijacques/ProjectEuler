let rec fib (a:bigint) (b:bigint) =
    if (a + b).ToString().Length = 1000 then
        []
    else
        let current = a + b
        let rest = fib b current
        current :: rest

let add x y = x + y

fib 0I 1I
|> List.length
|> add 2 // index starts at 0 and there is one 1 missing