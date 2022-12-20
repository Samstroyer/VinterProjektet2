using System.Text.Json.Serialization;

public class Medium
{
    [JsonPropertyName("BaseEnemyHealth")]
    public int BaseEnemyHealth { get; set; }

    [JsonPropertyName("BaseEnemySpeed")]
    public int BaseEnemySpeed { get; set; }

    [JsonPropertyName("BaseEnemyDamage")]
    public int BaseEnemyDamage { get; set; }

    [JsonPropertyName("AverageGoldPerKill")]
    public int AverageGoldPerKill { get; set; }

    [JsonPropertyName("SpawnDelay")]
    public int SpawnDelay { get; set; }
}
