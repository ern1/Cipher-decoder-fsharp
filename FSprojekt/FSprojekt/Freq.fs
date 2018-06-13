module Freq

open System
open System.IO

let charByFreq = Seq.map (fun c -> if Char.IsLetter c then Char.ToUpper c else c) """ etaoinsrhldcumfgpywb,.vk-"_'x)(;0j1q=2:z/*!?$35>{}49[]867\+|&<%@#^`~""" |> Seq.toArray    // Character Frequency

let swapChars c1 c2 (s : string) =
    s |> Seq.toList |> Seq.map (fun c -> match c with | c when c = c1 -> c2 | c when c = c2 -> c1 | _ -> c) |> String.Concat

// Takes string, returns array with the most frequent letters first
let sortByFreq (s : string) = 
    s |> Seq.toArray 
      |> Array.filter (fun x -> Array.contains x charByFreq)
      |> Array.countBy (fun x -> x) 
      |> Array.sortBy (fun (x, y) -> (-y, x)) 
      |> Array.map (fun (x, y) -> x)

// Takes a string (TEXT_2 in this case), returns a new string based on the frequency table
let newStringByCharFreq (s : string) =

    // These arrays will only contain the charachters used in the text
    let usedCharFreq = sortByFreq s                                                 // Sorted by how often they are used in the text that are to be decrypted                                                                                  
    let charFreq = Array.filter (fun c -> Array.contains c usedCharFreq) charByFreq // Sorted by how often they are used in english writing

    let replaceCharByFreq (c : Char) =
        if Array.contains c charFreq then
            let index = Array.findIndex (fun a -> a = c) usedCharFreq // index = how frequent the charachter occurs in the text
            charFreq.[index]                                          // return the charachters that occurs with the same frequency in english writing
        else c

    s |> Seq.map replaceCharByFreq |> String.Concat

// Not used
let shortenString (s : string) = 
    s |> Seq.indexed |> Seq.filter (fun (i, c) -> i < 1200) |> Seq.map (fun (i, c) -> c) |> String.Concat

(*let writeToFile (s : String) =
    use sw = new StreamWriter(@"c:\temp\test.txt")
    sw.WriteLine(s) *)

let rec loop text =
    Console.Clear()
    Console.Write (string text)

    Console.Write ("\n\nEnter characters to swap: ")
    let chars = Console.ReadLine() |> Seq.toArray
    
    match chars with
        | [|c1; c2|] -> loop (swapChars (Char.ToUpper c1) (Char.ToUpper c2) text)
        | [||]       -> Console.Clear(); Console.Write (text + "\n\n[Press enter to exit program]");
        | _          -> Console.Write "\nFaulty input\n\n[Press enter to continue]"; Console.ReadLine() |> ignore; loop text

    Console.ReadLine() |> ignore
    exit 0

let decode text = 
    Console.Write "\nInstructions:\nYou will be shown a partially decrypted text. Your job is to swap characters until it is fully decrypted.\n\nTo swap two characters, enter both characters (no spaces) and press enter. Repeat until done, then press enter without inputting any characters.\n\n[Press any key to continue]"
    Console.ReadLine() |> ignore

    // Perform some operations using frequency tables
    let text1 = text |> newStringByCharFreq

    // Send it in to a loop where user help decrypt it
    loop text1