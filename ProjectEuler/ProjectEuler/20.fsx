let rec factorial n =
    if n = 0I || n = 1I then 1I
    else n * factorial(n-1I)

(factorial 100I).ToString()
|> Seq.map (fun x -> int32(x.ToString()))
|> Seq.sum