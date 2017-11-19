using System;

namespace LogicMatrix
{
    public abstract class Matrixes<T>
    {
        protected internal T[,] matrix;
        private int dimension;

        public int Dimension => this.dimension;

        public EventHandler ChangeHandler;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="n">dimmension of matrix</param>
        protected Matrixes(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(n)} must be a positive number");
            }

            this.dimension = n;
            this.matrix = new T[this.dimension, this.dimension];
        }

        /// <summary>
        /// Change element on postition (i,j)
        /// </summary>
        /// <param name="i">line postition of element to be chanched</param>
        /// <param name="j">column position of element to be changed</param>
        /// <param name="obj">new element</param>
        /// <exception cref="ArgumentOutOfRangeException">i and j must be greater then 0 but less then dimension of matrix</exception>
        /// <exception cref="ArgumentNullException">new element must not be null</exception>
        public virtual void ChangeElement(int i, int j, T obj)
        {
            if (i < 0 || j < 0 || j >= this.Dimension || i >= this.Dimension)
            {
                throw new ArgumentOutOfRangeException($"{nameof(i)} and {nameof(j)} must be from 0 to {this.Dimension - 1}"); 
            }

            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException($"{nameof(obj)} must not be null");
            }

            this.matrix[i, j] = obj;

            this.MatrixChanged(this, new EventArgs());
        }

        #region Addition

        /// <summary>
        /// Add matrix to this one
        /// </summary>
        /// <param name="obj">matrix to be added</param>
        /// <param name="additionFunc">logic of addition 2 elements in matrixes</param>
        /// <exception cref="ArgumentNullException">parametres must not be null</exception>
        /// <exception cref="InvalidOperationException">type of elements in both matrixes must be the same</exception>
        /// <returns>this matrix(changed) if types are the same and new SquareMatrix if types are different</returns>
        public virtual Matrixes<T> AddMatrixes(Matrixes<T> obj, Func<T, T, T> additionFunc)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException($"{nameof(obj)} must not be null");
            }

            if (ReferenceEquals(additionFunc, null))
            {
                throw new ArgumentNullException($"{nameof(additionFunc)} must not be null");
            }

            if (this.matrix.GetType() != obj.matrix.GetType())
            {
                throw new InvalidOperationException("Type of elements in matrixes must be the same");
            }

            if (this.GetType() == obj.GetType())
            {
                this.AddMatrix(obj, additionFunc);
                return this;
            }

            var newMatrix = new SquareMatrix<T>(Math.Max(obj.Dimension, this.Dimension));
            for (var i = 0; i < newMatrix.Dimension; i++)
            {
                for (var j = 0; j < newMatrix.Dimension; j++)
                {
                    if (this.Dimension >= obj.Dimension)
                    {
                        newMatrix.matrix[i, j] = this.matrix[i, j];
                    }
                    else
                    {
                        newMatrix.matrix[i, j] = obj.matrix[i, j];
                    }                    
                }                
            }

            newMatrix.AddMatrix(this.Dimension >= obj.Dimension ? obj : this, additionFunc);

            return newMatrix;
        }

        private void AddMatrix(Matrixes<T> obj, Func<T, T, T> additionFunc)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException($"{nameof(obj)} must not be null");
            }

            if (this.matrix.GetType() != obj.matrix.GetType())
            {
                throw new InvalidOperationException("Type of elements in matrixes must be the same");
            }

            var maxDim = Math.Max(obj.Dimension, this.Dimension);
            var minDim = Math.Min(obj.Dimension, this.Dimension);

            var newMatrix = new T[maxDim, maxDim];

            for (var i = 0; i < minDim; i++)
            {
                for (var j = 0; j < minDim; j++)
                {
                    newMatrix[i, j] = additionFunc(this.matrix[i, j], obj.matrix[i, j]);
                }
            }

            T[,] arr = null;
            if (obj.Dimension > this.Dimension)
            {
                arr = obj.matrix;
            }
            else
            {
                if (obj.Dimension < this.Dimension)
                {
                    arr = this.matrix;
                }
            }

            if (arr != null)
            {
                for (var i = 0; i < maxDim; i++)
                {
                    for (var j = minDim; j < maxDim; j++)
                    {
                        newMatrix[i, j] = arr[i, j];
                    }
                }

                for (var i = minDim; i < maxDim; i++)
                {
                    for (var j = 0; j < maxDim; j++)
                    {
                        newMatrix[i, j] = arr[i, j];
                    }
                }
            }

            this.matrix = newMatrix;
            this.dimension = maxDim;
        }
        #endregion

        private void MatrixChanged(object o, EventArgs args)
        {
            this.ChangeHandler(o, args);
        }
    }
}
