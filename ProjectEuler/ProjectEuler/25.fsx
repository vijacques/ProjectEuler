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