let nPower x =
    2I ** x

(nPower 1000).ToString()
|> Seq.map (fun x -> int32(x.ToString()))
|> Seq.sum