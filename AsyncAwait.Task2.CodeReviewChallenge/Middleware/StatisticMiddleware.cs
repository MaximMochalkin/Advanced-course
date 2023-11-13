using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncAwait.Task2.CodeReviewChallenge.Headers;
using CloudServices.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AsyncAwait.Task2.CodeReviewChallenge.Middleware;

public class StatisticMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IStatisticService _statisticService;

    public StatisticMiddleware(RequestDelegate next, IStatisticService statisticService)
    {
        _next = next;
        _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;

        var staticRegTask = _statisticService.RegisterVisitAsync(path);
        
        Console.WriteLine(staticRegTask.Status); // just for debugging purposes
        await Task.WhenAny(staticRegTask).ConfigureAwait(false);

        if (staticRegTask.IsCompletedSuccessfully)
        {
            await UpdateHeaders(context, path).ConfigureAwait(false);
        }
        
        await _next(context).ConfigureAwait(false);
    }

    private async Task UpdateHeaders(HttpContext context, string path)
    {
        var visitsCount
            = await _statisticService.GetVisitsCountAsync(path).ConfigureAwait(false);

        context.Response.Headers.Add(
            CustomHttpHeaders.TotalPageVisits,
            visitsCount.ToString());
    }
}
