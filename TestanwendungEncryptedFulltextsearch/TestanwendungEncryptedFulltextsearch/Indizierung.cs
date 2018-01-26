using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestanwendungEncryptedFulltextsearch
{
    class Indizierung
    {
        private string documentPath = @"C:\Users\rui\Dropbox\t21\Testdokumente";

        public void IndexDocuments()
        {
            var documents = LoadDocuments();

            foreach (var document in documents)
            {
                IndexDocument(document.Item2, document.Item1);
            }
        }

        private List<Tuple<string, string>> LoadDocuments()
        {
            var retVal = new List<Tuple<string, string>>();
            foreach (var path in Directory.GetFiles(documentPath, "*.txt"))
            {
                var document = File.OpenText(path).ReadToEnd();
                retVal.Add(Tuple.Create<string, string>(Path.GetFileName(path), document));
            }
            return retVal;
        }

        private Dictionary<string, List<Suchtreffer>> Index = new Dictionary<string, List<Suchtreffer>>();
        private void IndexDocument(string document, string filename)
        {
            var words = document.Split(' ', '.', ',', '!', '?', ':', ';');
            var counter = 0;

            foreach (var w in words)
            {
                var word = w.ToLower();
                if (Index.ContainsKey(word))
                {
                    Index[word].Add(new Suchtreffer(counter, filename));
                }
                else
                {
                    Index.Add(word, new List<Suchtreffer>() { new Suchtreffer(counter, filename) });
                }
                counter++;
            }
        }

        public List<Suchtreffer> Search(string query)
        {
            List<List<Suchtreffer>> indicees = new List<List<Suchtreffer>>();
            var words = query.ToLower().Split(' ');
            foreach (var word in words)
            {
                if (Index.ContainsKey(word))
                {
                    indicees.Add(Index[word]);
                }
            }
            var retVal = new List<Suchtreffer>();
            if (indicees.Count == words.Length)
            {
                foreach (var index in indicees)
                {
                    retVal.AddRange(index);
                }
            }
            return retVal;
        }
    }
}
