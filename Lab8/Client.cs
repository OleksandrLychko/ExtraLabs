using System;
using System.Collections.Generic;

namespace Lab8
{
    public class Client : User
    {
        public string Group;
        public readonly List<Document> DocumentsLended = new List<Document>();
        private string _option = "";

        public Client(string firstname, string lastname, string group, int id)
        {
            Firstname = firstname;
            Lastname = lastname;
            Group = group;
            Id = id;
        }

        public override void DisplayMenu(Library library)
        {
            while (_option != "0")
            {
                Console.WriteLine(@"Choose option:
1 to make request
0 to return");
                _option = Console.ReadLine();
                switch (_option)
                {
                    case "1":
                        Console.Write("Enter name of document for request: ");
                        string docName = Console.ReadLine();
                        Request newRequest = new Request(docName);
                        Document currentDoc = library.FindDocumentByName(docName);
                        if (currentDoc == null)
                        {
                            Console.WriteLine("No such document in library");
                            newRequest.Response = "not found";
                            library._requests.Add(newRequest);
                            break;
                        }

                        if (currentDoc.Status == "in stock")
                        {
                            Console.WriteLine("Document is in stock");
                            newRequest.Response = currentDoc.Status;
                            library._requests.Add(newRequest);
                            break;
                        }

                        Console.WriteLine("Document is now lended out");
                        newRequest.Response = currentDoc.Status;
                        library._requests.Add(newRequest);
                        break;
                    case "0":
                        _option = "";
                        return;
                }
            }
        }

        public void LendOutDocument(Document document)
        {
            DocumentsLended.Add(document);
            document.Status = "lended out";
        }

        public void ReturnDocument(Document document)
        {
            if (!DocumentsLended.Contains(document))
            {
                Console.WriteLine("No such document lended");
            }
            DocumentsLended.Remove(document);
            document.Status = "in stock";
        }

        public void RewriteInfo(string firstName, string lastName, string group, int id)
        {
            Firstname = firstName;
            Lastname = lastName;
            Group = group;
            Id = id;
        }
    }
}