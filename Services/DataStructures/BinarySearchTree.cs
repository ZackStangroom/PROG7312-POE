// References:
// Binary Search Trees:
// - Cormen, T. H., Leiserson, C. E., Rivest, R. L., & Stein, C. (2009). Introduction to Algorithms (3rd ed.). MIT Press.
//   Chapter 12: Binary Search Trees
//   https://mitpress.mit.edu/9780262033848/introduction-to-algorithms/
//
// Tree Traversals:
// - GeeksforGeeks. (2024). Tree Traversals (Inorder, Preorder and Postorder).
//   https://www.geeksforgeeks.org/tree-traversals-inorder-preorder-and-postorder/
//
// Generic Types in C#:
// - Microsoft. (2024). Generic classes and methods.
//   https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics
//
// IComparer Interface:
// - Microsoft. (2024). IComparer<T> Interface.
//   https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icomparer-1

namespace PROG7312_POE.Services.DataStructures
{

    public class BinarySearchTree<T> where T : class
    {
        private BSTNode<T>? _root;
        private readonly IComparer<T> _comparer;

        public BinarySearchTree(IComparer<T> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
            _root = null;
        }

        // Insert an item into the tree
        public void Insert(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (_root == null)
            {
                _root = new BSTNode<T>(item);
            }
            else
            {
                InsertRecursive(_root, item);
            }
        }

        private void InsertRecursive(BSTNode<T> node, T item)
        {
            int comparison = _comparer.Compare(item, node.Data);

            if (comparison < 0)
            {
                // Insert into left subtree
                if (node.Left == null)
                {
                    node.Left = new BSTNode<T>(item);
                }
                else
                {
                    InsertRecursive(node.Left, item);
                }
            }
            else
            {
                // Insert into right subtree (including duplicates)
                if (node.Right == null)
                {
                    node.Right = new BSTNode<T>(item);
                }
                else
                {
                    InsertRecursive(node.Right, item);
                }
            }
        }

        // Search for an item in the tree using a predicate
        public T? Search(Func<T, bool> predicate)
        {
            return SearchRecursive(_root, predicate);
        }

        private T? SearchRecursive(BSTNode<T>? node, Func<T, bool> predicate)
        {
            if (node == null)
                return null;

            if (predicate(node.Data))
                return node.Data;

            // Search both subtrees since we're using a predicate
            var leftResult = SearchRecursive(node.Left, predicate);
            if (leftResult != null)
                return leftResult;

            return SearchRecursive(node.Right, predicate);
        }


        // Get all items in sorted order using in-order traversal
        // Reference: In-order traversal visits left subtree, root, then right subtree
        public List<T> InOrderTraversal()
        {
            var result = new List<T>();
            InOrderRecursive(_root, result);
            return result;
        }

        private void InOrderRecursive(BSTNode<T>? node, List<T> result)
        {
            if (node == null)
                return;

            InOrderRecursive(node.Left, result);
            result.Add(node.Data);
            InOrderRecursive(node.Right, result);
        }

        // Get all items in pre-order traversal
        // Reference: Pre-order traversal visits root, left subtree, then right subtree
        public List<T> PreOrderTraversal()
        {
            var result = new List<T>();
            PreOrderRecursive(_root, result);
            return result;
        }

        private void PreOrderRecursive(BSTNode<T>? node, List<T> result)
        {
            if (node == null)
                return;

            result.Add(node.Data);
            PreOrderRecursive(node.Left, result);
            PreOrderRecursive(node.Right, result);
        }

        // Get all items in post-order traversal
        // Reference: Post-order traversal visits left subtree, right subtree, then root
        public List<T> PostOrderTraversal()
        {
            var result = new List<T>();
            PostOrderRecursive(_root, result);
            return result;
        }

        private void PostOrderRecursive(BSTNode<T>? node, List<T> result)
        {
            if (node == null)
                return;

            PostOrderRecursive(node.Left, result);
            PostOrderRecursive(node.Right, result);
            result.Add(node.Data);
        }

        // Find all items that match a predicate
        public List<T> FindAll(Func<T, bool> predicate)
        {
            var result = new List<T>();
            FindAllRecursive(_root, predicate, result);
            return result;
        }

        private void FindAllRecursive(BSTNode<T>? node, Func<T, bool> predicate, List<T> result)
        {
            if (node == null)
                return;

            FindAllRecursive(node.Left, predicate, result);
            
            if (predicate(node.Data))
            {
                result.Add(node.Data);
            }

            FindAllRecursive(node.Right, predicate, result);
        }

        // Get the total count of nodes in the tree
        public int Count()
        {
            return CountRecursive(_root);
        }

        private int CountRecursive(BSTNode<T>? node)
        {
            if (node == null)
                return 0;

            return 1 + CountRecursive(node.Left) + CountRecursive(node.Right);
        }

        // Get the height of the tree
        public int GetHeight()
        {
            return GetHeightRecursive(_root);
        }

        private int GetHeightRecursive(BSTNode<T>? node)
        {
            if (node == null)
                return 0;

            int leftHeight = GetHeightRecursive(node.Left);
            int rightHeight = GetHeightRecursive(node.Right);

            return 1 + Math.Max(leftHeight, rightHeight);
        }

        // clear 
        public void Clear()
        {
            _root = null;
        }

        //check if tree is empty
        public bool IsEmpty()
        {
            return _root == null;
        }
    }

    // Node class for Binary Search Tree
    internal class BSTNode<T>
    {
        public T Data { get; set; }
        public BSTNode<T>? Left { get; set; }
        public BSTNode<T>? Right { get; set; }

        public BSTNode(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }
}