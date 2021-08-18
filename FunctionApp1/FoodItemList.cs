using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FunctionApp1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class FoodItemList
    {
        [FunctionName("GetFoodItems")]
        public static async Task<IActionResult> GetFoodItems(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route="fooditem")] HttpRequest req, ILogger log)
        {
            log.LogInformation("GetFoodItems called with GET");

            return new OkObjectResult(FoodItemStore.Fooditems);
        }

        [FunctionName("GetFoodItemsById")]
        public static async Task<IActionResult> GetFoodItemsById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "fooditem/{id}")] HttpRequest req, string id, ILogger log)
        {
            log.LogInformation("GetFoodItemsById called with GET");

            var foodItem = FoodItemStore.Fooditems.FirstOrDefault(f => f.Id == id);

            if (foodItem == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(foodItem);
        }

        [FunctionName("AddFoodItem")]
        public static async Task<IActionResult> AddFoodItem(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "fooditem")] HttpRequest req, ILogger log)
        {
            log.LogInformation("AddFoodItem called with POST");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var input = JsonConvert.DeserializeObject<FoodItem>(requestBody);

            var fooditem = new FoodItem()
            {
                Name = input.Name,
                Person = input.Person,
                Vegetarian = input.Vegetarian,
                Glutenfree = input.Glutenfree
            };

            FoodItemStore.Fooditems.Add(fooditem);
            
            return new OkObjectResult(fooditem);
        }

        [FunctionName("DeleteFoodItem")]
        public static async Task<IActionResult> DeleteFoodItem(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "fooditem/{id}")] HttpRequest req, string id, ILogger log)
        {
            log.LogInformation("DeleteFoodItem called with DELETE");

            var foodItem = FoodItemStore.Fooditems.FirstOrDefault(f => f.Id == id);

            if (foodItem == null)
            {
                return new NotFoundResult();
            }

            FoodItemStore.Fooditems.Remove(foodItem);

            return new OkResult();
        }
    }
}