open System
printfn "------------Lab 2---------------\n\n"


module Zad2_4 = 
    Console.WriteLine("\nZad2_4")

    let email_slice (email)  =
        let len = String.length email
        let rec find i c = if len <= i || email[i] = c then i else find (i+1) c            
        let i = find 0 '@'
        (email.[0..i-1], email.[i+1..len])
    
    let fu = fun a -> $"{a} => {email_slice a}"
    Console.WriteLine(fu "user@example.com")


module Zad2_5 =
    Console.WriteLine("\nZad2_5")
    
    let pcz_email email =
        match Zad2_4.email_slice email with
        | (_, "pcz.pl") -> "Adres pcz"
        | _ -> "Nie adres pcz"

    let fu = fun a -> $"{a} => {pcz_email a}"
    Console.WriteLine(fu "user@example.com")
    Console.WriteLine(fu "user@pcz.pl")
    Console.WriteLine(fu "user2@example.com")


module Zad2_9 =
    Console.WriteLine("\nZad2_9")

    // rekord z operatorami (tabulacja jest istotna)
    type Ulamek = 
        { 
            L:int
            M:int 
        } 
        static member (+) (a:Ulamek, b:Ulamek) = 
            if b.M = a.M then {L=(a.L+b.L);M=a.M} else {L=(a.L*b.M+b.L*a.M);M=a.M*b.M}
        static member (-) (a:Ulamek, b:Ulamek) = 
            if b.M = a.M then {L=(a.L-b.L);M=a.M} else {L=(a.L*b.M+b.L*a.M);M=a.M*b.M}
        static member (~-) (a:Ulamek) = 
            {L=(-a.L);M=a.M}
        static member (*) (a:Ulamek, b:Ulamek) = 
            {L=a.L*b.L;M=a.M*b.M}
        static member (/) (a:Ulamek, b:Ulamek) = 
            {L=a.L*b.M;M=a.M*b.L}
        override this.ToString() = $"[{this.L}/{this.M}]"
    
    let u1 = {L=1;M=2}
    let u2 = {L=1;M=3}
    let u3 = {L=3;M=1}
    let u4 = {L=2;M=20}
    Console.WriteLine($"{u1}/{u2} = {u1/u2}")
    Console.WriteLine($"{u3}/{u2} = {u3/u2}")
    Console.WriteLine($"{u3}-({u2}/{u1})+{u4} = {u3-(u2/u1)+u4}")


module Zad2_10 =
    Console.WriteLine("\nZad2_10")
    type Kalendarz = 
        {
            D:int
            M:int
            R:int
        }
        member this.Przestepny = if (this.R % 4) = 0 then 1 else 0
        // member this.Dni_wM = 
        //     match this with
        //     | {M = 2} -> 28 + this.Przestepny 
        //     | {M = m} when (m % 2) = 1 -> 31
        //     | _ -> 30
        // member this.Dni_wR = 365 + this.Przestepny
        member this.Dzien_Roku = (this.M - 1) * 31 - int ((this.M - 1) / 2) + this.D - (if this.M > 2 then 2 - this.Przestepny else 0)
        member this.Dzien_odR0 = (365 * this.R + int (this.R / 4)) + this.Dzien_Roku + this.D
        member this.Dzien_Tyg = ((this.Dzien_odR0 - {D=1;M=1;R=1990}.Dzien_odR0) % 7)
        override this.ToString() = $"{this.D:D2}-{this.M:D2}-{this.R:D4}r"

    let d1 = {D=1;M=12;R=2022}
    let d0 = {D=1;M=1;R=1990}
    let dt = ["pn";"wt";"śr";"cz";"pt";"so";"nd"]
    Console.WriteLine($"{d1} => {dt.[d1.Dzien_Tyg]} \ttest: {DateTime(2022, 12, 1).DayOfWeek}")
    Console.WriteLine($"{d0} => {dt.[d0.Dzien_Tyg]} \ttest: {DateTime(1990, 1, 1).DayOfWeek}")    


module Zad2_11 =
    Console.WriteLine("\nZad2_11")
    type OpResult =
        | Float of float
        //| Single
        | OpError of string
        static member (/) (a:OpResult, b:OpResult) :OpResult = 
            match (a,b) with
            | (Float a,Float b) -> if b = 0 then OpError $"->({a}/{b})<- Error: Dzielimy przez zero!" else Float (a / b)
            | _ -> OpError $"{a}/{b}" 
        override this.ToString() = 
            match this with
            | Float a -> $"{a}"
            | OpError a -> $"{a}"
    
    let a = Float 0
    let b = Float 1
    let c = Float 5
    Console.WriteLine($"{b}/{a} = {b/a}")
    Console.WriteLine($"{b}/{c} = {b/c}")
    Console.WriteLine($"{b}/{c}/{c} = {b/c/c}")
    Console.WriteLine($"{b}/{a}/{c} = {b/a/c}")
    Console.WriteLine($"({b}/{a})/({c}/{a}) = {(b/a)/(c/a)}")
        


module Zad2_15 =
    Console.WriteLine("\nZad2_15")
    // piekne to nie jest bo rekurencyjnie, ale pętli jeszcze nie było
    type Osoba = 
        {
            Imie:string
            Nawisko:string
            Wiek:uint
        }
        override this.ToString() = $"({this.Imie} {this.Nawisko} {this.Wiek})"
    type Dane =
        | DaneOsoby of Osoba
        | Empty

    let Program () =
        let GetValues = fun (text:string) -> 
            Console.WriteLine(text)
            Console.ReadLine().Split(' ', StringSplitOptions.TrimEntries)
        let GetCmd = fun () ->  
            let args = GetValues "Opcje:\n1 - utworzenie rekordu\n2 - edycja rekordu\n3 - pokaz rekord\n4 - zakończ"
            try (args[0] |> int) with | _ -> 0
        let GetOsosba = fun () ->
            let args = GetValues ("Podaj dane rekordu:\n imie nazwisko wiek")
            try DaneOsoby {Imie=args[0];Nawisko=args[1];Wiek=args[2]|>uint}with | _ -> Empty
        let rec CmdLoop = fun (data:Dane) ->
            match GetCmd() with
            | 1 -> CmdLoop(GetOsosba())
            | 2 -> 
                match data with 
                | DaneOsoby s -> CmdLoop(GetOsosba())
                | Empty -> 
                    Console.WriteLine("Brak danych, edycja przerwana")
                    CmdLoop(data)
            | 3 -> 
                Console.WriteLine(data)
                CmdLoop(data)
            | 4 -> Console.WriteLine("Exit")
            | _ -> 
                Console.WriteLine("Błędny parametr")
                CmdLoop(data)
        CmdLoop(Empty)
    Program ()