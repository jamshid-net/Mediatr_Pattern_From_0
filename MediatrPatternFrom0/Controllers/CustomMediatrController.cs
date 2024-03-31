using MediatrPatternFrom0.Services;
using Microsoft.AspNetCore.Mvc;
using Pattern.Interfaces;

namespace MediatrPatternFrom0.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CustomMediatrController(IMediatr mediatr) : ControllerBase
{
    [HttpPost]
    public async Task<string> Hello(HelloCommand command)
    {
        return await mediatr.SendAsync(command);
    }

    [HttpPost]
    public async Task<string> Hello2(HelloCommand2 cmd)
    {
        return await mediatr.SendAsync(cmd);    
    }
}
