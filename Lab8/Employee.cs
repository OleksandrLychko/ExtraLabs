using System;
using System.Linq;

namespace Lab8
{
    public class Employee : User
    {
        private string _option = "";
        
        public Employee(string firstname, string lastname, int id)
        {
            Firstname = firstname;
            Lastname = lastname;
            Id = id;
        }

        public override void DisplayMenu(Library library)
        {
            while (_option != "3")
            {
                DisplayOptions();
                _option = Console.ReadLine();
                switch (_option)
                {
                    case "1":
                        DisplayClientOptions();
                        _option = Console.ReadLine();
                        ExecuteUserOption(_option, library);
                        _option = "";
                        break;
                    case "2":
                        DisplayDocOptions();
                        _option = Console.ReadLine();
                        ExecuteDocOption(_option, library);
                        _option = "";
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
        
        
        private static void ExecuteUserOption(string option, Library library)
        {
            switch (option)
            {
                case "1":
                    Console.Write("Enter firstname, lastname, group and unique id separated with space: ");
                    string[] input = Console.ReadLine()?.Split().ToArray();
                    library.AddClient(input?[0], input?[1], input?[2], Convert.ToInt32(input?[3]));
                    break;
                case "2":
                    Console.Write("Enter the id of user to remove: ");
                    int removeId = Convert.ToInt32(Console.ReadLine());
                    library.RemoveClient(removeId);
                    break;
                case "3":
                    Console.Write("Enter the id of user to make change: ");
                    int changeId = Convert.ToInt32(Console.ReadLine());
                    library.ChangeClientInfo(changeId);
                    break;
                case "4":
                    Console.Write("Enter the id of user to view info: ");
                    int viewId = Convert.ToInt32(Console.ReadLine());
                    library.ViewClientInfo(viewId);
                    break;
                case "5":
                    library.ManageClientLending();
                    break;
                case "6":
                    library.DisplayAllClients();
                    break;
                case "7":
                    Console.Write("Enter keyword: ");
                    string keyword = Console.ReadLine();
                    library.ClientKeywordSearch(keyword);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }
        }
        
        private static void ExecuteDocOption(string option, Library library)
        {
            switch (option)
            {
                case "1":
                    Console.Write("Enter new name and author separated with space: ");
                    string[] input = Console.ReadLine()?.Split().ToArray();
                    library.AddDocument(input?[0], input?[1]);
                    break;
                case "2":
                    Console.Write("Enter the name of document to remove: ");
                    library.RemoveDocument(Console.ReadLine());
                    break;
                case "3":
                    Console.Write("Enter the name of document to make change: ");
                    library.ChangeDocInfo(Console.ReadLine());
                    break;
                case "4":
                    Console.Write("Enter the name of document to view info: ");
                    library.ViewDocInfo(Console.ReadLine());
                    break;
                case "5":
                    library.DisplayAllDocs();
                    break;
                case "6":
                    Console.Write("Enter keyword: ");
                    library.DocKeywordSearch(Console.ReadLine());
                    break;
                case "7":
                    library.ViewRequests();
                    break;
                case "0":
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
0 to return");
        }

        private static void DisplayClientOptions()
        {
            Console.WriteLine(@"Choose option:
1 to add user
2 to remove user
3 to rewrite user info
4 to view user info
5 to manage user lending
6 to view users list
7 to search by keyword
0 to return");
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
7 to view requests
0 to return");
        }
    }
}