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
                Console.WriteLine(root.Value);

                var list = new S();
                while(el.Next != null){
                    el = el.Next;
                    if(filter == null || filter(el.Value))
                        list.Add(el.Value);
                }
                return list;
            }

        }

        public static void PMain()
        {
            var l = new MyList<string>(new []{"ala", "tomek", "romek"});
            var list = l.Map<List<String>>();
            list.ForEach(Console.WriteLine);

            l.Map<List<String>>(s=>s.StartsWith("al")).ForEach(Console.WriteLine);

        }

    }
}