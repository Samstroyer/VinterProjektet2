using System.Text.Json.Serialization;

public class Easy
{
    [JsonPropertyName("BaseEnemyHealth")]
    public int BaseEnemyHealth { get; set; }

    [JsonPropertyName("BaseEnemySpeed")]
    public int BaseEnemySpeed { get; set; }

    [JsonPropertyName("BaseEnemyDamage")]
    public int BaseEnemyDamage { get; set; }

    [JsonPropertyName("GoldPerKill")]
    public int GoldPerKill { get; set; }
}
