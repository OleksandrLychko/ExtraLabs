using System;
using System.Linq;

namespace Lab8
{
    internal class Program
    {
        private static readonly Library Library = new Library();
        private static string _option = "";
        
        public static void Main(string[] args)
        {
            Library.AddEmployee("Boss", "Adminovich", 1);           
            
            Library.AddClient("John", "Smith", "IP-32", 1);
            Library.AddClient("Michael", "Roberts", "IP-33", 2);
            Library.AddClient("Ashley", "Carter", "IP-34", 3);
            Library.AddClient("Christopher", "Jackson", "IP-32", 4);
            Library.AddClient("Jennifer", "Thompson", "IP-34", 5);
            
            Library.AddDocument("Doc1", "Author1");
            Library.AddDocument("Doc2", "Author2");
            Library.AddDocument("Doc4", "Author1");
            Library.AddDocument("Doc6", "Author1");
            Library.AddDocument("Doc3", "Author4");
            Library.AddDocument("Doc5", "Author3");
            Library.AddDocument("Doc8", "Author1");
            Library.AddDocument("Doc9", "Author4");
            Library.AddDocument("Doc7", "Author3");
            
            MainMenu();
        }

        private static void MainMenu()
        {
            while (_option != "0")
            {
                Console.WriteLine(@"Choose option:
1 to log in as client
2 to sign up as client
3 to log in as employee
0 to finish");
                _option = Console.ReadLine();
                switch (_option)
                {
                    case "1":
                        Console.Write("Enter your id: ");
                        int clientId = Convert.ToInt32(Console.ReadLine());
                        Client currentClient = Library.FindClientById(clientId);
                        if (currentClient == null)
                        {
                            Console.WriteLine("No user found");
                            break;
                        }
                        currentClient.DisplayMenu(Library);
                        break;
                    case "2":
                        Console.WriteLine("Enter your firstname, lastname and group separated with space:");
                        string[] input = Console.ReadLine()?.Split().ToArray();
                        for (int i = 1; i <= Library.Clients.Count + 1; i++)
                        {
                            if (Library.FindClientById(i) == null)
                            {
                                Library.AddClient(input?[0], input?[1], input?[2], i);
                                currentClient = Library.FindClientById(i);
                                Console.WriteLine($"Your personal id is {i}");
                                currentClient.DisplayMenu(Library);
                                break;
                            }
                        }
                        break;
                    case "3":
                        Console.Write("Enter your id: ");
                        int employeeId = Convert.ToInt32(Console.ReadLine());
                        Employee currentEmployee = Library.FindEmployeeById(employeeId);
                        if (currentEmployee == null)
                        {
                            Console.WriteLine("No user found");
                            break;
                        }
                        currentEmployee.DisplayMenu(Library);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}