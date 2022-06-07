using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab9.Zad3_5
{

    public class Program
    {

        public static void PMain()
        {
            Action<T> Powtorz<T>(int n, Action<T> ac) {
                return (s)=>{for (int i = 0; i < n; i++)
                {
                   ac(s); 
                }};
            }

            var akcja = Powtorz<string>(10, Console.WriteLine);
            akcja("Ala ma kota");
        }

    }
}