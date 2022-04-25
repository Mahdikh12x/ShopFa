using Newtonsoft.Json;

namespace _01_ShopFaQuery.Contracts.Report
{
    public class ChartViewModel
    {
        [JsonProperty(PropertyName ="labels")]
        public List<string>? Labels { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string? Label { get; set; }
        [JsonProperty(PropertyName ="backgroundColor")]
        public List<string>? BackgroundColors { get; set; }
        [JsonProperty(PropertyName ="borderColor")]
        public List<string>? BorderColor{ get; set; }
        [JsonProperty(PropertyName ="data")]
        public List<int> Data { get; set; }


        public void AddItem(string label)
        {
            Labels?.Add(label);
        }
    }
}
