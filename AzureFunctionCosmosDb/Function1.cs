using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.Common;
using Microsoft.Azure.WebJobs.Description;

namespace AzureFunctionCosmosDb
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run(
            [QueueTrigger("myqueue", Connection = "ConnectionString")] CheckList quequeCheckListItem,
            [CosmosDB(
            databaseName:"CheckList",
            collectionName:"Tasks",
            ConnectionStringSetting = "CosmosDbConnection",
            Id ="{Id}",
            PartitionKey ="{Title}")] CheckList checkList,
            ILogger log)
        {

            log.LogInformation($"C# QueueTrigger function processed Id = {quequeCheckListItem?.Id} Key = {quequeCheckListItem?.Title}");

            if (checkList == null)
            {
                log.LogInformation($"CheckList item not found");
            }
            else
                log.LogInformation($"Found CheckList item, Content={checkList.Content}");

        }
    }

    public class CheckList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        
    }
}
