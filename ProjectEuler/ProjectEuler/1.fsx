let numbers = [1..999]
let multiples =
    List.filter (fun x -> x % 3 = 0 || x % 5 = 0) numbers
List.sum multiples