namespace StringCalculatorKata
open System
open NUnit.Framework
open FsUnit

type StringCalculator() =
    let split (delimiters: string[]) (input: string) =
        input.Split(delimiters, StringSplitOptions.None)

    let getDelimiterSection (input:string) =
        if input.StartsWith("//") then
            input.Substring(0, input.IndexOf("\n") + 1)
        else
            ""
    let getNumberSection (input:string) (delimiterSection: string) =
        if delimiterSection = "" then input
        else
            input.Replace(delimiterSection, "")

    let getCustomDelimiters (delimiterSection:string) =
        delimiterSection.Replace("//", "").Replace("\n","").TrimStart('[').TrimEnd(']') 
            |> split [|"]["|] 
            |> Array.toList

    let getDelimiters (delimiterSection: string) =
        let defaultDelimiters = [",";"\n"]
        if delimiterSection = "" then Seq.toArray defaultDelimiters
        else
            let customDelimiters = delimiterSection |> getCustomDelimiters
            let allDelimiters = customDelimiters @ defaultDelimiters
            Seq.toArray customDelimiters
 
    let concatNegatives negatives =
        (negatives |> Seq.map (fun n -> n.ToString()) |> String.concat ",")

    let checkForNegatives numbers =
        let negatives = Seq.where (fun n -> n<0) numbers
        if (Seq.length negatives = 0) then numbers
        else
            let errorMessage = "negatives not allowed:" +  (concatNegatives negatives)
            failwith errorMessage
    let filterLargeNumbers numbers =
        numbers |> Seq.filter (fun n -> n<=1000)
                              
    let doAdd (input: string) =
        let delimiterSection = getDelimiterSection input
        let numberSection = getNumberSection input delimiterSection
        let delimiters = getDelimiters delimiterSection
        
        split delimiters numberSection
        |> Seq.map Int32.Parse
        |> checkForNegatives
        |> filterLargeNumbers
        |> Seq.sum

    member this.Add(input:string) =
        match input with 
            | "" -> 0
            | _ -> doAdd input

