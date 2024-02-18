using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Notion_to_Html.Models;
using System.Collections.Generic;
using System.Linq;

namespace Notion_to_Html
{
    public static class ShoppingCartApi
    {
        static List<ShoppingCartItem> shoppingCartItems = new();


        [FunctionName("GetShoppingCartItems")]
        public static async Task<IActionResult> GetShoppingCartItems(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "shoppingcartitem")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting All shopping cart Items");
            return new OkObjectResult(shoppingCartItems);

        }

        [FunctionName("GetShoppingCartItemById")]
        public static async Task<IActionResult> GetShoppingCartItemById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "shoppingcartitem/{id}")] 
            HttpRequest req, ILogger log, string id)
        {
            log.LogInformation($"Getting Shopping cart Item with ID: {id}");
            var shoppingCartItem = shoppingCartItems.FirstOrDefault(q => q.Id == id);
            if(shoppingCartItem == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(shoppingCartItem);

        }

        [FunctionName("CreateShoppingCartItems")]
        public static async Task<IActionResult> CreateShoppingCartItems(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "shoppingcartitem")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestData =  await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<CreateShoppingCartItem> (requestData);

            var item = new ShoppingCartItem
            {
                ItemName = data.ItemName
            };

            shoppingCartItems.Add(item);

            return new OkObjectResult(item);
        }
        
        
        [FunctionName("PutShoppingCartItems")]
        public static async Task<IActionResult> PutShoppingCartItem(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "shoppingcartitem/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation($"Updating Shopping cart Item with ID: {id}");
            var shoppingCartItem = shoppingCartItems.FirstOrDefault(q => q.Id == id);
            if (shoppingCartItem == null)
            {
                return new NotFoundResult();
            }

            string requestData = await new StreamReader (req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<UpdateShoppingCart>(requestData);

            shoppingCartItem.Collected = data.Collected;
            return new OkObjectResult(shoppingCartItem);
        }
        
        
        [FunctionName("DeleteShoppingCartItems")]
        public static async Task<IActionResult> DeleteShoppingCartItem(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "shoppingcartitem/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            log.LogInformation($"Deleting Shopping cart Item with ID: {id}");
            var shoppingCartItem = shoppingCartItems.FirstOrDefault(q => q.Id == id);
            if (shoppingCartItem == null)
            {
                return new NotFoundResult();
            }

            shoppingCartItems.Remove(shoppingCartItem);
            return new OkResult();
        }
    }
}
