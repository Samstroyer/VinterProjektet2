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

    private Random generator = new();

    public List<Enemy> GetEnemyList(int roundNumber, Difficulty difficulty)
    {
        //Create the return variable
        //There will always be a slime in each round
        List<Enemy> ret = new();
        ret.Add(new SlimeEnemy());

        switch (difficulty)
        {
            case Difficulty.Easy:
                EasyGenerator(ref ret, roundNumber);
                break;

            case Difficulty.Medium:
                MediumGenerator(ref ret, roundNumber);
                break;

            case Difficulty.Hard:
                HardGenerator(ref ret, roundNumber);
                break;

            default:
                Console.WriteLine("This message should be impossible to get...");
                Console.WriteLine("As punishment you will get hard difficulty enemies");
                HardGenerator(ref ret, roundNumber);
                break;
        }

        return ret;
    }

    private void EasyGenerator(ref List<Enemy> enemyList, int rolls)
    {
        for (int i = 0; i < rolls; i++)
        {
            float result = generator.NextSingle();

            enemyList.Add(new SkeletonEnemy());

            if (result < 0.3)
            {
                SlimeEnemy temp = new()
                {
                    BaseDamage = Easy.BaseEnemyDamage,
                    BaseGoldDrop = Easy.GoldPerKill,
                    BaseHitpoints = Easy.BaseEnemyHealth,
                    BaseSpeed = Easy.BaseEnemySpeed
                };

                enemyList.Add(temp);
            }
        }
    }

    private void MediumGenerator(ref List<Enemy> enemyList, int rolls)
    {
        for (int i = 0; i < rolls; i++)
        {

        }
    }

    private void HardGenerator(ref List<Enemy> enemyList, int rolls)
    {
        for (int i = 0; i < rolls; i++)
        {

        }
    }
}
