using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab8.Zad2_2
{
    public readonly struct Autor
    {
        public readonly string Imie;
        public readonly string Nazwisko;

        public Autor(string Imie, string Nazwisko)
        {
            this.Imie = Imie;
            this.Nazwisko = Nazwisko;
        }
    }

    public class Ksiazka
    {
        readonly string _ID;
        public string ID { get => _ID; }
        readonly List<Autor> _Autorzy;
        public IReadOnlyList<Autor> Autorzy { get => _Autorzy; }
        readonly string _Tytul;
        public string Tytul { get => _Tytul; }
        readonly int _Rok;
        public int Rok { get => _Rok; }
        readonly string _Cena;
        public string Cena { get => _Cena; }
        public Ksiazka(string ID, List<Autor> Autorzy, string Tytul, int Rok, string Cena)
        {
            if (!Regex.IsMatch(ID, @"^[A-Z]-[0-9]{2}-[0-9]{3}$")
            || Autorzy.Count == 0
            || Tytul.Length < 50
            || Rok < 2015 || Rok > DateTime.Now.Year)
                throw new Exception("Złe dane");
            this._ID = ID;
            this._Autorzy = Autorzy;
            this._Tytul = Tytul;
            this._Rok = Rok;
            this._Cena = Cena;
        }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {
                var k = new Ksiazka("B-10-001",
                    new List<Autor> { new Autor("Imie", "Nazwisko") },
                    "123456789 123456789 123456789 123456789 1234567890",
                    2017, "12,21 zł");

                // zmiana autora
                k = new Ksiazka(k.ID,
                    new List<Autor> { new Autor("Imie", "Nazwisko") },
                    k.Cena, k.Rok, k.Cena);


                var k_err = new Ksiazka("B-10-001",
                    new List<Autor> { new Autor("Imie", "Nazwisko") },
                    "123456789 123456789 123456789 123456789 1234567890",
                    207, "12,21 zł");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}