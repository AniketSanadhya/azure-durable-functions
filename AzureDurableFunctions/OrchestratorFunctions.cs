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
            log = context.CreateReplaySafeLogger(log);

            var fileLocation = context.GetInput<string>();

            var result1 = await context.CallActivityAsync<string>("CleanData", fileLocation);
            var result2 = await context.CallActivityAsync<string>("ApplyRules", result1);
            var result3 = await context.CallActivityAsync<string>("ExtractData", result2);

            return new
            {
                CleanData = result1,
                ApplyRules = result2,
                ExtractData = result3
            };
        }
    }

}