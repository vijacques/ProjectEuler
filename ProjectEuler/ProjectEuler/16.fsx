let nPower x =
    2I ** x
let nPowerSum x =
    (nPower x).ToString()
    |> Seq.map (fun x -> int32(x.ToString()))
    |> Seq.sum
let result16 = nPowerSum 1000