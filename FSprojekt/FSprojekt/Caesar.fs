module Caesar

open System
open System.IO
open System

let alphabet = [| 'A' .. 'Z' |]

let shiftLetter (c : Char) =
    if Array.contains c alphabet then
        let index = Array.findIndex (fun a -> a = c) alphabet 
        match index with
        | 0 -> alphabet.[25]
        | _ -> alphabet.[index - 1]
    else c

let shiftAllLetters (s : string) =
    s |> Seq.map shiftLetter |> String.Concat

let shortenString (s : string) = 
    s |> Seq.indexed |> Seq.filter (fun (i, c) -> i < 80) |> Seq.map (fun (i, c) -> c) |> String.Concat

let rec loop (cipherText : string) c =
    Console.Clear()
    Console.Write (shortenString cipherText + "\n\nIs this correct?\n 1. Yes\n 2. No\n Choice: ")

    let choice = Console.ReadLine()
    match choice with
        | "1" -> Console.Clear(); Console.Write ("\n" + cipherText + "\n\nC = " + string c)
        | "2" -> loop (shiftAllLetters cipherText) (c + 1)
        | _   -> Console.Write "\nChoice does not exist\n\n[Press enter to continue]"; Console.ReadLine() |> ignore; loop cipherText c

    Console.Write "\nPress enter to exit program"
    Console.ReadLine() |> ignore
    exit 0

let decode text =
    loop text 0