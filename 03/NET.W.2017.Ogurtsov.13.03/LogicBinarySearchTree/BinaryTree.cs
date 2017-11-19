using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogicBinarySearchTree
{
    public class BinaryTree<T> : IEnumerable<T>
    {
        private Node<T> head;

        public int Count => this.counter;

        private int counter;

        private readonly Comparison<T> comparer;

        /// <summary>
        /// create binary tree whith logic of comparer.(Defalut compearing is by hashcode)
        /// </summary>
        /// <param name="elements">elements to be added to the tree</param>
        /// <param name="comparer">logic of compearing elements in the tree</param>
        /// <exception cref="ArgumentNullException">Elements must not be null</exception>
        public BinaryTree(IEnumerable<T> elements, Comparison<T> comparer = null)
        {
            if (ReferenceEquals(elements, null))
            {
                throw new ArgumentNullException($"{nameof(elements)} must not be null");
            }

            if (typeof(T).GetInterfaces().Contains(typeof(IComparable))
                || typeof(T).GetInterfaces().Contains(typeof(IComparable<T>))
                || typeof(T).GetInterfaces().Contains(typeof(IComparer))
                || typeof(T).GetInterfaces().Contains(typeof(IComparer<T>)))
                this.comparer = ReferenceEquals(comparer, null) ? Comparer<T>.Default.Compare : comparer;
            else
            {
                throw new ArgumentException(
                    $"Type {nameof(T)} doesn't have default method Comparer. Grant your own comparer or implement " + $"IComparer or IComparable interfaces for your type");             
            }

            foreach (var value in elements)
            {
                this.Add(value);
            }
        }

        public BinaryTree(IEnumerable<T> elements, IComparer<T> comparer)
            : this(elements, comparer.Compare)
        {
        }

        /// <summary>
        /// Add element to the tree
        /// </summary>
        /// <param name="item">element to be added</param>
        /// <exception cref="ArgumentNullException">argument must not be null</exception>
        public void Add(T item)
        {
            if (ReferenceEquals(item, null))
            {
                throw new ArgumentNullException($"{nameof(item)} must not be null");
            }

            var node = new Node<T>(item);

            if (this.head == null)
            {
                this.head = node;
            }
            else
            {
                Node<T> current = this.head, parent = null;

                while (current != null)
                {
                    parent = current;
                    current = this.comparer(item, current.Value) < 0 ? current.Left : current.Right;
                }

                if (this.comparer(item, parent.Value) < 0)
                {
                    parent.Left = node;
                }
                else
                {
                    parent.Right = node;
                }
            }

            ++this.counter;
        }

        /// <summary>
        /// Add some elements to the tree
        /// </summary>
        /// <param name="elems">elements to be added</param>
        /// <exception cref="ArgumentNullException">Elements must not be null</exception>
        public void AddElements(IEnumerable<T> elems)
        {
            if (ReferenceEquals(elems, null))
            {
                throw new ArgumentNullException($"{nameof(elems)} must not be null");
            }

            foreach (var element in elems)
            {
                this.Add(element);
            }
        }

        /// <summary>
        /// Check if tree contains element
        /// </summary>
        /// <param name="value">element to be checked</param>
        /// <returns>true if tree contains the element otherwise false</returns>
        public bool Contains(T value)
        {
            if (ReferenceEquals(value, null))
            {
                return false;
            }

            var curent = this.head;

            while (curent != null)
            {
                if (this.comparer(curent.Value, value) == 0)
                {
                    return true;
                }

                curent = this.comparer(value, curent.Value) > 0 ? curent.Right : curent.Left;
            }

            return false;
        }

        /// <summary>
        /// Remove all elements from the tree
        /// </summary>
        public void Clear()
        {
            this.head = null;
            this.counter = 0;
        }

        /// <summary>
        /// Remove element from the tree
        /// </summary>
        /// <param name="value">element to be removed</param>
        /// <exception cref="ArgumentNullException">argument must not be null</exception>
        public void Remove(T value)
        {
            if (ReferenceEquals(value, null))
            {
                throw new ArgumentNullException($"{nameof(value)} must not be null");
            }

            if (!this.Contains(value))
            {
                return;
            }

            var curent = this.head;
            Node<T> parentCurent = null;

            while (this.comparer(curent.Value, value) != 0)
            {
                parentCurent = curent;

                curent = this.comparer(value, curent.Value) > 0 ? curent.Right : curent.Left;
            }

            if (curent.Right == null)
            {
                if (parentCurent == null)
                {
                    this.head = curent.Left;
                }
                else
                {
                    if (this.comparer(curent.Value, parentCurent.Left.Value) == 0)
                    {
                        parentCurent.Left = curent.Left;
                    }
                    else
                    {
                        parentCurent.Right = curent.Right;
                    }
                }
            }
            else
            {
                var temp = curent.Right;
                Node<T> parentTemp = null;

                while (temp.Left != null)
                {
                    parentTemp = temp;
                    temp = temp.Left;
                }

                if (parentTemp != null)
                {
                    parentTemp.Left = temp.Right;
                }
                else
                {
                    curent.Right = temp.Right;
                }

                curent.Value = temp.Value;
            }

            this.counter--;
        }

        /// <summary>
        /// Inorder version to watch on the tree
        /// </summary>
        /// <returns>IEnumerable representation of the tree </returns>
        public IEnumerable<T> Inorder()
        {
            if (this.head == null)
            {
                yield break;
            }

            var stack = new Stack<Node<T>>();
            var node = this.head;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node.Value;
                    node = node.Right;
                }
                else
                {
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }

        /// <summary>
        /// Preoder version to watch on the tree
        /// </summary>
        /// <returns>IEnumerable representation of the tree </returns>
        public IEnumerable<T> Preorder()
        {
            if (this.head == null)
            {
                yield break;
            }

            var stack = new Stack<Node<T>>();
            stack.Push(this.head);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node.Value;
                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }

                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }
            }
        }

        /// <summary>
        /// Postoder version to watch on the tree
        /// </summary>
        /// <returns>IEnumerable representation of the tree </returns>
        public IEnumerable<T> Postorder()
        {
            if (this.head == null)
            {
                yield break;
            }

            var stack = new Stack<Node<T>>();
            var node = this.head;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else
                    {
                        yield return node.Value;
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }

                    stack.Push(node);
                    node = node.Left;
                }
        }
    }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Inorder().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }


    internal sealed class Node<T>
    {
        public T Value { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        /// <summary>
        /// Create node of the tree with element inside
        /// </summary>
        /// <param name="obj">element inside the node</param>
        /// <exception cref="ArgumentNullException">argument must not be null</exception>
        public Node(T obj)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException($"{nameof(obj)} must not be null");
            }

            this.Value = obj;
        }
    }
}
