namespace Experimental.Controllers;

public static class MyGcService
{
    private static long _startMemSize = 0;

    public static void Reset()
    {
        GC.Collect(2, GCCollectionMode.Forced, true, false);
        _startMemSize = GC.GetTotalMemory(false);
    }
    public static void PrintMemorySize(string message)
    {
        var memory = GC.GetTotalMemory(false) - _startMemSize;
        GC.Collect(2, GCCollectionMode.Forced, true, false);
        Console.WriteLine(message + memory);
    }
}