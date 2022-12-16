using System.Text.Json.Serialization;

public abstract class Difficulties
{
    [JsonPropertyName("Easy")]
    public Easy Easy { get; set; }

    [JsonPropertyName("Medium")]
    public Medium Medium { get; set; }

    [JsonPropertyName("Hard")]
    public Hard Hard { get; set; }
}
