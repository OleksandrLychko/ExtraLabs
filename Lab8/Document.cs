namespace Lab8
{
    public class Document
    {
        public string Name;
        public string Author;
        public string Status;
        public int OwnerId;

        public Document(string name, string author)
        {
            Name = name;
            Author = author;
            Status = "in stock";
            OwnerId = -1;
        }
        
        public void RewriteInfo(string name, string author)
        {
            Name = name;
            Author = author;
        }
    }
}