let sequence n =
    n
    |> Seq.unfold(fun x -> match x with
                            | x when x = 0 -> None
                            | x when x = 1 -> Some(1, 0)
                            | x when x % 2 = 0 -> Some(x, x/2)
                            | _ -> Some(x, 3*x+1))

List.map sequence [1..99999] //999999 freezes
|> Seq.map (Seq.length)
|> Seq.max