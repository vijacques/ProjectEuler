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