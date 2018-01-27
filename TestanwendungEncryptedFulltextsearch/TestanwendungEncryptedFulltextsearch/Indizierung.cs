using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestanwendungEncryptedFulltextsearch
{
    public class Indizierung
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
            var retVal = SplitQuery(query);

            return retVal;
        }

        private List<Suchtreffer> SplitQuery(string query)
        {
            var indexOfUnd = query.IndexOf('+');
            var indexOfOder = query.IndexOf('|');

            if (indexOfOder == -1 && indexOfUnd == -1)
            {
                if (Index.ContainsKey(query))
                {
                    return Index[query];
                }
            }
            else
            {
                if (indexOfOder == -1 || indexOfUnd < indexOfOder)
                {
                    return Und(query.Substring(0, indexOfUnd).Trim(), query.Substring(indexOfUnd) + 1);
                }
                if (indexOfUnd == -1 || indexOfOder <= indexOfUnd)
                {
                    return Oder(query.Substring(0, indexOfOder).Trim(), query.Substring(indexOfOder) + 1);
                }
            }
            return new List<Suchtreffer>();
        }

        private List<Suchtreffer> Und(string query1, string query2)
        {
            var retVal = new List<Suchtreffer>();

            if (Index.ContainsKey(query1))
            {
                retVal.AddRange(Index[query1]);
            }
            if (Index.ContainsKey(query2))
            {
                retVal.AddRange(Index[query2]);
            }

            return retVal;
        }

        public List<Suchtreffer> Oder(string query1, string query2)
        {
            var retVal = new List<Suchtreffer>();

            if (Index.ContainsKey(query1) && Index.ContainsKey(query2))
            {
                retVal.AddRange(Index[query1]);
                retVal.AddRange(Index[query2]);
            }
            return retVal;
        }
    }
}
