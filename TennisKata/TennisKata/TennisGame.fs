namespace TennisKata


type TennisScores =
    | LOVE = 0
    | FIFTEEN = 15
    | THIRTY = 30
    | FORTY = 40
    | GAME = 100
    | ADV = 200

type TennisGame() =
    let mutable currentScore = TennisScores.LOVE, TennisScores.LOVE

    let calculateScore current =
        match current with 
            | (TennisScores.LOVE, x) -> (TennisScores.FIFTEEN, x)
            | (TennisScores.FIFTEEN, x) -> (TennisScores.THIRTY, x)
            | (TennisScores.THIRTY, x) -> (TennisScores.FORTY, x)
            | (TennisScores.FORTY, TennisScores.FORTY) -> (TennisScores.ADV, snd current)
            | (TennisScores.FORTY, x) 
            | (TennisScores.ADV, x)  -> (TennisScores.GAME, x)
            | (_, _) -> current 

    member this.ScoreToService() =
        currentScore <- calculateScore currentScore

    member this.ScoreToReceiver() =
        let reverse = (snd currentScore, fst currentScore)
        let result = calculateScore reverse
        currentScore <- (snd result, fst result)

    member this.CurrentScore() =
        match currentScore with 
            | (TennisScores.GAME, x) -> "GAME TO SERVICE"
            | (x, TennisScores.GAME) -> "GAME TO RECEIVER"
            | (TennisScores.FORTY, TennisScores.FORTY) -> "DEUCE"
            | (x, y) -> if (x=y) then
                            x.ToString() + " ALL"
                        else 
                            x.ToString() + " - " + y.ToString() 

                            