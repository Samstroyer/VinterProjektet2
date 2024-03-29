using System.Numerics;
using System.Timers;
using Raylib_cs;

//Base enemy

public abstract class Enemy
{
    public Vector2 Position { get; protected set; }
    protected Vector2 pathDestination;

    public float BaseSpeed { get; set; }
    public float BaseHitpoints { get; set; }
    public float BaseDamage { get; set; }
    public float BaseShield { get; set; }
    public float BaseGoldDrop { get; set; }

    static protected Random RandomGenerator = new();

    private readonly int delayMilliSeconds = 1000;
    System.Timers.Timer timer;
    private bool canAttack = false;

    public bool IsDead { get; private set; }

    //Spawnlocations
    private SpawnLocation spawnLocation;
    enum SpawnLocation
    {
        North,
        East,
        South,
        West
    }

    //RecieveDamage function, could possibly be made an interface
    public void RecieveDamage(float amount)
    {
        BaseHitpoints -= amount;
        CheckDeath();

        Console.WriteLine("New enemy hitpoints = {0}", BaseHitpoints);
    }

    //Check if it is alive
    public void CheckDeath()
    {
        if (BaseHitpoints <= 0)
        {
            IsDead = true;
        }
    }

    //It can't attack every frame, it is limited to a timer
    private void AllowAttacking(Object source, ElapsedEventArgs e)
    {
        canAttack = true;
    }

    public virtual void Unload() { }

    public virtual void UpdateEnemy(Vector2 playerPos) { }

    //Spawn function
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

    //Code to set the spawn position
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

    //Base loot function, should probably be an interface
    public virtual float Loot()
    {
        return 0;
    }

    //Attack the player
    public void Attack(ref Player player)
    {
        if (canAttack && (Vector2.Distance(player.position, Position) < 10))
        {
            player.RecieveDamage(BaseDamage);
            canAttack = false;
            timer.Start();
        }
    }

    //For debugging
    protected virtual void DrawEnemyRectangle()
    {
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 10, 10, Color.BLUE);
    }
}