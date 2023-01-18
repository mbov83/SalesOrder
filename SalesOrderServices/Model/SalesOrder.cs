using Newtonsoft.Json;

namespace SalesOrderServices.Model
{
    public class SalesOrder
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
      
              
        [JsonProperty(PropertyName = "customerId")]
        public string CustomerId { get; set; }
              
        [JsonProperty(PropertyName = "orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonProperty(PropertyName = "shipDate")]
        public DateTime ShipDate { get; set; }

        [JsonProperty(PropertyName = "details")]
        public Detail[] Details { get; set; }
    }

    public class Detail {

        [JsonProperty(PropertyName = "sku")]
        public string Sku { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }


    } 
}
