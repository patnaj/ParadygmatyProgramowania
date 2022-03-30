open System
printfn "------------Lab 1---------------\n\n"

module Zad1_8 = 
    Console.WriteLine("\nZad1_8")
    
    let DwumianNewtona n k =
        let rec loop n k =
            if k = 0 || k = n then
                1
            elif 0 < k && k < n then
                (loop (n-1) k) + (loop (n-1) (k-1))
            else
                0
        loop n k
    
    let str = fun n k -> $"(n:{n}|k:{k}) = {DwumianNewtona n k}"
    Console.WriteLine(str 3 1)
    Console.WriteLine(str 4 2)


module Zad1_13 = 
    Console.WriteLine("\nZad1_13")
    let rec silnia = fun n -> if n < 2 then 1 else n * silnia (n - 1)
    let fun1 = fun (i:int) -> 1./(float i)**2.
    let fun2 = fun (i:int) -> ((-1.)**i)/(float (silnia i)) 
    let fun3 = fun (i:int) -> 1. / (float (i*(i+1))) 
    let fun4 = fun (i:int) -> ((-2.)**i)/(float (silnia i)) 

    let e = 10.**(-5)
    let Szereg = fun e fu ->
        let rec loop = fun (i:int) -> 
            let v = fu i 
            if abs v < e then 0. else v + loop (i+1)
        loop 1
    
    let print = fun f n -> $"{n} => {Szereg e f}" 
    Console.WriteLine(print fun1 (nameof fun1))
    Console.WriteLine(print fun2 (nameof fun2))
    Console.WriteLine(print fun3 (nameof fun3))
    Console.WriteLine(print fun4 (nameof fun4))
    

module Zad1_15 = 
    Console.WriteLine("\nZad1_15")

    [<Measure>] type F  
    [<Measure>] type C

    type F with
        static member toC = fun (t:float<F>) -> (t - 32.<F>) * 5.<C>/9.<F>
    type C with
        static member toF = fun (t:float<C>) -> t * 9.<F>/5.<C> + 32.<F>

    let f = 63.5<F>
    let c = 33.5<C>
    
    Console.WriteLine($"{f}F -> {F.toC(f)}C")
    Console.WriteLine($"{c}C -> {C.toF(c)}F")

module Zad1_27 = 
    Console.WriteLine("\nZad1_27")

    let Czas = fun () ->
        Console.WriteLine($"Podaj liczbe minut:")
        let i = try int (Console.ReadLine() |> uint) with | _ -> -1
        if i < 0 || i > 24*60 then
            Console.WriteLine($"Poza zakresem (max {24*60})")
        else 
            Console.WriteLine($"Czas {int (i/60)}:{i%60}")
    Czas()
    
module Zad1_30 = 
    Console.WriteLine("\nZad1_30")
    
    let Prog = fun ()->
        let rec loop = fun sum num ->
            Console.WriteLine($"Podaj liczbe:")
            let i = try int (Console.ReadLine() |> uint) with | _ -> -1
            if i > 0 then 
                Console.WriteLine($"Srednia: {(sum+i)}/{(num+1)} => {(sum+i)/(num+1)}")            
                loop (sum+i) (num+1) 
            else 
                ()
        loop 0 0
    Prog()
