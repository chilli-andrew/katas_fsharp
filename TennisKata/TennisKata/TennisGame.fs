namespace TennisKata
open Microsoft.FSharp.Reflection

type TennisScores = | LOVE | FIFTEEN| THIRTY | FORTY | GAME | ADV

type TennisGame() =
    ///Returns the case name of the object with union type 'ty.
    let GetUnionCaseName (x:'a) = 
        match FSharpValue.GetUnionFields(x, typeof<'a>) with
        | case, _ -> case.Name  

    let mutable currentScore = TennisScores.LOVE, TennisScores.LOVE

    let calculateScore current =
        match current with 
            | (LOVE, x) -> (FIFTEEN, x)
            | (FIFTEEN, x) -> (THIRTY, x)
            | (THIRTY, x) -> (FORTY, x)
            | (FORTY, FORTY) -> (ADV, snd current)
            | (FORTY, x) 
            | (ADV, x)  -> (GAME, x)
            | (_, _) -> current 

    member this.ScoreToService() =
        currentScore <- calculateScore currentScore

    member this.ScoreToReceiver() =
        let reverse = (snd currentScore, fst currentScore)
        let result = calculateScore reverse
        currentScore <- (snd result, fst result)

    member this.CurrentScore() =
        match currentScore with 
            | (GAME, x) -> "GAME TO SERVICE"
            | (x, GAME) -> "GAME TO RECEIVER"
            | (FORTY, FORTY) -> "DEUCE"
            | (x, y) -> 
                        let xname = x |> GetUnionCaseName
                        if (x=y) then xname + " ALL"
                        else 
                            xname + " - " + (y |> GetUnionCaseName)

                            