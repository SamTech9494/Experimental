namespace Experimental.Controllers;

public static class MyTimer
{
    private static DateTime _stTime;

    public static void Start()
    {
        _stTime = DateTime.Now;
    }

    public static void Stop(string message)
    {
        Console.WriteLine($"{(DateTime.Now - _stTime).TotalMilliseconds/1000:000.00}s {message}");

    }
    
    public static void PrintAndReset(string message)
    {
        Console.WriteLine($"{(DateTime.Now - _stTime).TotalMilliseconds/1000:000.00}s {message}");
        _stTime = DateTime.Now;
    }
}