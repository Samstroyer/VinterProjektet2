using System.Text.Json.Serialization;

//This file was more appropriately named Difficulties
//Later I realised this was confusing as there is an enum called Difficulty
//I decided to make this the RoundGenerator as it does it also

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class RoundGenerator
{
    [JsonPropertyName("Easy")]
    public Easy Easy { get; set; }

    [JsonPropertyName("Medium")]
    public Medium Medium { get; set; }

    [JsonPropertyName("Hard")]
    public Hard Hard { get; set; }

    public List<Enemy> GetEnemyList(int RoundNumber, Difficulty difficulty)
    {
        List<Enemy> ret = new();
        ret.Add(new LightEnemy());

        switch (difficulty)
        {
            case Difficulty.Easy:
                break;
            case Difficulty.Medium:
                break;
            case Difficulty.Hard:
                break;
            default:
                Console.WriteLine("This message should be impossible to get...");
                Console.WriteLine("As punishment you will get hard difficulty enemies this round and all coming rounds...");
                break;
        }

        return ret;
    }
}
