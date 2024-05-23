using System;
using System.Collections.Generic;

namespace Lab8
{
    public class User
    {
        public string Firstname;
        public string Lastname;
        public int Group;
        public int Id;
        public readonly List<Document> DocumentsLanded = new List<Document>();

        public User(string firstname, string lastname, int group, int id)
        {
            Firstname = firstname;
            Lastname = lastname;
            Group = group;
            Id = id;
        }

        public void LendOutDocument(Document document)
        {
            DocumentsLanded.Add(document);
            document.Status = "lended out";
        }

        public void ReturnDocument(Document document)
        {
            if (!DocumentsLanded.Contains(document))
            {
                Console.WriteLine("No such document lended");
            }
            DocumentsLanded.Remove(document);
            document.Status = "in stock";
        }

        public void RewriteInfo(string firstName, string lastName, int group, int id)
        {
            Firstname = firstName;
            Lastname = lastName;
            Group = group;
            Id = id;
        }
    }
}