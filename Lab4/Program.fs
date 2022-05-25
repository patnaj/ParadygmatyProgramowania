open System
printfn "------------Lab 4---------------\n\n"


module Zad4_1 = 
    Console.WriteLine("\nZad4_1")

    type Lista<'a> = 
        | Pusta
        | Wezel of 'a*Lista<'a>
    
    let nPierwszych (n) = 
        let rec recur = fun (i, n) -> if i < n then Wezel(i, recur (i+1, n)) else Pusta
        recur (0 ,n)

    /// ------- rozwiązanie
    let rec mapuj map_fun = function
    | Pusta -> Pusta
    | Wezel(v, n) -> Wezel(map_fun v, mapuj (map_fun) n)
    /// ------- rozwiązanie

    let list = nPierwszych (3)
    Console.WriteLine($"{list}")

    let list2 = mapuj (fun x -> x * x) list
    Console.WriteLine($"{list2}")

    let fff = fun x -> x |> float
    let list3 = mapuj fff list
    Console.WriteLine($"{list3}")
    
    type TTT = {abc:int}
    let list4 = mapuj (fun x -> {abc=x} ) list
    Console.WriteLine($"{list4}")


module Zad4_2 = 
    Console.WriteLine("\nZad4_2")

    type Drzewo<'a> = 
        | Puste
        | Wezel of 'a*Drzewo<'a>*Drzewo<'a>
    
    let rec Dodaj (v:'a, d: Drzewo<'a>) :Drzewo<'a> =  
        match d with
        | Puste -> Wezel(v, Puste, Puste)
        | Wezel(wv, l, p) when wv <= v -> Wezel(wv, l ,Dodaj (v, p) )
        | Wezel(wv, l, p) -> Wezel(wv, Dodaj (v, l) , p )

    let drzewo = Dodaj(5, Dodaj(10,Dodaj(2,Dodaj(3, Puste))))    
    Console.WriteLine($"{drzewo}")

    /// ------- rozwiązanie
    let rec agregacja (agr_fun, agr_val) = function
    | Puste -> agr_val
    | Wezel(v, l, p) -> 
        let val_1 = (agr_fun agr_val v)
        let val_2 = agregacja (agr_fun, val_1) l
        agregacja (agr_fun, val_2) p
    /// ------- rozwiązanie

    let run = fun f -> agregacja (f, 0) drzewo

    Console.WriteLine($"\t(+) -> {run(+)}")
    Console.WriteLine($"\t(-) -> {run(-)}")
    Console.WriteLine($"\t(/) -> {run(fun v wynik->v*2+wynik)}")

    let wynik_string = agregacja ((fun a b -> $"{b} <-> {a}"), "") drzewo
    Console.WriteLine($"\t(str) -> {wynik_string}")

module Zad4_4 = 
    Console.WriteLine("\nZad4_4")
    open Zad4_2

    let list = [1;2;-5;4;9;-1]
    Console.WriteLine($"{list}")

    /// ------- rozwiązanie
    let rec lista_na_drzewo (d:Drzewo<'a>) = function
        | [] -> d
        | v::n -> lista_na_drzewo (Dodaj ( v, d )) n
    /// ------- rozwiązanie
   
    let drzewko = lista_na_drzewo Puste list
    Console.WriteLine($"{drzewko}")


module Zad4_8 = 
    Console.WriteLine("\nZad4_8")
    open System.IO

    File.WriteAllText("Zad4_8.txt", "1 5 3\n4 3 1\n7 1 4\n8 7 0")

    
    let redFile = fun fnam -> 
        let lines_to_points = fun (l:String) -> 
            let l = l.Split(' ')
            (l.[0]|>float),(l.[1]|>float),(l.[2]|>float)
        Seq.toList( Seq.map lines_to_points (File.ReadLines fnam) )

    let parametry = redFile "Zad4_8.txt"
    Console.WriteLine($"parametry -> {parametry}")

    // axx+bx+c -> delta > 0 = dwa rozwiazania, delta = 0 jedno, delta < 0 - brak
    let delta = fun (a, b, c) -> b*b - 4.*a*c

    let dwa_rozwiazania = List.filter (fun abc -> delta abc > 0) parametry  
    let jedno_rozwiazanie = List.filter (fun abc -> delta abc = 0) parametry  
    let zero_rozwiazan = List.filter (fun abc -> delta abc < 0) parametry  
    Console.WriteLine($"jedno -> {jedno_rozwiazanie}")
    Console.WriteLine($"dwa -> {dwa_rozwiazania}")
    Console.WriteLine($"zero -> {zero_rozwiazan}")


module Zad4_13 = 
    Console.WriteLine("\nZad4_13")

    let rand = new System.Random()
    let rand_lsit = [for i in 1..1000 -> rand.Next(-10, 10)]
    Console.WriteLine($"rand {rand_lsit}")

    let count = fun v -> (List.filter (fun i -> i = v) rand_lsit).Length
    let map = Map.ofList([for i in -10..10->(i, count i)])
    Console.WriteLine($"map {map}")
