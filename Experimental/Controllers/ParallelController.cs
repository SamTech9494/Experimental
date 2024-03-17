using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Experimental.Controllers;

//reference: https://dev.to/kamilbugnokrk/parallel-programming-in-c-53eg
[ApiController]
[Route("[controller]/[Action]")]
public class ParallelController : ControllerBase
{
    [HttpGet]
    public void Sync()
    {
        var timer = new Stopwatch();
        timer.Start();
        HeavyComputation("A");
        HeavyComputation("B");
        HeavyComputation("C");
        HeavyComputation("D");
        HeavyComputation("E");
        timer.Stop();
        Console.WriteLine("All: " + timer.ElapsedMilliseconds);
    }

    [HttpGet]
    public void ParallelInvoke()
    {
        var timer = new Stopwatch();
        timer.Start();
        Parallel.Invoke(
            () => HeavyComputation("A"),
            () => HeavyComputation("B"),
            () => HeavyComputation("C"),
            () => HeavyComputation("D"),
            () => HeavyComputation("E")
        );
        timer.Stop();
        Console.WriteLine("All: " + timer.ElapsedMilliseconds);
    }

    [HttpGet]
    public void PLINQ()
    {
        var ints = new List<int>();
        for (var i = 0; i < 1000000000; i++)
        {
            ints.Add(i);
        }
        MyTimer.Start();

        var list = ints.Select(x=>x *2).ToList();
        MyTimer.PrintAndReset("LINQ"); 
        
        
        var pList = ints.AsParallel().Select(x=>x *2).ToList();
        MyTimer.PrintAndReset("PLINQ"); 
    }

    private static int HeavyComputation(string name)
    {
        Console.WriteLine("Start: " + name);
        var timer = new Stopwatch();
        timer.Start();
        var result = 0;
        for (var i = 0; i < 10_000_000; i++)
        {
            var a = ((i + 1_500) / (i + 30)) * (i + 10);
            result += (a % 10) - 120;
        }

        timer.Stop();
        Console.WriteLine("End: " + name + ' ' + timer.ElapsedMilliseconds);
        return result;
    }
}