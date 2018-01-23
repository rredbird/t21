namespace TestanwendungEncryptedFulltextsearch
{
    public class Suchtreffer
    {
        public Suchtreffer(int wordnumber, string filename)
        {
            this.wordnumber = wordnumber;
            this.filename = filename;
        }

        public string filename = "";
        public int wordnumber = -1;
    }
}