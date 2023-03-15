open System
printfn "------------Lab 3---------------\n\n"


module Zad3_1 = 
    Console.WriteLine("\nZad3_1")

    type Lista<'a> = 
        | Pusta
        | Wezel of 'a*Lista<'a>
        // odkomentować dla czytelności
        // override this.ToString() = 
        //     match this with
        //     | Wezel(v,n) -> $"{v} -> {n}"
        //     | _ -> ""


    let nPierwszych (n) = 
        let rec recur = fun (i, n) -> if i < n then Wezel(i, recur (i+1, n)) else Pusta
        recur (0 ,n)

    Console.WriteLine($"(3)->{nPierwszych 3}")

module Zad3_3 = 
    open Zad3_1 // dodaje moduł z listą
    Console.WriteLine("\nZad3_3")
    let rec nElement (l : Lista<'a>, n :int) : ('a) = 
        match l with
        | Pusta ->  failwith "n poza zakresem"
        | Wezel(lval, lnext) when n > 1 -> nElement (lnext, n-1)
        | Wezel(lval, lnext) -> lval

    let list = nPierwszych 5
    Console.WriteLine($"{(list)} \t [3] -> {(nElement (list, 3))}")

module Zad3_4 = 
    open Zad3_1 // dodaje moduł z listą
    Console.WriteLine("\nZad3_4")
    let rec elementIsnieje (l : Lista<'a>, e :'a) : Boolean = 
        match l with
        | Pusta ->  false
        | Wezel(lval, lnext) when lval = e -> true
        | Wezel(lval, lnext) -> elementIsnieje(lnext, e)

    let list = nPierwszych 5
    Console.WriteLine($"{(list)} \t [2] -> {(elementIsnieje(list, 2))}, \t [8] -> {(elementIsnieje(list, 8))}")


/// ---------------- wyszukanie listy indesków
module Zad3_5a = 
    open Zad3_1 // dodaje moduł z listą

    let rand = (new System.Random())
    let nRand (n) = 
        let rec recur = fun (i, n) -> if i < n then Wezel(rand.Next(1, 10), recur (i+1, n)) else Pusta
        recur (0 ,n)

    let rec Zanjdz (l : Lista<'a>, e :('a) -> Boolean, i:int) : int*Lista<'a> = 
        match l with
        | Pusta ->  (-1,l)
        | Wezel(lval, lnext) when e(lval) -> (i,l)
        | Wezel(lval, lnext) -> Zanjdz(lnext, e, i+1)

    // warunek wyszukiwania - labda 
    let war = fun (a) -> a = 2
    let list = nRand 5
    Console.WriteLine($"{(list)} \t [2] -> {(Zanjdz(list, war,0))}, \t [8] -> {(Zanjdz(list, (fun (a)->a=8), 0))}")

    let rec ZanjdzListe = fun (l : Lista<'a>, e :('a) -> Boolean, i:int) -> 
        match Zanjdz(l, e, i) with
        | (i, Pusta) -> Pusta
        | (i, Wezel(lval, lnext)) -> Wezel(i, ZanjdzListe(lnext, e, i+1))        


    Console.WriteLine($"{(list)} \t [2] -> {(ZanjdzListe(list, war,0))}, \t [8] -> {(ZanjdzListe(list, (fun a->a=8), 0))}")
/// ----------------



module Zad3_6 = 
    open Zad3_1 // dodaje moduł z listą
    Console.WriteLine("\nZad3_6")
    let rec nUsun (l : Lista<'a>, n :int) : (Lista<'a>) = 
        match l with
        | Pusta ->  failwith "n poza zakresem"
        | Wezel(lval, lnext) when n > 1 -> Wezel(lval, nUsun (lnext, n-1)) // przepisujemy początkowe elementy listy
        | Wezel(lval, lnext) -> lnext // pomijamy element i przekazujemy resztę listy

    let list = nPierwszych 5
    Console.WriteLine($"{(list)} \t [3] -> {(nUsun (list, 3))}")

module Zad3_12 = 
    open Zad3_1 // dodaje moduł z listą
    Console.WriteLine("\nZad3_12")
    let rec nRewers (l : Lista<'a>, lr : Lista<'a>) : (Lista<'a>) = 
        match l with
        | Wezel(lval, lnext) ->  nRewers(lnext, Wezel(lval, lr))
        | Pusta ->  lr
        
    let list = nPierwszych 5
    Console.WriteLine($"{(list)} \t [3] -> {(nRewers (list, Pusta))}")

module Zad3_14 = 
    open Zad3_1 // dodaje moduł z listą
    open Zad3_12 // dodaje moduł z listą
    Console.WriteLine("\nZad3_14")
    let rec nCompare (la : Lista<int>, lb : Lista<int>) : (Lista<Boolean>) = 
        match la, lb with
        | Wezel(laval, lanext), Wezel(lbval, lbnext) ->  Wezel(laval > lbval, nCompare(lanext, lbnext))
        | Pusta, Pusta  ->  Pusta
        | _  ->  failwith "nierówne listy"
        
    let list = nPierwszych 5
    let list2 = nRewers(list, Pusta)
    Console.WriteLine($"({list}) > ({list2}) \t -> {(nCompare (list, list2))}")

module Zad3_18 = 
    open Zad3_1 // dodaje moduł z listą
    Console.WriteLine("\nZad3_18")
    
    type Stos<'b> = 
        {
            stos: Lista<'b>
        }
        member this.Wartsc() :'b =
            match this.stos with
            | Pusta -> failwith "Pusty stos"
            | Wezel(v,next) -> v
        
        member this.Dodaj(v:'b) : Stos<'b> = 
            {stos = Wezel(v, this.stos)}
        
        member this.Zdejmij() : Stos<'b> =
            match this.stos with
            | Pusta -> failwith "Pusty stos"
            | Wezel(v,next) -> {stos=next}
        
    
    let st = {stos=Pusta}.Dodaj(1).Dodaj(2).Dodaj(3)
    Console.WriteLine($"{st} -> wartos -> {st.Wartsc()}")
    let st2 = st.Zdejmij()
    Console.WriteLine($"{st2} -> wartos -> {st2.Wartsc()}")
 

module Zad3_24 = 
    Console.WriteLine("\nZad3_24")

    type Drzewo = 
        | Puste
        | Wezel of float*Drzewo*Drzewo
        override this.ToString() =
            match this with
            | Wezel(v,l,p) -> $"{v} l: ({l}) p: ({p})"
            | _ -> ""

    let rec Dodaj (v:float, d: Drzewo) :Drzewo =  
        match d with
        | Puste -> Wezel(v, Puste, Puste)
        | Wezel(wv, l, p) when wv <= v -> Wezel(wv, l ,Dodaj (v, p) )
        | Wezel(wv, l, p) -> Wezel(wv, Dodaj (v, l) , p )

    
    let rec Sciezka (v:float, d: Drzewo) : Zad3_1.Lista<float> =  
        match d with
        | Puste -> Zad3_1.Pusta
        | Wezel(wv, l, p) when wv <= v -> Zad3_1.Wezel(wv, Sciezka(v, p) )
        | Wezel(wv, l, p) -> Zad3_1.Wezel(wv, Sciezka(v, l) )

    let d = Dodaj(5, Dodaj(6,Dodaj(8,Dodaj(-10,Dodaj(2,Dodaj(3, Puste))))))    
    Console.WriteLine($"{d}")
    Console.WriteLine($" scizka {6} -> {Sciezka (6, d)}")
    

