using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab8.Zad2_7
{

    public class Program
    {
        Stack<double> stack = new Stack<double>();

        bool ParseLine(string line){
            if(double.TryParse(line, out double value)){
                stack.Push(value);
                return true; // continue
            }

            switch(line){
                case "+":
                    if(stack.Count >= 2)
                        stack.Push(stack.Pop() + stack.Pop());
                    else
                        Console.WriteLine($"Mało parametrów: {stack.Count}");
                    break;
                case "-":
                    if(stack.Count >= 2)
                        stack.Push(stack.Pop() - stack.Pop());
                    else
                        Console.WriteLine($"Mało parametrów: {stack.Count}");
                    break;
                case "*":
                    if(stack.Count >= 2)
                        stack.Push(stack.Pop() * stack.Pop());
                    else
                        Console.WriteLine($"Mało parametrów: {stack.Count}");
                    break;
                case "/":
                    if(stack.Count >= 2)
                        stack.Push(stack.Pop() / stack.Pop());
                    else
                        Console.WriteLine($"Mało parametrów: {stack.Count}");
                    break;
                case "sin":
                    if(stack.Count >= 1)
                        stack.Push(Math.Sin(stack.Pop()));
                    else
                        Console.WriteLine($"Mało parametrów: {stack.Count}");
                    break;
                case "cos":
                    if(stack.Count >= 1)
                        stack.Push(Math.Cos(stack.Pop()));
                    else
                        Console.WriteLine($"Mało parametrów: {stack.Count}");
                    break;
                case "sqrt":
                    if(stack.Count >= 1)
                        stack.Push(Math.Sqrt(stack.Pop()));
                    else
                        Console.WriteLine($"Mało parametrów: {stack.Count}");
                    break;
                case "=":
                    if(stack.Count >= 1){
                        Console.WriteLine($"Wynik: {stack.First()}");
                        return false;
                    }
                    else
                        Console.WriteLine($"Mało parametrów: {stack.Count}");
                    break;
                default:
                    Console.WriteLine($"Nie znane polecenie: '{line}'");
                    break;
            }
            return true;
        }

        public static void PMain()
        {
            try
            {                
                var r = new Program();
                while(r.ParseLine(Console.ReadLine()??""));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}