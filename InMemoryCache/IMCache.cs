using System.Configuration;

namespace InMemoryCache
{
    /// <summary>
    /// Singleton In Memory cache manager. Add, update, remove
    /// and fetch cache from memory.
    /// </summary>
    public sealed class IMCache
    {
        /// <summary>
        /// The capacity of the in memory cache
        /// </summary>
        private int capacity;

        /// <summary>
        /// Track the size of the in memory cache list
        /// </summary>
        private int count;

        /// <summary>
        /// Dictionary for the cache objects. Used for fast fetching of nodes.
        /// </summary>
        Dictionary<int, IMCacheNode> map;

        /// <summary>
        /// Double linked list used to track 'recently used' order of cache
        /// </summary>
        IMCacheDoubleLinkedList doubleLinkedList;

        private IMCache()
        {
            // Fetch the configured capacity of the cache
            capacity = int.Parse(ConfigurationManager.AppSettings["CacheCapacity"]);
            count = 0;
            map = new Dictionary<int, IMCacheNode>();
            doubleLinkedList = new IMCacheDoubleLinkedList();
        }

        public static IMCache Instance { get { return Nested.instance; } }

        /// <summary>
        /// Nested class used to create the instance from
        /// </summary>
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly IMCache instance = new IMCache();
        }

        // each time when access the node, we move it to the top
        public object Get(int key)
        {
            if (!map.ContainsKey(key)) return -1;
            IMCacheNode node = map[key];
            doubleLinkedList.RemoveNode(node);
            doubleLinkedList.AddNodeToTop(node);
            return node.Value;
        }

        /// <summary>
        /// Adds new key value pairs to the in memory cache list,
        /// or updates existing items with that key.
        /// </summary>
        /// <param name="key">Object key</param>
        /// <param name="value">Object value</param>
        public void AddOrUpdate(int key, object value)
        {
            // Update the value and move it to the top
            if (map.ContainsKey(key))
            {
                IMCacheNode node = map[key];
                doubleLinkedList.RemoveNode(node);
                node.Value = value;
                doubleLinkedList.AddNodeToTop(node);
            }
            else
            {
                // Remove the least recently used node in the event capacity is reached
                if (count == capacity)
                {
                    IMCacheNode leastRecentlyUsedNode = doubleLinkedList.RemoveLeastRecentlyUsedNode();
                    map.Remove(leastRecentlyUsedNode.Key);
                    count--;
                }

                // Add the newly passed node
                IMCacheNode node = new IMCacheNode(key, value);
                doubleLinkedList.AddNodeToTop(node);
                map[key] = node;
                count++;
            }
        }

        public void PrintAllCache()
        {
            var currNode = doubleLinkedList.GetHeadNode().Next;
            while(currNode.Next != null)
            {
                Console.WriteLine("IMCache Key-Value: " + currNode.Key + "-" + currNode.Value?.ToString());
                currNode = currNode.Next;
            }
            Console.WriteLine("=============================================");
        }

    }
}


