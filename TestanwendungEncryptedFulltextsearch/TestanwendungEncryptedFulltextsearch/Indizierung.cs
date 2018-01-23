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
        }

        private object LoadDocuments()
        {
            foreach (var path in Directory.GetFiles(documentPath, "*.txt"))
            {
                var document = File.OpenText(path).ReadToEnd();

                IndexDocument(document, Path.GetFileName(document));
            }
        }

        private Dictionary<string, List<Suchtreffer>> Index = new Dictionary<string, List<Suchtreffer>>();
        private void IndexDocument(string document, string filename)
        {
            var words = document.Split(' ', '.', ',', '!', '?', ':', ';');
            var counter = 0;

            foreach (var word in words)
            {
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
    }
}
