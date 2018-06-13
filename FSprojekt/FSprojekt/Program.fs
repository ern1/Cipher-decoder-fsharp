open System
open System.IO

[<EntryPoint>]
let main argv = 
    //printfn "%A" argv

    let readFile f =
        let path = Path.GetFullPath(f)
        File.ReadAllText(path)
    
    Console.Write ("Choose assignment \n 1. Caesar Cipher\n 2. Frequency Analysis\n Choice: ")

    let choice = Console.ReadLine()
    match choice with
        | "1" -> Caesar.decode (readFile "TEXT_1.txt")
        | "2" -> Freq.decode (readFile "TEXT_2.txt")
        | _   -> Console.Write "\nChoice does not exist\n\n[Press enter to exit]";

    Console.ReadLine() |> ignore
    0 // return an integer exit code
