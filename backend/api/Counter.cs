using Newtonsoft.Json;

namespace Company.Function
{
    public class Counterdata 
    {
        [JsonProperty(PropertyName ="id")]

        public string id {get; set;}

         [JsonProperty(PropertyName ="count")]

        public int  count {get; set;}
    }

}