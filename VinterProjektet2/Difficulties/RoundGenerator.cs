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
        enemyList.Add(new SlimeEnemy()
        {
            BaseDamage = Easy.BaseEnemyDamage,
            BaseGoldDrop = Easy.GoldPerKill,
            BaseHitpoints = Easy.BaseEnemyHealth,
            BaseSpeed = Easy.BaseEnemySpeed
        });

        for (int i = 0; i < rolls; i++)
        {
            float result = generator.NextSingle();

            if (result < 0.2) enemyList.Add(new SkeletonEnemy()
            {
                BaseDamage = Easy.BaseEnemyDamage,
                BaseGoldDrop = Easy.GoldPerKill,
                BaseHitpoints = Easy.BaseEnemyHealth,
                BaseSpeed = Easy.BaseEnemySpeed
            });

            // 50% chance of adding a new slime
            if (result < 0.5) enemyList.Add(new SlimeEnemy()
            {
                BaseDamage = Easy.BaseEnemyDamage,
                BaseGoldDrop = Easy.GoldPerKill,
                BaseHitpoints = Easy.BaseEnemyHealth,
                BaseSpeed = Easy.BaseEnemySpeed
            });
        }
    }

    private void MediumGenerator(ref List<Enemy> enemyList, int rolls)
    {
        enemyList.Add(new SlimeEnemy()
        {
            BaseDamage = Medium.BaseEnemyDamage,
            BaseGoldDrop = Medium.GoldPerKill,
            BaseHitpoints = Medium.BaseEnemyHealth,
            BaseSpeed = Medium.BaseEnemySpeed
        });

        for (int i = 0; i < rolls; i++)
        {
            float result = generator.NextSingle();

            if (result < 0.2) enemyList.Add(new SkeletonEnemy()
            {
                BaseDamage = Medium.BaseEnemyDamage,
                BaseGoldDrop = Medium.GoldPerKill,
                BaseHitpoints = Medium.BaseEnemyHealth,
                BaseSpeed = Medium.BaseEnemySpeed
            });

            // 50% chance of adding a new slime
            if (result < 0.5) enemyList.Add(new SlimeEnemy()
            {
                BaseDamage = Medium.BaseEnemyDamage,
                BaseGoldDrop = Medium.GoldPerKill,
                BaseHitpoints = Medium.BaseEnemyHealth,
                BaseSpeed = Medium.BaseEnemySpeed
            });
        }
    }

    private void HardGenerator(ref List<Enemy> enemyList, int rolls)
    {
        enemyList.Add(new SlimeEnemy()
        {
            BaseDamage = Hard.BaseEnemyDamage,
            BaseGoldDrop = Hard.GoldPerKill,
            BaseHitpoints = Hard.BaseEnemyHealth,
            BaseSpeed = Hard.BaseEnemySpeed
        });

        for (int i = 0; i < rolls; i++)
        {
            float result = generator.NextSingle();

            if (result < 0.2) enemyList.Add(new SkeletonEnemy()
            {
                BaseDamage = Hard.BaseEnemyDamage,
                BaseGoldDrop = Hard.GoldPerKill,
                BaseHitpoints = Hard.BaseEnemyHealth,
                BaseSpeed = Hard.BaseEnemySpeed
            });

            // 50% chance of adding a new slime
            if (result < 0.5) enemyList.Add(new SlimeEnemy()
            {
                BaseDamage = Hard.BaseEnemyDamage,
                BaseGoldDrop = Hard.GoldPerKill,
                BaseHitpoints = Hard.BaseEnemyHealth,
                BaseSpeed = Hard.BaseEnemySpeed
            });
        }
    }
}
