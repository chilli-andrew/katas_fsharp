namespace TennisKata

open NUnit.Framework
open FsUnit

[<TestFixture>]
type TestTennisGame() = 
    let Create() = new TennisGame()

    let scoreToService (times: int) (game: TennisGame)  =
        for i in 1..times do
            game.ScoreToService()
        game

    let scoreToReceiver (times: int) (game: TennisGame)  =
        for i in 1..times do
            game.ScoreToReceiver()
        game

    let deuceGame (game: TennisGame) =
        game 
            |> scoreToService 3 
            |> scoreToReceiver 3

    let advantageToService (game: TennisGame) =
        game 
            |> deuceGame 
            |> scoreToService 1

    let advantageToReceiver (game: TennisGame) =
        game |> deuceGame 
            |> scoreToReceiver 1

    [<Test>]
    member test.``CurrentScore, when game starts, should return LOVE ALL``() = 
        let game = Create()
        let expected = "LOVE ALL"
        game.CurrentScore() |> should equal expected

    [<Test>]
    member test.``ScoreToService, when called once on new game, should set CurrentScore to FIFTEEN - LOVE``() = 
        let game = Create()
        game |> scoreToService 1 |> ignore
        let expected = "FIFTEEN - LOVE"
        game.CurrentScore() |> should equal expected

    [<Test>]
    member test.``ScoreToService, when called twice on new game, should set CurrentScore to THIRTY - LOVE``() = 
        let game = Create()
        game |> scoreToService 2 |> ignore
        let expected = "THIRTY - LOVE"
        game.CurrentScore() |> should equal expected

    [<Test>]
    member test.``ScoreToService, when called 3 times on new game, should set CurrentScore to FORTY - LOVE``() = 
        let game = Create()
        game |> scoreToService 3 |> ignore
        let expected = "FORTY - LOVE"
        game.CurrentScore() |> should equal expected

    [<Test>]
    member test.``ScoreToService, when called 4 times on new game, should set CurrentScore to GAME TO SERVICE``() = 
        let game = Create()
        game |> scoreToService 4 |> ignore
        let expected = "GAME TO SERVICE"
        game.CurrentScore() |> should equal expected

    [<Test>]
    member test.``ScoreToReceiver, when called once on new game, should set CurrentScore to LOVE - FIFTEEN``() = 
        let game = Create()
        game |> scoreToReceiver 1 |> ignore
        let expected = "LOVE - FIFTEEN"
        game.CurrentScore() |> should equal expected

    [<Test>]
    member test.``ScoreToReceiver, when called twice on new game, should set CurrentScore to LOVE - THIRTY``() = 
        let game = Create()
        game |> scoreToReceiver 2 |> ignore
        let expected = "LOVE - THIRTY"
        game.CurrentScore() |> should equal expected

    [<Test>]
    member test.``ScoreToReceiver, when called 3 times on new game, should set CurrentScore to LOVE - FORTY``() = 
        let game = Create()
        game |> scoreToReceiver 3 |> ignore
        let expected = "LOVE - FORTY"
        game.CurrentScore() |> should equal expected

    [<Test>]
    member test.``ScoreToReceiver, when called 4 times on new game, should set CurrentScore to GAME TO RECEIVER``() = 
        let game = Create()
        game |> scoreToReceiver 4 |> ignore
        let expected = "GAME TO RECEIVER"
        game.CurrentScore() |> should equal expected


    [<Test>]
    member test.``ScoreToService and ScoreToReceiver, when called 3 times each on new game, should set CurrentScore to DEUCE``() = 
        let game = Create()
        game |> deuceGame |> ignore
        let expected = "DEUCE"
        game.CurrentScore() |> should equal expected
    
    [<Test>]
    member test.``ScoreToService, when deuce, should set CurrentScore to ADV - FORTY``() = 
        let game = Create()
        game |> deuceGame |> ignore
        game.ScoreToService()
        let expected = "ADV - FORTY"
        game.CurrentScore() |> should equal expected    

    [<Test>]
    member test.``ScoreToService, when advantage to service, should set CurrentScore to GAME TO SERVICE``() = 
        let game = Create()
        game |> advantageToService |> ignore
        game.ScoreToService()
        let expected = "GAME TO SERVICE"
        game.CurrentScore() |> should equal expected
            
    [<Test>]
    member test.``ScoreToReceiver, when advantage to receiver, should set CurrentScore to GAME TO RECEIVER``() = 
        let game = Create()
        game |> advantageToReceiver |> ignore
        game.ScoreToReceiver()
        let expected = "GAME TO RECEIVER"
        game.CurrentScore() |> should equal expected      
        
    [<Test>]
    member test.``ScoreToService, ScoreToReceiver, ScoreToReceiver, should set CurrentScore to FIFTEEN - THIRTY``() = 
        let game = Create()
        game 
            |> scoreToService 1 
            |> scoreToReceiver 2 
            |> ignore
        let expected = "FIFTEEN - THIRTY"
        game.CurrentScore() |> should equal expected  