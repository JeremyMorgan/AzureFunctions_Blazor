using System;
using Newtonsoft.Json;

namespace FunctionApp1.Models
{
    public class FoodItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "person")]
        public string Person { get; set; }
        [JsonProperty(PropertyName = "vegetarian")]
        public bool Vegetarian { get; set; }
        [JsonProperty(PropertyName = "glutenfree")]
        public bool Glutenfree { get; set; }
    }
}