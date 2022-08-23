namespace InMemoryCacheTests;
using System.Configuration;
using InMemoryCache;

public class InMemoryCacheTests
{
    private IMCache cache;

    [SetUp]
    public void TestSetup()
    {
        ConfigurationHelper.AddOrUpdateAppSettings("CacheCapacity", "3");
        cache = IMCache.Instance;
    }

    [Test, Order(1)]
    public void Test_IMCache_CacheObjectShouldBeAdded()
    {
        cache.AddOrUpdate(1, "Value1");

        Assert.That(cache.Get(1), Is.EqualTo("Value1"));
    }

    [Test, Order(2)]
    public void Test_IMCache_CacheObjectShouldBeUpdated()
    {
        cache.AddOrUpdate(1, 1);

        Assert.That(cache.Get(1), Is.EqualTo(1));
    }

    [Test, Order(3)]
    public void Test_IMCache_LRUCacheObjectShouldBeRemoved()
    {
        cache.AddOrUpdate(2, 2);
        cache.AddOrUpdate(3, "Value3");
        cache.Get(1);
        cache.AddOrUpdate(4, "Value4");

        Assert.That(cache.Get(2), Is.EqualTo(-1));
    }
}
