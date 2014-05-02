namespace TestStringCalculatorKata
open System

open NUnit.Framework
open FsUnit
open StringCalculatorKata

[<TestFixture>]
type StringCalculatorTests() =
    let Create() = new StringCalculator()

    [<Test>]
    member test.``Add, given empty string, should return 0``() =
        let sc = Create()
        let input = ""
        sc.Add(input) |> should equal 0

    [<Test>]
    member test.``Add, given single char number string, should return number``() =
        let sc = Create()
        let input = "1"
        sc.Add(input) |> should equal 1

    [<Test>]
    member test.``Add, given multi char number string, should return number``() =
        let sc = Create()
        let input = "123"
        sc.Add(input) |> should equal 123

    [<Test>]
    member test.``Add, given two comma delimited numbers in string, should return sum``() =
        let sc = Create()
        let input = "1,2"
        sc.Add(input) |> should equal 3

    [<Test>]
    member test.``Add, given many comma delimited numbers in string, should return sum``() =
        let sc = Create()
        let input = "1,2,25"
        sc.Add(input) |> should equal 28

    [<Test>]
    member test.``Add, given numbers string with comma and newline delimiters, should return sum``() =
        let sc = Create()
        let input = "3,5\n7"
        sc.Add(input) |> should equal 15

    [<Test>]
    member test.``Add, given numbers string with single customer delimiter, should return sum``() =
        let sc = Create()
        let input = "//;\n2;4;6"
        sc.Add(input) |> should equal 12

    [<Test>]
    member test.``Add, given delimited number string with negative number, should throw``() =
        let sc = Create()
        let input = "1,-2,3"
        let ex = Assert.Throws<Exception>(fun () -> sc.Add(input)|> ignore) 
        ex.Message |> should equal "negatives not allowed:-2"

    [<Test>]
    member test.``Add, given delimited number string with negative numbers, should throw``() =
        let sc = Create()
        let input = "1,-2,-3"
        let ex = Assert.Throws<Exception>(fun () -> sc.Add(input)|> ignore) 
        ex.Message |> should equal "negatives not allowed:-2,-3"        

    [<Test>]
    member test.``Add, given delimited number string with large numbers, should filter large numbers and sum``() =
        let sc = Create()
        let input = "1,2,1000,1001,1002"
        sc.Add(input) |> should equal 1003

    [<Test>]
    member test.``Add, given number string with multi char delimiter, should sum``() =
        let sc = Create()
        let input = "//[***]\n5***5***5"
        sc.Add(input) |> should equal 15

    [<Test>]
    member test.``Add, given number string with multiple delimiters, should sum``() =
        let sc = Create()
        let input = "//[*][$][?]\n4*4$4?4"
        sc.Add(input) |> should equal 16

