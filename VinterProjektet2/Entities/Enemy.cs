using System.Numerics;
using System.Timers;
using Raylib_cs;

public abstract class Enemy
{
    public Vector2 Position { get; protected set; }
    protected Vector2 pathDestination;

    //Default values below (will get serialized from file otherwise)
    protected float Speed { get; set; } = 2;
    protected float Hitpoints { get; set; } = 100;
    protected float Damage { get; set; } = 10;
    protected float Shield { get; set; } = 0;

    static protected Random RandomGenerator = new();

    private readonly int delayMilliSeconds = 1000;
    System.Timers.Timer timer;
    private bool canAttack = false;

    public bool IsDead { get; private set; }

    private SpawnLocation spawnLocation;
    enum SpawnLocation
    {
        North,
        East,
        South,
        West
    }

    public void RecieveDamage(float amount)
    {
        Hitpoints -= amount;
        CheckDeath();

        Console.WriteLine("New enemy hitpoints = {0}", Hitpoints);
    }

    public void CheckDeath()
    {
        if (Hitpoints <= 0)
        {
            IsDead = true;
        }
    }

    private void AllowAttacking(Object source, ElapsedEventArgs e)
    {
        canAttack = true;
    }

    public virtual void Unload() { }

    public virtual void UpdateEnemy(Vector2 playerPos) { }

    public virtual void Spawn()
    {
        timer = new(delayMilliSeconds);
        timer.Elapsed += AllowAttacking;
        timer.AutoReset = false;
        timer.Enabled = true;

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

    unsafe public void Attack(ref Player player)
    {
        if (canAttack && (Vector2.Distance(player.position, Position) < 5))
        {
            player.RecieveDamage(Damage);
            canAttack = false;
            timer.Start();
        }
    }

    protected virtual void DrawEnemyRectangle()
    {
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 10, 10, Color.BLUE);
    }
}