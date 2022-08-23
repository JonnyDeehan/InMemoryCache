namespace InMemoryCache
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test the IMCache functionality and print to the console log.
            IMCache cache = IMCache.Instance;

            cache.AddOrUpdate(1, "Value1");
            cache.AddOrUpdate(2, 2);
            cache.AddOrUpdate(3, "Value3");

            cache.PrintAllCache();

            // Fetch cache 1
            cache.Get(1);

            cache.PrintAllCache();

            cache.AddOrUpdate(4, "Value4");    

            cache.PrintAllCache();
        }
    }
}