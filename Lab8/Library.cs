using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8
{
    public class Library
    {
        private readonly List<User> _users = new List<User>();
        private readonly List<Document> _documents = new List<Document>();

        public void AddUser(string firstName, string lastName, int group, int id)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == id)
                {
                    Console.WriteLine($"User with id {id} already exists");
                    return;
                }
            }

            User user = new User(firstName, lastName, group, id);
            _users.Add(user);
        }

        public void RemoveUser(int id)
        {
            if (FindUserById(id) == null)
            {
                Console.WriteLine($"No user with id {id} was found");
                return;
            }

            _users.Remove(FindUserById(id));
        }

        public void ChangeUserInfo(int id)
        {
            User currentUser = FindUserById(id);
            if (currentUser == null)
            {
                Console.WriteLine($"No user with id {id} was found");
                return;
            }

            Console.WriteLine("Enter new firstname, lastname, group and id separated with space: ");
            string[] input = Console.ReadLine()?.Split().ToArray();
            currentUser.RewriteInfo(input?[0], input?[1], Convert.ToInt32(input?[2]), Convert.ToInt32(input?[3]));
        }

        public void ViewUserInfo(int id)
        {
            User currentUser = FindUserById(id);

            if (currentUser == null)
            {
                Console.WriteLine($"No user with id {id} was found");
                return;
            }

            Console.WriteLine($@"<<<{id}>>>
Firstname: {currentUser.Firstname}
Lastname: {currentUser.Lastname}
Group: {currentUser.Group}");
            if (currentUser.DocumentsLanded.Count == 0)
            {
                Console.WriteLine("No documents landed out");
                return;
            }

            Console.WriteLine("Landed out documents:");
            foreach (Document document in currentUser.DocumentsLanded)
            {
                Console.WriteLine(document.Name);
            }
        }

        public void DisplayAllUsers()
        {
            if (_users.Count == 0)
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
            foreach (User user in _users)
            {
                Console.WriteLine($"User id: {user.Id}; {user.Firstname} {user.Lastname}, group {user.Group}");
            }
        }

        private User FindUserById(int id)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == id)
                {
                    return _users[i];
                }
            }

            return null;
        }

        public void ManageUserLending()
        {
            Console.Write("Enter user id: ");
            int inputId = Convert.ToInt32(Console.ReadLine());

            User currentUser = FindUserById(inputId);

            if (currentUser == null)
            {
                Console.WriteLine($"No user with id {inputId} was found");
                return;
            }

            Console.WriteLine(@"Choose option:
1 to lend out  
2 to receive the document
3 to return");
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

                    if (currentUser.DocumentsLanded.Count >= 5)
                    {
                        Console.WriteLine("Lended limit for user is reached");
                        return;
                    }

                    currentUser.LendOutDocument(currentDoc);
                    currentDoc.OwnerId = currentUser.Id;
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

                    currentUser.ReturnDocument(currentDoc);
                    currentDoc.OwnerId = -1;
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    return;
            }
        }

        public void UserKeywordSearch(string keyword)
        {
            foreach (User user in _users)
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

        private Document FindDocumentByName(string name)
        {
            for (int i = 0; i < _documents.Count; i++)
            {
                if (_documents[i].Name == name)
                {
                    return _documents[i];
                }
            }

            return null;
        }

        private void SortUsers(string option)
        {
            switch (option)
            {
                case "1":
                    _users.Sort((user1, user2) =>
                        String.Compare(user1.Firstname, user2.Firstname, StringComparison.Ordinal));
                    break;
                case "2":
                    _users.Sort((user1, user2) =>
                        String.Compare(user1.Lastname, user2.Lastname, StringComparison.Ordinal));
                    break;
                case "3":
                    _users.Sort((user1, user2) => user1.Group.CompareTo(user2.Group));
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
    }
}