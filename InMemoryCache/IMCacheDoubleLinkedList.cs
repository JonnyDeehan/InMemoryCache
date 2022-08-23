namespace InMemoryCache
{
    /// <summary>
    /// Double linked list used to track the order in which
    /// in memory cache objects are stored.
    /// </summary>
    public class IMCacheDoubleLinkedList
    {
        /// <summary>
        /// Head node used to mark the top of the linked list
        /// </summary>
        private IMCacheNode head;

        /// <summary>
        /// Tail node used to mark the least recently used object
        /// </summary>
        private IMCacheNode tail;

        public IMCacheDoubleLinkedList()
        {
            head = new IMCacheNode();
            tail = new IMCacheNode();

            // Link the head and tail nodes
            head.Next = tail;
            tail.Previous = head;
        }

        /// <summary>
        /// Shifts the passed node to the top of the linked list
        /// </summary>
        /// <param name="node">Node to add to the top</param>
        public void AddNodeToTop(IMCacheNode node)
        {
            node.Next = head.Next;
            head.Next.Previous = node;
            node.Previous = head;
            head.Next = node;
        }

        /// <summary>
        /// Removes passed node from the linked list
        /// </summary>
        /// <param name="node">Node to remove</param>
        public void RemoveNode(IMCacheNode node)
        {
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            node.Next = null;
            node.Previous = null;
        }

        /// <summary>
        /// Remove the node that is the 'Least recently used', i.e. the bottom of the list.
        /// In this case, nodes are added to the top when fetched from the list,
        /// or simply updated/added to the list for the first time.
        /// </summary>
        /// <returns></returns>
        public IMCacheNode RemoveLeastRecentlyUsedNode()
        {
            IMCacheNode target = tail.Previous;

            Console.WriteLine("Node Removed: " + target.Key + "-" + target.Value?.ToString());
            Console.WriteLine("=============================================");

            RemoveNode(target);
            return target;
        }

        public IMCacheNode GetHeadNode()
        {
            return head;
        }
    }
}

