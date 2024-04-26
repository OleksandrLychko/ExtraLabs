using System;

namespace Lab7
{
    public class LinkedList<T> : System.Collections.Generic.LinkedList<T>
    {
        private Node<T> head;

        public LinkedList()
        {
            head = null;
        }

        private Node<T> GetHead()
        {
            return head;
        }


        public void InsertAtBeginning(T data)
        {
            Node<T> newNode = new Node<T>(data);

            newNode.Next = head;
            head = newNode;
        }

        public void ShortenListByOne()
        {
            Node<T> current = GetHead();
            Node<T> previous = null;
            
            while (current.Next != null)
            {
                previous = current;
                current = current.Next;
            }

            previous.Next = null;
        }

        public int Length()
        {
            int length = 0;
            Node<T> current = head;

            while (current != null)
            {
                length++;
                current = current.Next;
            }

            return length;
        }

        public T this[int index]
        {
            get
            {
                int currentIndex = 0;
                Node<T> current = head;

                while (current != null)
                {
                    if (currentIndex == index)
                        return current.Value;

                    current = current.Next;
                    currentIndex++;
                }

                throw new IndexOutOfRangeException("Indexer getter out of range");
            }
            set
            {
                int currentIndex = 0;
                Node<T> current = head;

                while (current != null)
                {
                    if (currentIndex == index)
                    {
                        current.Value = value;
                        return;
                    }

                    current = current.Next;
                    currentIndex++;
                }

                throw new IndexOutOfRangeException("Indexer setter out of range");
            }
        }
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }

        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
}
