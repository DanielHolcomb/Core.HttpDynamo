using System.Text.Json.Serialization;

namespace SampleHttpDynamoApp
{
    public class PostItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
