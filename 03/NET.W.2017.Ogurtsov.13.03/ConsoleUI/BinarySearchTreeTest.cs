using System;
using System.Collections.Generic;
using LogicBinarySearchTree;

namespace ConsoleUI
{
    internal class BinarySearchTreeTest
    {
        private static void Main(string[] args)
        {
            #region BinaryTree Int32 with default and not default comparers

            Console.WriteLine("Create new binary tree<int> with default comparer");
            BinaryTree<int> binaryTreeInt = new BinaryTree<int>(new int[] { 10, 15, -7, 7, 14, 3, 2, 1, 22 });
            Show(binaryTreeInt);
            Console.WriteLine("Tree contains 22: " + binaryTreeInt.Contains(22) + "\n Tree contains -16:" + binaryTreeInt.Contains(-16));

            Console.WriteLine("Removing 3");
            binaryTreeInt.Remove(3);
            Show(binaryTreeInt);

            Console.WriteLine("Create new binary trr<int> with logic of compearing int by Math.Abs");
            BinaryTree<int> binaryTreeInt2 = new BinaryTree<int>(new int[] { 10, 15, -7, 7, 14, 3, 2, 1, 22 }, NoneDefaultComparisionForIntTree);
            Show(binaryTreeInt2);
            Console.WriteLine("Tree contains 22: " + binaryTreeInt2.Contains(22) + "\n Tree contains -16:" + binaryTreeInt2.Contains(-16));

            Console.WriteLine("Removing 3");
            binaryTreeInt2.Remove(3);
            Show(binaryTreeInt2);

            Console.WriteLine("Create new trii but with interface parametr");
            BinaryTree<int> newByTree = new BinaryTree<int>(new[] { -123, 32, 312, 31, 5, 12, 13, 515 }, new ComparerForBinaryTree());
            Show(newByTree);

            #endregion

            #region BiTree String with default and nonedefault comparers

            Console.WriteLine("Create a tree of string with default comparer");
            BinaryTree<string> biTreeStrings = new BinaryTree<string>(new[] { "onde", "two", "three", "four", "five", "six", "seven" });
            Show(biTreeStrings);

            Console.WriteLine("Tree contains onde: " + biTreeStrings.Contains("onde") + "\nTree contains nine" + biTreeStrings.Contains("nine"));

            Console.WriteLine("Remove four");
            biTreeStrings.Remove("four");
            Show(biTreeStrings);


            Console.WriteLine("Create a tree of string with comparer of lenth of string");
            BinaryTree<string> biTreeStrings2 = new BinaryTree<string>(new[] { "onde", "two", "three", "four", "five", "six", "seven" });
            Show(biTreeStrings2);

            Console.WriteLine("Tree contains onde: " + biTreeStrings2.Contains("onde") + "\nTree contains nine" + biTreeStrings2.Contains("nine"));

            Console.WriteLine("Remove four");
            biTreeStrings.Remove("four");
            Show(biTreeStrings2);

            Console.ReadKey();
            #endregion>
        }
        #region Show functions

        public static void Show<T>(BinaryTree<T> binaryTree)
        {
            Console.WriteLine("Tree now consist of " + binaryTree.Count + " elements(inorder version):");
            foreach (var element in binaryTree.Inorder())
            {
                Console.Write(element + " ");
            }

            Console.WriteLine("\n");
            Console.WriteLine("Preoder version of the same tree");
            foreach (var element in binaryTree.Preorder())
            {
                Console.Write(element + " ");
            }

            Console.WriteLine("\n");
        }
        #endregion
        public static int NoneDefaultComparisionForIntTree(int obj1, int obj2)
            => Math.Abs(obj1).CompareTo(Math.Abs(obj2));

        public static int NoneDefaultComparisionForStringTree(string object1, string object2)
            => object1.Length.CompareTo(object2.Length);

        public class NoneDefaultStringComparer : IEqualityComparer<string>
        {
            public bool Equals(string string1, string string2)
            {
                return string1.Equals(string2, StringComparison.InvariantCultureIgnoreCase);
            }

            public int GetHashCode(string a)
            {
                return a.GetHashCode();
            }
        }
    }
}
