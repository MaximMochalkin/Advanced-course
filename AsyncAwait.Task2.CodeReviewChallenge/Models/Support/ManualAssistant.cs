using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CloudServices.Interfaces;

namespace AsyncAwait.Task2.CodeReviewChallenge.Models.Support;

public class ManualAssistant : IAssistant
{
    private readonly ISupportService _supportService;

    public ManualAssistant(ISupportService supportService)
    {
        _supportService = supportService ?? throw new ArgumentNullException(nameof(supportService));
    }

    public async Task<string> RequestAssistanceAsync(string requestInfo)
    {
        try
        {
            var registerTask =  _supportService.RegisterSupportRequestAsync(requestInfo);
            Console.WriteLine(registerTask.Status); // this is for debugging purposes
            await Task.WhenAny(registerTask).ConfigureAwait(false);
            
            if (registerTask.IsCompletedSuccessfully)
            {
                return await _supportService.GetSupportInfoAsync(requestInfo)
                    .ConfigureAwait(false);
            }

            return await Task.FromResult("Failed to register assistance request. Please try later.");
        }
        catch (HttpRequestException ex)
        {
            return await Task.FromResult($"Failed to register assistance request. Please try later. {ex.Message}");
        }
    }
}
