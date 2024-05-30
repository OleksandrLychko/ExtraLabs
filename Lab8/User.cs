namespace Lab8
{
    public abstract class User
    {
        public string Firstname;
        public string Lastname;
        public int Id;
        
        public abstract void DisplayMenu(Library library);
    }
}