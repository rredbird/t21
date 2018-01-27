using System;

namespace TestanwendungEncryptedFulltextsearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var indizierung = new Indizierung();
            indizierung.IndexDocuments();

            Console.WriteLine("Bond Wikipedia");
            foreach (var searchresult in indizierung.Search("Bond Wikipedia"))
            {
                Console.WriteLine(searchresult.filename + ":" + searchresult.wordnumber);
            }
            Console.WriteLine("Wikipedia ship");
            foreach (var searchresult in indizierung.Search("Wikipedia ship"))

            {
                Console.WriteLine(searchresult.filename + ":" + searchresult.wordnumber);
            }
            Console.WriteLine("DIN Norm");
            foreach (var searchresult in indizierung.Search("DIN Norm"))
            {
                Console.WriteLine(searchresult.filename + ":" + searchresult.wordnumber);
            }
            Console.WriteLine("ship");
            foreach (var searchresult in indizierung.Search("ship"))
            {
                Console.WriteLine(searchresult.filename + ":" + searchresult.wordnumber);
            }

            Console.ReadKey();
        }
    }
}
