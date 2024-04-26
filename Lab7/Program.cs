using System;

namespace Lab7
{
    public class Program
    {
        public static void Main()
        {
            LinkedList<short> nums = new LinkedList<short>();
            
            nums.InsertAtBeginning(1);
            nums.InsertAtBeginning(4);
            nums.InsertAtBeginning(9);
            nums.InsertAtBeginning(5);
            nums.InsertAtBeginning(3);
            nums.InsertAtBeginning(4);
            nums.InsertAtBeginning(8);
            nums.InsertAtBeginning(1);
            nums.InsertAtBeginning(2);
            nums.InsertAtBeginning(6);
            nums.InsertAtBeginning(9);
            
            int choice = 0;
            
            while (choice != 6)
            {
                DisplayList(nums);
                DisplayOptions();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        InsertNum(nums);
                        break;
                    case 2:
                        GetFirstMultipleOfThree(nums);
                        break;
                    case 3:
                        GetProductOfLessThanAvg(nums);
                        break;
                    case 4:
                        LinkedList<short> newList = GetListOfDivisibleByThree(nums);
                        DisplayList(newList);
                        break;
                    case 5: 
                        RemoveElementsLessThanAvg(nums);
                        break;
                    case 6:
                        return;
                    default:
                        return;

                }
            }
        }

        private static void DisplayList(LinkedList<short> list)
        {
            Console.Write("List: ");
            for (int i = 0; i < list.Length(); i++)
            {
                Console.Write($"{list[i]} ");
            }

            Console.WriteLine();
        }

        private static void DisplayOptions()
        {
            Console.WriteLine(
                @"Choose option:
1- Add element
2- Find first multiple of 3
3- Find product of elements less than the average
4- New list of elements divisible by 3
5- Remove elements greater than the average
6- Exit");
        }

        private static void InsertNum(LinkedList<short> list)
        {
            Console.WriteLine("Number to insert: ");
            short insertNum = (short)Convert.ToInt32(Console.ReadLine());
            list.InsertAtBeginning(insertNum);
        }

        private static void GetFirstMultipleOfThree(LinkedList<short> list)
        {
            if (list.Length() == 0)
            {
                Console.WriteLine("The list is empty");
                return;
            }
            
            short firstMultipleOfThree = 0;
            int index = -1;

            for (int i = 0; i < list.Length(); i++)
            {
                if (list[i] % 3 == 0)
                {
                    firstMultipleOfThree = list[i];
                    index = i;
                    break;
                }
            }
            
            if (index == -1)
            {
                Console.WriteLine("No numbers multiple of three in the list");
                return;
            }
            
            Console.WriteLine($"First multiple of three is {firstMultipleOfThree}. It is located at index {index}");
        }
        
        private static double GetAverage(LinkedList<short> list)
        {
            short listSize = (short)list.Length();
            short listSum = 0;
            
            for (int i = 0; i < list.Length(); i++)
            {
                listSum += list[i];
            }
            
            return (double)listSum / listSize;
        }
        
        private static void GetProductOfLessThanAvg(LinkedList<short> list)
        {
            double avg = GetAverage(list);
            int product = 1;
            
            for (int i = 0; i < list.Length(); i++)
            {
                if (list[i] < avg)
                {
                    product *= list[i];
                }
            }

            Console.WriteLine($"Product is {product}");
        }

        private static LinkedList<short> GetListOfDivisibleByThree(LinkedList<short> list)
        {
            LinkedList<short> result = new LinkedList<short>();
            
            for (int i = 0; i < list.Length(); i++)
            {
                if (list[i] % 3 == 0)
                {
                    result.InsertAtBeginning(list[i]);
                }
            }
            
            return result;
        }

        private static void RemoveElementsLessThanAvg(LinkedList<short> list)
        {
            double avg = GetAverage(list);
            Console.WriteLine($"Average: {avg}");
            int index = 0;

            while (list.Length() > index)
            {
                if (list[index] > avg)
                {
                    for (int i = index; i < list.Length() - 1; i++)
                    {
                        list[i] = list[i + 1];
                    }
                    
                    list.ShortenListByOne();
                    index--;
                }

                index++;
            }
        }
    }
}