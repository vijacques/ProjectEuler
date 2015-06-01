module ProjectEuler.Tests

open NUnit.Framework
open FsUnit
open ProjectEuler.Solutions

[<Test>]
let ``Should say hello`` () =
    Assert.AreEqual("Hello world!", sayHello "world")

[<Test>]
let ``Solution 1`` () =
    Assert.AreEqual(233168, sumMultiples [1..999])

[<Test>]
let ``Solution 2`` () =
    Assert.AreEqual(4613732, sumEvenFib 0 1)

[<Test>]
let ``Solution 6`` () =
    Assert.AreEqual(25164150, squareSumMinusSumSquares 100)

[<Test>]
let ``Solution 14`` () =
    Assert.AreEqual(351, maxSizeSequence [1..99999])

[<Test>]
let ``Solution 16`` () =
    Assert.AreEqual(1366, nPowerSum 1000)

[<Test>]
let ``Solution 20`` () =
    Assert.AreEqual(648, factorialSum 100I)

[<Test>]
let ``Solution 25`` () =
    Assert.AreEqual(4782, fibLength4 0I 1I)