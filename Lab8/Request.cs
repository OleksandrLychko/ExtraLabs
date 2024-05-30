namespace Lab8
{
    public class Request
    {
        public readonly string DocName;
        public string Response;
        
        public Request(string docName)
        {
            DocName = docName;
        }
    }
}