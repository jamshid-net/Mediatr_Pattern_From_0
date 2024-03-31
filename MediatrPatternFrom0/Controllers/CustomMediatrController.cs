using MediatrPatternFrom0.RepositoryPattern;
using MediatrPatternFrom0.Services;
using Microsoft.AspNetCore.Mvc;
using Pattern.Interfaces;
using System.Diagnostics;

namespace MediatrPatternFrom0.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class CustomMediatrController(IMediatr mediatr, IRepository repository) : ControllerBase
{
    [HttpPost]
    public async Task<string> Hello(HelloCommand command)
    {
        return await mediatr.SendAsync(command);
    }



    [HttpPost] 
    public async Task<string> TestPerformance()
    {
        Stopwatch stpForMediator =new Stopwatch();
        stpForMediator.Start();
        var resultOfMediator = await mediatr.SendAsync(new HelloCommand2());
        stpForMediator.Stop();
        var mTicks = stpForMediator.ElapsedTicks;

        Stopwatch stpForRepository = new Stopwatch();   
        stpForRepository.Start();
        var resultOfRepository = await repository.Handle();
        stpForRepository.Stop();
        var rTicks = stpForRepository.ElapsedTicks;


        return $"MEDIATOR EXECUTED IN {mTicks} ticks\n REPOSITORY EXECUTED IN {rTicks} ticks";
    }
}
