namespace InMemoryCache
{
    /// <summary>
    /// In memory cache node. Capture cache key-value data.
    /// Stored as part of a linked list to track the least recently used cache.
    /// </summary>
    public class IMCacheNode
    {
        /// <summary>
        /// Cache key
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Cache object
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Previous node
        /// </summary>
        public IMCacheNode Previous { get; set; }

        /// <summary>
        /// Next node
        /// </summary>
        public IMCacheNode Next { get; set; }

        public IMCacheNode() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public IMCacheNode(int key, object value)
        {
            Key = key;
            Value = value;
        }
    }
}

