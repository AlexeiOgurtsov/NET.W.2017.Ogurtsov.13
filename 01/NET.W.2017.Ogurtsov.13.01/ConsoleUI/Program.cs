using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Queue of integers

            Queue<int> intQueue = new Queue<int>(new int[] { 1, 2, 3, 4, 5, 2 });
            Show(intQueue);

            Console.WriteLine("Add some elements to the queue");
            intQueue.Enqueue(10);
            intQueue.Enqueue(-41);
            Show(intQueue);

            Console.WriteLine("Remove 3 elements from the queue");
            intQueue.Dequeue();
            intQueue.Dequeue();
            intQueue.Dequeue();
            Show(intQueue);

            Console.WriteLine("First element in the queue is : " + intQueue.Peek());

            Console.WriteLine("Delete the queue");
            intQueue.Clear();
            Show(intQueue);

            #endregion

            #region Queue of strings

            Console.WriteLine("\n\nNew Queue of strings: ");
            Queue<string> stringQueue = new Queue<string>(new[] { "first", "second", "third", "fourth", "fifth" });
            Show(stringQueue);

            Console.WriteLine("Add some elements to the queue");
            stringQueue.Enqueue("sixth");
            stringQueue.Enqueue("seventh");
            Show(stringQueue);

            Console.WriteLine("Remove 3 elements from the queue");
            stringQueue.Dequeue();
            stringQueue.Dequeue();
            stringQueue.Dequeue();
            Show(stringQueue);

            Console.WriteLine($"Queue contains sixth : {stringQueue.Contains("sixth")}");
            Console.WriteLine($"Queue contains Sixth with defaultcomparer : {stringQueue.Contains("Sixth")}");
            Console.WriteLine($"Queue contains Sixth with nonedafaultcomparer : {stringQueue.Contains("Sixth", new NoneDefaultStringComparer())}");

            Console.WriteLine("\nFirst element in the queue is : " + stringQueue.Peek());

            Console.WriteLine("Delete the queue");
            stringQueue.Clear();
            Show(stringQueue);
            Console.ReadKey();

            #endregion
        }

        public static void Show<T>(Queue<T> queue)
        {
            Console.WriteLine("Queue now consist if " + queue.Count + " elements: ");
            foreach (T element in queue)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine("\n");
        }

        public class NoneDefaultStringComparer : IEqualityComparer<string>
        {
            public bool Equals(string str1, string str2)
            {
                return str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(string a)
            {
                return a.GetHashCode();
            }
        }
    }
}
