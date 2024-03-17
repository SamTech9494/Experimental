using Microsoft.AspNetCore.Mvc;

namespace Experimental.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
public class TestAsyncIEnumerableController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<TestAsyncIEnumerableController> _logger;

    public TestAsyncIEnumerableController(ILogger<TestAsyncIEnumerableController> logger)
    {
        _logger = logger;
    }

    [HttpGet("GetWeatherForecastYieldReturn")]
    public async IAsyncEnumerable<WeatherForecast> GetWeatherForecastYieldReturn(int delay = 20, int count = 100)
    {
        GC.Collect(2, GCCollectionMode.Forced, true, false);
        var startMemSize = GC.GetTotalMemory(false);
        Func<Task<int>> getRandomTemperature = async () =>
        {
            await Task.Delay(delay);
            return Random.Shared.Next(-20, 55);
        };
        for (var i = 0; i < count; i++)
        {
            GetMemorySize(startMemSize);
            yield return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                TemperatureC = await getRandomTemperature(),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }
    }
    
    [HttpGet(Name = "GetWeatherForecastNormal")]
    public IEnumerable<WeatherForecast> GetWeatherForecastNormal(int delay = 20, int count = 100)
    {
        GC.Collect(2, GCCollectionMode.Forced, true, false);
        var startMemSize = GC.GetTotalMemory(false);
        Func<int> getRandomTemperature = () =>
        {
            Thread.Sleep(delay);
            return Random.Shared.Next(-20, 55);
        };
        var weatherForecasts = Enumerable.Range(1, count).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = getRandomTemperature(),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        });
        GetMemorySize(startMemSize); 
        var weatherForecastNormal = weatherForecasts
            .ToArray();
        GetMemorySize(startMemSize);
        return weatherForecastNormal;
    }
    
    private void GetMemorySize(long startMemSize)
    {
        Console.WriteLine("StartMemory: " + startMemSize);
        var memory = GC.GetTotalMemory(false) - startMemSize;
        GC.Collect(2, GCCollectionMode.Forced, true, false);
        // var memorySize = $"{memory / 1024 / 1024} MB";
        Console.WriteLine(memory);
    }

}