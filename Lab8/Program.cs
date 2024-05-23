using System;
using System.Linq;

namespace Lab8
{
    internal class Program
    {
        static readonly Library Library = new Library();
        
        public static void Main(string[] args)
        {
           
            Library.AddUser("John", "Smith", 32, 1);
            Library.AddUser("Michael", "Roberts", 33, 2);
            Library.AddUser("Ashley", "Carter", 34, 3);
            Library.AddUser("Christopher", "Jackson", 32, 4);
            Library.AddUser("Jennifer", "Thompson", 34, 5);
            
            Library.AddDocument("Doc1", "Author1");
            Library.AddDocument("Doc2", "Author2");
            Library.AddDocument("Doc4", "Author1");
            Library.AddDocument("Doc6", "Author1");
            Library.AddDocument("Doc3", "Author4");
            Library.AddDocument("Doc5", "Author3");
            Library.AddDocument("Doc8", "Author1");
            Library.AddDocument("Doc9", "Author4");
            Library.AddDocument("Doc7", "Author3");

            string option = "";

            while (option != "3")
            {
                DisplayOptions();
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        DisplayUserOptions();
                        option = Console.ReadLine();
                        ExecuteUserOption(option);
                        option = "";
                        break;
                    case "2":
                        DisplayDocOptions();
                        option = Console.ReadLine();
                        ExecuteDocOption(option);
                        option = "";
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private static void ExecuteUserOption(string option)
        {
            switch (option)
            {
                case "1":
                    Console.Write("Enter firstname, lastname, group and unique id separated with space: ");
                    string[] input = Console.ReadLine()?.Split().ToArray();
                    Library.AddUser(input?[0], input?[1], Convert.ToInt32(input?[2]), Convert.ToInt32(input?[3]));
                    break;
                case "2":
                    Console.Write("Enter the id of user to remove: ");
                    int removeId = Convert.ToInt32(Console.ReadLine());
                    Library.RemoveUser(removeId);
                    break;
                case "3":
                    Console.Write("Enter the id of user to make change: ");
                    int changeId = Convert.ToInt32(Console.ReadLine());
                    Library.ChangeUserInfo(changeId);
                    break;
                case "4":
                    Console.Write("Enter the id of user to view info: ");
                    int viewId = Convert.ToInt32(Console.ReadLine());
                    Library.ViewUserInfo(viewId);
                    break;
                case "5":
                    Library.ManageUserLending();
                    break;
                case "6":
                    Library.DisplayAllUsers();
                    break;
                case "7":
                    Console.Write("Enter keyword: ");
                    string keyword = Console.ReadLine();
                    Library.UserKeywordSearch(keyword);
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }
        }
        
        private static void ExecuteDocOption(string option)
        {
            switch (option)
            {
                case "1":
                    Console.Write("Enter new name and author separated with space: ");
                    string[] input = Console.ReadLine()?.Split().ToArray();
                    Library.AddDocument(input?[0], input?[1]);
                    break;
                case "2":
                    Console.Write("Enter the name of document to remove: ");
                    Library.RemoveDocument(Console.ReadLine());
                    break;
                case "3":
                    Console.Write("Enter the name of document to make change: ");
                    Library.ChangeDocInfo(Console.ReadLine());
                    break;
                case "4":
                    Console.Write("Enter the name of document to view info: ");
                    Library.ViewDocInfo(Console.ReadLine());
                    break;
                case "5":
                    Library.DisplayAllDocs();
                    break;
                case "6":
                    Console.Write("Enter keyword: ");
                    Library.DocKeywordSearch(Console.ReadLine());
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }
        }

        private static void DisplayOptions()
        {
            Console.WriteLine(@"Choose option:
1 to view users options
2 to view documents options
3 to finish");
        }

        private static void DisplayUserOptions()
        {
            Console.WriteLine(@"Choose option:
1 to add user
2 to remove user
3 to rewrite user info
4 to view user info
5 to manage user lending
6 to view users list
7 to search by keyword
8 to return");
        }

        private static void DisplayDocOptions()
        {
            Console.WriteLine(@"Choose option
1 to add document
2 to remove document
3 to change document info
4 to view document info
5 to view documents list
6 to search by keyword
7 to return");
        }
    }
}