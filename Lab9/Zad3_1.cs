using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab9.Zad3_1
{
    public class Program
    {

        public static void PMain()
        {
            Func<double, double, double> fun1 = (a, b) => a / b;
            var x1 = 10;
            var x2 = 2;
            Func<double, double, double> Odwroc(Func<double, double, double> fun)
            {
                return (a, b) => fun(b, a);
            }


            var wynik1 = fun1(x1, x2);
            var fun2 = Odwroc(fun1);
            var wynik2 = fun2(x2, x1);
            if (wynik1 == wynik2)
                Console.WriteLine("OK");
            else
                Console.WriteLine("Err");

            // variant 2
            Func<Func<double, double, double>, Func<double, double, double>> Odwroc_v2 = fun => (a, b) => fun(b, a);
            var fun2_v2 = Odwroc_v2(fun1);
            var wynik2_v2 = fun2_v2(x2, x1);
            if (wynik1 == wynik2_v2)
                Console.WriteLine("OK");
            else
                Console.WriteLine("Err");
        }

    }
}