using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab9.Zad3_9
{

    public class Program
    {

        public class MyList<T>{
            private class Node{
                public T? Value {get;set;}
                public Node? Next  {get;set;}

            }

            // root element bez wartosci
            private Node root = new Node();

            public MyList(IEnumerable<T> objects){
                var last = root;
                foreach(var el in objects)
                    last = last.Next = new Node() {Value=el};   
            }

            public S Map<S>(Func<T, bool>? filter = null) where S : IList<T>, new(){
                var el = root;
                var list = new S();
                while(el.Next != null){
                    el = el.Next;
                    if(filter == null || filter(el.Value))
                        list.Add(el.Value);
                }
                return list;
            }


            public IEnumerable<T> MapLazzy(Func<T, bool>? filter = null){
                var el = root;

                while(el.Next != null){
                    el = el.Next;
                    if(filter == null || filter(el.Value))
                        yield return el.Value;
                }
                yield break;
            }

            public IEnumerable<int> GenLazzy(){
                var r = new Random();
                while(true){
                    var s = r.Next(-100,100);
                    Console.WriteLine($"Lazzy gen: {s}");               
                    if(s < -10) 
                        yield break;
                    yield return s;
                }
           }

        }

        public static void PMain()
        {
            var l = new MyList<string>(new []{"ala", "tomek", "romek"});
            var list = l.Map<List<String>>();
            list.ForEach(Console.WriteLine);
            l.Map<List<String>>(s=>s.StartsWith("al")).ForEach(Console.WriteLine);

            Console.WriteLine("Lazzy");
            var l2 = l.MapLazzy();
            l2.ToList().ForEach(Console.WriteLine);
            l.MapLazzy(s=>s.Length>4).ToList().ForEach(Console.WriteLine);



            
            foreach(var s in l.GenLazzy()){
                Console.WriteLine($"Lazzy for: {s}");               
            }


        }

    }
}