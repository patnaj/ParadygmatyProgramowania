open System
printfn "------------Lab 3---------------\n\n"


module Zad3_1 = 
    Console.WriteLine("\nZad3_1")

    type Lista<'a> = 
    | Pusta
    | Wezel of 'a*Lista<'a>

    let nPierwszych (n) = 
        let rec recur = fun (i, n) -> if i < n then Wezel(i, recur (i+1, n)) else Pusta
        recur (0 ,n)

    Console.WriteLine($"(3)->{nPierwszych 3}")

