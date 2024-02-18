using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Notion_to_Html
{
    public static class HtmlContentTrail
    {
        [FunctionName("HtmlContentTrail")]
        public static async Task<IActionResult> htmlcontent(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "htmlcontent")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string htmlcontent = "<html><body><h1>Hello, World!</h1></body></html>";
            return new ContentResult
            {
                Content = htmlcontent,
                ContentType = "text/html",
                StatusCode = 200
            };
        }
    }
}
