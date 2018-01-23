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

                IndexDocument(document);
            }
        }

        private Dictionary<string, List<Suchtreffer>> Index = new Dictionary<string, List<Suchtreffer>>();
        private void IndexDocument(string document)
        {
            var words = document.Split(' ', '.', ',', '!', '?', ':', ';');
            var counter = 0;

            foreach (var word in words)
            {
                if (Index.ContainsKey(word))
                {
                    Index[word].Add(new Suchtreffer() { wordnumber = counter });
                }
                else
                {
                    Index.Add(word, new List<Suchtreffer>() { new Suchtreffer() { wordnumber(counter) } });
                }
                counter++;
            }
        }
    }
}
