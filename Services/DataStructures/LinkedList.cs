namespace PROG7312_POE.Services.DataStructures
{
    public class LinkedList<T>
    {
        private Node<T>? _head;
        private Node<T>? _tail;
        private int _count;

        public int Count => _count;
        public bool IsEmpty => _head == null;

        // Adds an item to the end of the list
        public void Add(T item)
        {
            var newNode = new Node<T>(item);

            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail!.Next = newNode;
                newNode.Previous = _tail;
                _tail = newNode;
            }

            _count++;
        }

        // Adds an item to the beginning of the list
        public void AddFirst(T item)
        {
            var newNode = new Node<T>(item);

            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                newNode.Next = _head;
                _head.Previous = newNode;
                _head = newNode;
            }

            _count++;
        }

        // Retrieves the first item in the list
        public T? GetFirst()
        {
            return _head != null ? _head.Data : default(T);
        }

        public T? GetLast()
        {
            return _tail != null ? _tail.Data : default(T);
        }

        // Removes the first occurrence of the specified item from the list
        public bool Remove(T item)
        {
            var current = _head;

            while (current != null)
            {
                if (current.Data != null && current.Data.Equals(item))
                {
                    RemoveNode(current);
                    return true;
                }
                current = current.Next;
            }

            return false;
        }


        // Removes the first item from the list
        private void RemoveNode(Node<T> node)
        {
            if (node.Previous != null)
            {
                node.Previous.Next = node.Next;
            }
            else
            {
                _head = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Previous = node.Previous;
            }
            else
            {
                _tail = node.Previous;
            }

            _count--;
        }

        // Finds all items that match the given predicate
        public LinkedList<T> FindAll(Func<T, bool> predicate)
        {
            var result = new LinkedList<T>();
            var current = _head;

            while (current != null)
            {
                if (current.Data != null && predicate(current.Data))
                {
                    result.Add(current.Data);
                }
                current = current.Next;
            }

            return result;
        }

        // Finds the first item that matches the given predicate
        public T? Find(Func<T, bool> predicate)
        {
            var current = _head;

            while (current != null)
            {
                if (current.Data != null && predicate(current.Data))
                {
                    return current.Data;
                }
                current = current.Next;
            }

            return default(T);
        }

        // Executes the given action for each item in the list
        public void ForEach(Action<T> action)
        {
            var current = _head;
            while (current != null)
            {
                if (current.Data != null)
                {
                    action(current.Data);
                }
                current = current.Next;
            }
        }
    }

    // Node class representing each element in the linked list
    internal class Node<T>
    {
        public T? Data { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Previous { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }
}