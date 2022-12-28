using System.Numerics;
using Raylib_cs;

public abstract class Enemy
{
    protected Vector2 Position { get; set; }
    protected float Speed { get; set; } = 5; //Default
    protected float Hitpoints { get; set; }
    protected float Damage { get; set; }
    protected float Shield { get; set; }

    protected Vector2 pathDestination;

    static protected Random RandomGenerator = new();

    private SpawnLocation spawnLocation;

    enum SpawnLocation
    {
        North,
        East,
        South,
        West
    }

    public virtual void Spawn()
    {
        float ran = RandomGenerator.NextSingle();

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
        spawnLocation = loc;

        int x = RandomGenerator.Next(Raylib.GetScreenWidth());
        int y = RandomGenerator.Next(Raylib.GetScreenHeight());

        int width = Raylib.GetScreenWidth();
        int height = Raylib.GetScreenHeight();

        switch (spawnLocation)
        {
            case SpawnLocation.North:
                Position = new(x, -50);
                break;
            case SpawnLocation.East:
                Position = new(width + 50, y);
                break;
            case SpawnLocation.South:
                Position = new(x, height + 50);
                break;
            case SpawnLocation.West:
                Position = new(-50, y);
                break;
            default:
                Console.WriteLine("Position wrong! {0} not recognised!", loc);
                Console.WriteLine("Setting Enemy spawn position to North");
                Position = new(x, -50);
                break;
        }
    }

    protected virtual void Draw()
    {
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 10, 10, Color.BLUE);
    }

    public virtual void UpdateEnemy(Vector2 playerPos)
    {
        Advance(playerPos);
        Draw();
    }

    protected void Advance(Vector2 playerPos)
    {
        Vector2 temp = Vector2.Subtract(playerPos, Position);

        temp = Vector2.Normalize(temp);
        temp *= Speed;

        Position += temp;
    }
}