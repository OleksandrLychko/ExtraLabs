using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8
{
    public class Library
    {
        public readonly List<Client> _clients = new List<Client>();
        private readonly List<Employee> _employees = new List<Employee>();
        private readonly List<Document> _documents = new List<Document>();
        public readonly List<Request> _requests = new List<Request>();

        public void AddEmployee(string firstName, string lastName, int id)
        {
            Employee newEmployee = new Employee(firstName, lastName, id);
            _employees.Add(newEmployee);
        }
        
        public void AddClient(string firstName, string lastName, string group, int id)
        {
            foreach (Client client in _clients)
            {
                if (client.Id == id)
                {
                    Console.WriteLine($"User with id {id} already exists");
                    return;
                }
            }

            Client newClient = new Client(firstName, lastName, group, id);
            _clients.Add(newClient);
        }

        public void RemoveClient(int id)
        {
            if (FindClientById(id) == null)
            {
                Console.WriteLine($"No user with id {id} was found");
                return;
            }

            if (FindClientById(id).DocumentsLended.Count != 0)
            {
                Console.WriteLine("User cannot be removed, they still have lended documents");
                return;
            }

            _clients.Remove(FindClientById(id));
        }

        public void ChangeClientInfo(int id)
        {
            Client currentClient = FindClientById(id);
            if (currentClient == null)
            {
                Console.WriteLine($"No user with id {id} was found");
                return;
            }

            Console.WriteLine("Enter new firstname, lastname, group and id separated with space: ");
            string[] input = Console.ReadLine()?.Split().ToArray();
            currentClient.RewriteInfo(input?[0], input?[1], input?[2], Convert.ToInt32(input?[3]));
        }

        public void ViewClientInfo(int id)
        {
            Client currentClient = FindClientById(id);

            if (currentClient == null)
            {
                Console.WriteLine($"No user with id {id} was found");
                return;
            }

            Console.WriteLine($@"<<<{id}>>>
Firstname: {currentClient.Firstname}
Lastname: {currentClient.Lastname}
Group: {currentClient.Group}");
            if (currentClient.DocumentsLended.Count == 0)
            {
                Console.WriteLine("No documents landed out");
                return;
            }

            Console.WriteLine("Landed out documents:");
            foreach (Document document in currentClient.DocumentsLended)
            {
                Console.WriteLine(document.Name);
            }
        }

        public void DisplayAllClients()
        {
            if (_clients.Count == 0)
            {
                Console.WriteLine("No users registered");
                return;
            }

            Console.WriteLine(@"Choose option:
1 to sort by firstname
2 to sort by lastname
3 to sort by group");
            string sortOption = Console.ReadLine();
            SortUsers(sortOption);

            Console.WriteLine("Users registered:");
            foreach (Client user in _clients)
            {
                Console.WriteLine($"User id: {user.Id}; {user.Firstname} {user.Lastname}, group {user.Group}");
            }
        }

        public Client FindClientById(int id)
        {
            foreach (Client client in _clients)
            {
                if (client.Id == id)
                {
                    return client;
                }
            }

            return null;
        }

        public void ManageClientLending()
        {
            Console.Write("Enter user id: ");
            int inputId = Convert.ToInt32(Console.ReadLine());

            Client currentClient = FindClientById(inputId);

            if (currentClient == null)
            {
                Console.WriteLine($"No user with id {inputId} was found");
                return;
            }

            Console.WriteLine(@"Choose option:
1 to lend out  
2 to receive the document
0 to return");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.Write("Enter document name: ");
                    string inputName = Console.ReadLine();
                    Document currentDoc = FindDocumentByName(inputName);

                    if (currentDoc == null)
                    {
                        Console.WriteLine("No document found");
                        return;
                    }

                    if (currentClient.DocumentsLended.Count >= 5)
                    {
                        Console.WriteLine("Lended limit for user is reached");
                        return;
                    }

                    currentClient.LendOutDocument(currentDoc);
                    currentDoc.OwnerId = currentClient.Id;
                    break;
                case "2":
                    Console.Write("Enter document name: ");
                    inputName = Console.ReadLine();
                    currentDoc = FindDocumentByName(inputName);

                    if (currentDoc == null)
                    {
                        Console.WriteLine("No document found");
                        return;
                    }

                    currentClient.ReturnDocument(currentDoc);
                    currentDoc.OwnerId = -1;
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }
        }

        public void ClientKeywordSearch(string keyword)
        {
            foreach (Client user in _clients)
            {
                if (user.Firstname.Contains(keyword) || user.Lastname.Contains(keyword))
                {
                    Console.WriteLine($"User id: {user.Id}; {user.Firstname} {user.Lastname}, group {user.Group}");
                }
            }
        }

        public void DocKeywordSearch(string keyword)
        {
            foreach (Document document in _documents)
            {
                if (document.Name.Contains(keyword) || document.Author.Contains(keyword))
                {
                    Console.Write($"Name: {document.Name}, author: {document.Author}, status: {document.Status}");
                    if (document.Status == "lended out")
                    {
                        Console.WriteLine($", owner id: {document.OwnerId}");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        public void AddDocument(string name, string author)
        {
            Document document = new Document(name, author);
            _documents.Add(document);
        }

        public void RemoveDocument(string docName)
        {
            Document currentDoc = FindDocumentByName(docName);
            if (currentDoc == null)
            {
                Console.WriteLine("No document found");
                return;
            }

            if (currentDoc.Status == "lended out")
            {
                Console.WriteLine("Document cannot be removed, it is lended out now");
            }

            _documents.Remove(currentDoc);
        }

        public void ChangeDocInfo(string name)
        {
            if (FindDocumentByName(name) == null)
            {
                Console.WriteLine($"No document with name {name} was found");
                return;
            }

            Console.Write("Enter new name and author separated with space: ");
            string[] input = Console.ReadLine()?.Split().ToArray();
            FindDocumentByName(name).RewriteInfo(input?[0], input?[1]);
        }

        public void ViewDocInfo(string name)
        {
            Document currentDoc = FindDocumentByName(name);
            if (currentDoc == null)
            {
                Console.WriteLine($"No document with name {name} was found");
                return;
            }

            Console.Write($"Name: {currentDoc.Name}, author: {currentDoc.Author}, status: {currentDoc.Status}");
            if (currentDoc.Status == "lended out")
            {
                Console.WriteLine($", owner id: {currentDoc.OwnerId}");
            }
            else
            {
                Console.WriteLine();
            }
        }

        public Document FindDocumentByName(string name)
        {
            foreach (Document document in _documents)
            {
                if (document.Name == name)
                {
                    return document;
                }
            }

            return null;
        }

        private void SortUsers(string option)
        {
            switch (option)
            {
                case "1":
                    _clients.Sort((user1, user2) =>
                        String.Compare(user1.Firstname, user2.Firstname, StringComparison.Ordinal));
                    break;
                case "2":
                    _clients.Sort((user1, user2) =>
                        String.Compare(user1.Lastname, user2.Lastname, StringComparison.Ordinal));
                    break;
                case "3":
                    _clients.Sort((user1, user2) => 
                        String.Compare(user1.Group, user2.Group, StringComparison.Ordinal));
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }
        }

        public void DisplayAllDocs()
        {
            if (_documents.Count == 0)
            {
                Console.WriteLine("No documents registered");
                return;
            }

            Console.WriteLine(@"Choose option:
1 to sort by name
2 to sort by author");
            string sortOption = Console.ReadLine();
            SortDocs(sortOption);

            Console.WriteLine("Documents registered:");
            foreach (Document document in _documents)
            {
                Console.Write($"Name: {document.Name}, author: {document.Author}, status: {document.Status}");
                if (document.Status == "lended out")
                {
                    Console.WriteLine($", owner id: {document.OwnerId}");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        private void SortDocs(string option)
        {
            switch (option)
            {
                case "1":
                    _documents.Sort((doc1, doc2) => String.Compare(doc1.Name, doc2.Name, StringComparison.Ordinal));
                    break;
                case "2":
                    _documents.Sort((doc1, doc2) => String.Compare(doc1.Author, doc2.Author, StringComparison.Ordinal));
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }
        }

        public Employee FindEmployeeById(int id)
        {
            foreach (Employee employee in _employees)
            {
                if (employee.Id == id)
                {
                    return employee;
                }
            }

            return null;
        }

        public void ViewRequests()
        {
            int count = 0;
            Console.WriteLine("Five latest requests: ");
            foreach (Request request in _requests)
            {
                if (count == 5)
                {
                    return;
                }
                count++;
                Console.WriteLine($"Asked about: {request.DocName}. Response: {request.Response}");
            }
        }
    }
}