using Microsoft.AspNetCore.Mvc;

namespace Experimental.Controllers;
[ApiController]
[Route("[controller]/[Action]")]
public class TestAsyncController : ControllerBase
{
    [HttpGet]
    public void Get()
    {
        Thread.Sleep(2000);
    }
}
