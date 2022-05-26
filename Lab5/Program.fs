open System
printfn "------------Lab 5---------------\n\n"


module Zad5_1 = 
    Console.WriteLine("\nZad5_1")
    open System.IO

    File.WriteAllText("Zad4_8.txt", "1 5 3\n4 3 1\n7 1 4\n8 7 0")


    let redFile = fun fnam -> 
        // axx+bx+c -> delta > 0 = dwa rozwiazania, delta = 0 jedno, delta < 0 - brak
        let delta = fun (a, b, c) -> b*b - 4.*a*c
        
        fnam |> File.ReadLines
        |> Seq.map (fun l -> l.Split(' ') |> (fun ll -> (ll.[0]|>float),(ll.[1]|>float),(ll.[2]|>float))) 
        |> Seq.toList
        |> fun list -> (
            List.filter (fun abc -> delta abc < 0) list, 
            List.filter (fun abc -> delta abc = 0) list, 
            List.filter (fun abc -> delta abc > 0) list)
    
    "Zad4_8.txt" 
    |> redFile 
    |> fun (l1,l2,l3) -> Console.WriteLine($"brak -> {l1} \njedno -> {l2} \ndwa -> {l3} \n")


module Zad5_4 = 
    Console.WriteLine("\nZad5_4")

    try (Console.ReadLine() |> int |> Some) with | _ -> None 
    |> Option.map ( fun x -> $"{x*x}" )
    |> Option.defaultValue("Nie podałeś wartości")
    |> Console.WriteLine

module Zad5_6 = 
    Console.WriteLine("\nZad5_6")

    let rec read = fun () -> 
        "Podaj wartość:" |> Console.WriteLine 
        Console.ReadLine() |> (fun l -> if l = "" then read() else l) 
    
    try read() |> int |> (fun x -> Ok (x*x)) with | er -> Error er.Message
    |> fun x -> match x with | Error(ex) -> ex | Ok(v) -> v.ToString()
    |> Console.WriteLine

module Zad5_9 = 
    Console.WriteLine("\nZad5_9")

    Console.ReadLine() 
    |> fun l ->  if l.Trim().Length = 0 then None else Some l
    |> Option.map (fun l -> (try (l |> int |> fun x -> Ok (x*x)) with | er -> Error er))
    |> fun x -> match x with None -> "Nie podałeś wartości" | Some r -> match r with | Error(ex) -> ex.Message | Ok(v) -> v.ToString()
    |> Console.WriteLine


module Zad5_11 = 
    Console.WriteLine("\nZad5_11")

    let read_value = fun () -> Console.ReadLine().Trim()
    let parse_value = fun x -> try (x |> int |> Ok ) with | er -> Error er

    "podaj liczby:" |> Console.WriteLine 

    let ResToString = function
    | Error s -> $"Error: {s}"
    | Ok s -> $"Wynik: {s}"

    Some (read_value ()) 
    |> Option.bind (fun s -> if s = "" then None else (parse_value s) |> Some )
    |> Option.bind (fun s -> (s, (read_value())) |> Some)
    |> Option.bind (fun (a, b) -> if b = "" then None else (a, parse_value b) |> Some )
    |> Option.map (fun ab -> 
        match ab with 
        | (Ok a, Ok b) -> Ok (a+b) 
        | (Error a, _) -> Error $"Param1: {a.Message}" 
        | (_, Error b) -> Error $"Param2: {b.Message}"
    )
    |> Option.defaultValue(Error "Nie podałeś wartości")
    |> ResToString |> Console.WriteLine 
    