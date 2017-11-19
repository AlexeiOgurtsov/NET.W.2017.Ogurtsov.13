using System;
using System.Collections.Generic;
using System.Linq;

namespace LogicMatrix
{
    public class SymmetricalMatrix<T> : Matrixes<T>
    {
        /// <summary>
        /// create new symmetrical matrix 
        /// </summary>
        /// <param name="n">dimension of matrix</param>
        /// <param name="elems">half of elements in matrix</param>
        public SymmetricalMatrix(int n, IEnumerable<T> elems) : base(n)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(n)} must be a positive number");
            }

            if (ReferenceEquals(elems, null))
            {
                throw new ArgumentNullException($"{nameof(elems)} must not be null");
            }

            if (elems.Count() != NumberOfExpectedElems(n))
            {
                throw new ArgumentException ($"Numaer of elements in {nameof(elems)} must be {NumberOfExpectedElems(n)}");
            }

            int index = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    matrix[i, j] = elems.ElementAt(index);
                    matrix[j, i] = elems.ElementAt(index++);
                }
            }
        }

        /// <summary>
        /// create new empty symmetrical matrix
        /// </summary>
        /// <param name="n">dimension of matrix</param>
        public SymmetricalMatrix(int n) : base(n)
        {
        }

        /// <summary>
        /// Change element on position (i,j) and (j,i) 
        /// </summary>
        /// <param name="i">line position of element</param>
        /// <param name="j">column position of eleemnt</param>
        /// <exception cref="ArgumentNullException">new element must not be null</exception>
        /// <exception cref="ArgumentOutOfRangeException">i and j must be greater then 0 and less then dimension of matrix</exception>
        /// <param name="obj">new element</param>
        public override void ChangeElement(int i, int j, T obj)
        {
            base.ChangeElement(j, i, obj);
            base.ChangeElement(i, j, obj);
        }

        private int NumberOfExpectedElems(int n)
        {
            int res = 0;
            for (int i = 1; i < n; i++)
            {
                res += i;
            }

            return res;
        }
    }

    public class SquareMatrix<T> : Matrixes<T>
    {
        /// <summary>
        /// create new Square matrix 
        /// </summary>
        /// <param name="n">dimension of matrix</param>
        /// <param name="elems">elements of matrix</param>
        /// <exception cref="ArgumentNullException">elements must nit be null</exception>
        /// <exception cref="ArgumentException">Number of elements must be the same as number of elements in square matrix</exception>
        public SquareMatrix(int n, IEnumerable<T> elems) : base(n)
        {
            if (ReferenceEquals(elems, null))
            {
                throw new ArgumentNullException($"{nameof(elems)} must be not null");
            }

            if (elems.Count() != n * n)
            {
                throw new ArgumentException($"Wrong number of elements in {nameof(elems)}." + $"\n Number of elements with this dimensions must be {n * n}"); 
            }

            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = elems.ElementAt(index++);
                }
            }
        }

        /// <summary>
        /// create empty square matrix
        /// </summary>
        /// <param name="n">dimension of matrix</param>
        public SquareMatrix(int n)
            : base(n)
        {
        }

    }

    public class DiagonalMatrix<T> : Matrixes<T>
    {
        /// <summary>
        /// create new Diagonal matrix
        /// </summary>
        /// <param name="n">dimension of matrix</param>
        /// <param name="elems">elements on main diagonal</param>
        /// <exception cref="ArgumentNullException">elemets must not be null</exception>
        /// <exception cref="ArgumentException">Number of elements must be the same as number of element on main diagonal in matrix</exception>
        public DiagonalMatrix(int n, IEnumerable<T> elems) : base(n)
        {
            if (ReferenceEquals(elems, null))
            {
                throw new ArgumentNullException($"{nameof(elems)} must not be null");
            }

            if (elems.Count() != n)
            {
                throw new ArgumentException($"{nameof(elems)} must consist of {n} elements");
            }

            for (int i = 0, j = 0; i < n; i++, j++)
            {
                matrix[i, j] = elems.ElementAt(i);
            }
        }

        /// <summary>
        /// create empty diagonal matrix
        /// </summary>
        /// <param name="n">dimension of matrix</param>
        public DiagonalMatrix(int n) : base(n)
        {
        }

        /// <summary>
        /// chanche element on main diagonal
        /// </summary>
        /// <param name="i">line position of element</param>
        /// <param name="j">column position of element</param>
        /// <param name="obj">new element</param>
        /// <exception cref="ArgumentNullException">new element must not be null</exception>
        public override void ChangeElement(int i, int j, T obj)
        {
            if (i != j)
            {
                throw new ArgumentException($"{nameof(i)} and {nameof(j)} must be the same in diagonal matrix");
            }

            base.ChangeElement(i, j, obj);
        }
    }
}
