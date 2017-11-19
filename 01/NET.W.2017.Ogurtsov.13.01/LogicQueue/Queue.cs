using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicQueue
{
    public sealed class Queue<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
    {
        private T[] array;
        private int head;
        private int tail;
        private int counter;
        private int capacity = 8;

        /// <inheritdoc />
        /// <summary>
        /// Number of elements in queue
        /// </summary>
        public int Count => this.counter;

        /// <summary>
        /// is queue readonly or not 
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="obj">IEnumerable object of elements</param>
        /// <exception cref="ArgumentNullException">argument must not be null</exception>
        public Queue(IEnumerable<T> obj) : this()
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(obj)} mut not be null");
            }

            foreach (var i in obj)
            {
                this.Enqueue(i);
            }
        }

        /// <summary>
        /// ctor
        /// </summary>
        public Queue()
        {
            this.array = new T[this.capacity];
        }

        /// <summary>
        /// Add element to queue
        /// </summary>
        /// <param name="obj">element to be added</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        public void Enqueue(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(obj)} must not be null");
            }

            if (this.counter == this.array.Length)
            {
                this.Resize(this.counter * 2);
            }

            this.array[this.tail] = obj;
            this.tail = (this.tail + 1) % this.array.Length;
            this.counter++;
        }

        /// <summary>
        /// Dequeue element
        /// </summary>
        /// <exception cref="InvalidOperationException">No elements in the queue</exception>
        /// <returns>removed element from the queue</returns>
        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("No elements in the queue");
            }

            var removedObj = this.array[this.head];
            this.array[this.head] = default(T);
            this.head = (this.head + 1) % this.array.Length;
            this.counter--;
            return removedObj;
        }

        /// <summary>
        /// Remove all elements from queue
        /// </summary>
        public void Clear()
        {
            this.array = new T[this.capacity];
            this.head = 0;
            this.tail = 0;
            this.counter = 0;
        }

        /// <summary>
        /// Check if the queue contains the element
        /// </summary>
        /// <param name="obj">element to be checked</param>
        /// <param name="comparer">rule of compearing elements</param>
        /// <returns>true if queue contains the element and false if not</returns>
        public bool Contains(T obj, EqualityComparer<T> comparer = null)
        {
            if (obj == null)
            {
                return false;
            }

            if (comparer == null)
            {
                comparer = EqualityComparer<T>.Default;
            }

            var size = this.Count;
            var index = this.head;
            while (size-- > 0)
            {
                if (index == this.array.Length)
                {
                    index = 0;
                }

                if (comparer.Equals(this.array[index], obj))
                {
                    return true;
                }

                index++;
            }

            return false;
        }

        /// <summary>
        /// Find first element in queue
        /// </summary>
        /// <exception cref="InvalidOperationException">No elements in the queue</exception>
        /// <returns>first element in queue</returns>
        public T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("No elements in the queue");
            }

            return this.array[this.head];
        }

        private void Resize(int newSize)
        {
            var newArray = new T[newSize];
            if (this.head < this.tail)
            {
                Array.Copy(this.array, this.head, newArray, 0, this.counter);
            }
            else
            {
                Array.Copy(this.array, this.head, newArray, 0, this.array.Length - this.head);
                Array.Copy(this.array, 0, newArray, this.array.Length - this.head, this.tail);
            }

            this.array = newArray;
            this.head = 0;
            this.tail = this.counter;
        }

        /// <summary>
        /// Delete first element in queue elements
        /// </summary>
        public void Trim()
        {
            if (this.Count != 0)
            {
                this.Resize(this.counter);
            }
        }

        /// <summary>
        /// Make iterator
        /// </summary>
        /// <returns>iterator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            this.Trim();
            return new Iterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private struct Iterator : IEnumerator<T>
        {
            private readonly Queue<T> queue;
            private int currentIndex;

            public Iterator(Queue<T> queue)
            {
                this.queue = queue;
                this.currentIndex = -1;
            }

            object IEnumerator.Current => this.Current;

            public T Current
            {
                get
                {
                    if (this.currentIndex == -1 || this.currentIndex == this.queue.Count)
                    {
                        throw new InvalidOperationException();
                    }

                    return this.queue.array[this.currentIndex];
                }
            }

            public bool MoveNext()
            {
                return ++this.currentIndex < this.queue.Count;
            }

            public void Reset()
            {
                this.currentIndex = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}
