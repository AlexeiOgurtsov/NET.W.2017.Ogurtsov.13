using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    internal class ComparerForBinaryTree : IComparer<int>
    {
        public int Compare(int obj1, int obj2)
            => Math.Abs(obj1 - obj2);
    }
}