using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureDurableFunctions
{
    public static partial class Function1
    {

        [FunctionName(nameof(CleanData))]
        public static string CleanData([ActivityTrigger] string value, ILogger log)
        {
            log.LogInformation($"CleanData {value}.");
            return $"CleanData";
        }

        [FunctionName(nameof(ApplyRules))]
        public static string ApplyRules([ActivityTrigger] string value, ILogger log)
        {
            log.LogInformation($"ApplyRules {value}.");
            return $"ApplyRules";
        }

        [FunctionName(nameof(ExtractData))]
        public static string ExtractData([ActivityTrigger]  string value, ILogger log)
        {
            log.LogInformation($"ExtractData {value}.");
            return $"ExtractData";
        }

    }
}