using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;

namespace AzureDurableFunctions
{
    public static class OrchestratorFunctions
    {

        [FunctionName(nameof(ProcessFileOrchestrator))]
        public static async Task<object> ProcessFileOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            string result1 = null;
            string result2 = null;
            string result3 = null;
            try
            {
                log = context.CreateReplaySafeLogger(log);

                var fileLocation = context.GetInput<string>();

                result1 = await context.CallActivityAsync<string>("CleanData", fileLocation);
                result2 = await context.CallActivityAsync<string>("ApplyRules", result1);
                result3 = await context.CallActivityAsync<string>("ExtractData", result2);

                return new
                {
                    CleanData = result1,
                    ApplyRules = result2,
                    ExtractData = result3
                };
            }
            catch (Exception ex)
            {
                log.LogError($"Exception Occured: {ex.Message}");
                await context.CallActivityAsync<string>("Cleanup", "temp data");
                return new
                {
                    Error = "Failed to process file",
                    Message = ex.Message
                };
            }
        }
    }

}