using Microsoft.AspNetCore.Mvc;

namespace Experimental.Controllers;
[ApiController]
[Route("[controller]/[Action]")]
public class TestEnumerableController : ControllerBase
{
    [HttpGet]
    public void GetEnumerableResult()
    {
        Console.WriteLine("===========GetEnumerableResult=============");
        MyGcService.Reset();
        MyTimer.Start();
        var guidPool = Enumerable.Range(0, 1000000).Select(i => Guid.NewGuid());
        MyGcService.PrintMemorySize("Generate");
        MyTimer.PrintAndReset("Generate");
        var take = guidPool.Take(3).ToList();
        MyGcService.PrintMemorySize("take");
        MyTimer.PrintAndReset("Generate");
        var count = guidPool.Count();
        MyGcService.PrintMemorySize("count");
        MyTimer.PrintAndReset("count");
        var where = guidPool.Where(x=> x == Guid.Empty).ToList();
        MyGcService.PrintMemorySize("where");
        MyTimer.PrintAndReset("where");
    }
    
    [HttpGet]
    public void GetListResult()
    {
        Console.WriteLine("===========GetListResult=============");
        MyGcService.Reset();
        MyTimer.Start();
        var guidPool = Enumerable.Range(0, 1000000).Select(i => Guid.NewGuid()).ToList();
        MyGcService.PrintMemorySize("Generate");
        MyTimer.PrintAndReset("Generate");
        var take = guidPool.Take(3).ToList();
        MyGcService.PrintMemorySize("take");
        MyTimer.PrintAndReset("Generate");
        var count = guidPool.Count();
        MyGcService.PrintMemorySize("count");
        MyTimer.PrintAndReset("count");
        var where = guidPool.Where(x=> x == Guid.Empty).ToList();
        MyGcService.PrintMemorySize("where");
        MyTimer.PrintAndReset("where");
    }
    
    [HttpGet]
    public void GetIEnumerable()
    {
        var guidProvider = new GuidProvider();
        Console.WriteLine("===========GetIEnumerable=============");
        MyGcService.Reset();
        MyTimer.Start();
        var guidPool = guidProvider.GetIEnumerable();
        MyGcService.PrintMemorySize("Generate");
        MyTimer.PrintAndReset("Generate");
        var take = guidPool.Take(3).ToList();
        MyGcService.PrintMemorySize("take");
        MyTimer.PrintAndReset("Generate");
        var count = guidPool.Count();
        MyGcService.PrintMemorySize("count");
        MyTimer.PrintAndReset("count");
        var where = guidPool.Where(x=> x == Guid.Empty).ToList();
        MyGcService.PrintMemorySize("where");
        MyTimer.PrintAndReset("where");
    }
    
    [HttpGet]
    public void GetList()
    {
        var guidProvider = new GuidProvider();
        Console.WriteLine("===========GetList=============");
        MyGcService.Reset();
        MyTimer.Start();
        var guidPool = guidProvider.GetList();
        MyGcService.PrintMemorySize("Generate");
        MyTimer.PrintAndReset("Generate");
        var take = guidPool.Take(3).ToList();
        MyGcService.PrintMemorySize("take");
        MyTimer.PrintAndReset("Generate");
        var count = guidPool.Count();
        MyGcService.PrintMemorySize("count");
        MyTimer.PrintAndReset("count");
        var where = guidPool.Where(x=> x == Guid.Empty).ToList();
        MyGcService.PrintMemorySize("where");
        MyTimer.PrintAndReset("where");
    }

    [HttpGet]
    public void GetIEnumerableFromList()
    {
        var guidProvider = new GuidProvider();
        Console.WriteLine("===========GetIEnumerableFromList=============");
        MyGcService.Reset();
        MyTimer.Start();
        var guidPool = guidProvider.GetIEnumerableFromList();
        MyGcService.PrintMemorySize("Generate");
        MyTimer.PrintAndReset("Generate");
        var take = guidPool.Take(3).ToList();
        MyGcService.PrintMemorySize("take");
        MyTimer.PrintAndReset("Generate");
        var count = guidPool.Count();
        MyGcService.PrintMemorySize("count");
        MyTimer.PrintAndReset("count");
        var where = guidPool.Where(x=> x == Guid.Empty).ToList();
        MyGcService.PrintMemorySize("where");
        MyTimer.PrintAndReset("where");
    }


    [HttpGet]
    public async IAsyncEnumerable<Guid> GetIAsyncEnumerableResult()
    {
        var guidProvider = new GuidProvider();
        var guidPool = guidProvider.GetIEnumerable();
        foreach (var guid in guidPool)
        {
            await Task.Delay(0); //for async
            yield return guid;
        }
    }

    [HttpGet]
    public IEnumerable<Guid> GetIEnumerableResult()
    {
        var guidProvider = new GuidProvider();
        return guidProvider.GetIEnumerable();
    }
}

public class GuidProvider
{
    public IEnumerable<Guid> GetIEnumerable()
    {
        return Enumerable.Range(0, 1000000).Select(i => Guid.NewGuid());

    }
    
    public List<Guid> GetList()
    {
        return Enumerable.Range(0, 1000000).Select(i => Guid.NewGuid()).ToList();

    }
    
    public IEnumerable<Guid> GetIEnumerableFromList()
    {
        return Enumerable.Range(0, 1000000).Select(i => Guid.NewGuid()).ToList();
    }
}