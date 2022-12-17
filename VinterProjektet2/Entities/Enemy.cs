using System.Numerics;
using Raylib_cs;

public abstract class Enemy
{
    protected Vector2 pos { get; set; }
    protected float speed { get; set; }
    protected float hitpoints { get; set; }
    protected float damage { get; set; }
    protected float shield { get; set; }

    static protected Random randomGenerator = new();

    enum SpawnLocation
    {
        North,
        East,
        South,
        West
    }

    public virtual void Spawn()
    {
        float ran = randomGenerator.NextSingle();

        if (ran <= 0.25f)
        {
            SetSpawnPosition(SpawnLocation.North);
        }
        else if (ran <= 0.5f)
        {
            SetSpawnPosition(SpawnLocation.East);
        }
        else if (ran <= 0.75f)
        {
            SetSpawnPosition(SpawnLocation.South);
        }
        else
        {
            SetSpawnPosition(SpawnLocation.West);
        }
    }

    private void SetSpawnPosition(SpawnLocation loc)
    {
        int x = randomGenerator.Next(Raylib.GetScreenWidth());
        int y = randomGenerator.Next(Raylib.GetScreenHeight());

        int width = Raylib.GetScreenWidth();
        int height = Raylib.GetScreenHeight();

        switch (loc)
        {
            case SpawnLocation.North:
                pos = new(x, -50);
                break;
            case SpawnLocation.East:
                pos = new(width + 50, y);
                break;
            case SpawnLocation.South:
                pos = new(x, height + 50);
                break;
            case SpawnLocation.West:
                pos = new(-50, y);
                break;
            default:
                Console.WriteLine("Position wrong! {0} not recognised!", loc);
                Console.WriteLine("Setting Enemy spawn position to North");
                pos = new(x, -50);
                break;
        }
    }
}