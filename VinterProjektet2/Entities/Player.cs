using System.Numerics;
using System.Timers;
using Raylib_cs;

public unsafe class Player : ISprite
{
    public bool ranged = false;
    private bool moving = false;
    private int baseSpeed = 2;
    private float health = 100;

    public float Coins { get; private set; } = 0;

    public Inventory Inv { get; set; } = new();
    // Instead of giving damage, give equipped item and that can store the ranged variable also
    // Better than trying to get damage in the player class when it is a weapons class att...

    private Texture2D spriteSheet;
    private Vector2 spriteSize = new(20, 26);
    private Vector2 windowSize;

    private readonly int delayMilliSeconds = 500;
    System.Timers.Timer spriteTimer;

    public int AttackTimerMS
    {
        get
        {
            return AttackTimerMS;
        }
        set
        {
            cooldownTimer.Interval = value;
        }
    }
    public bool canAttack { get; private set; } = false;
    System.Timers.Timer cooldownTimer = new()
    {
        AutoReset = false,
        Enabled = true,
    };

    public Vector2 position
    {
        get
        {
            return new(player.playerRectangle.x, player.playerRectangle.y);
        }
        set
        {
            player.playerRectangle.x += value.X;
            player.playerRectangle.y += value.Y;
        }
    }

    private (Direction dir, SpriteNumber sprite, Rectangle playerRectangle) player;

    private enum Direction //What sprite to use (coordinates) - Height
    {
        North = 80,
        East = 26,
        South = 0,
        West = 53
    }

    private enum SpriteNumber //What sprite to use (coordinates) - Width
    {
        One = 0,
        Two = 20,
        Three = 40,
        Four = 60
    }

    private void ResetCooldown(Object source, ElapsedEventArgs e)
    {
        canAttack = true;
    }

    public Player()
    {
        spriteTimer = new(delayMilliSeconds);
        spriteTimer.Elapsed += UpdateSprite;
        spriteTimer.AutoReset = true;
        spriteTimer.Enabled = true;

        cooldownTimer.Elapsed += ResetCooldown;

        windowSize = new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        player = new()
        {
            dir = Direction.North,
            sprite = SpriteNumber.One,
            playerRectangle = new(windowSize.X / 2, windowSize.Y / 2, 20, 26)
        };

        Raylib.ImageResize(ref ImageLib.PlayerSprite, 80, 104);

        spriteSheet = Raylib.LoadTextureFromImage(ImageLib.PlayerSprite);

        Raylib.UnloadImage(ImageLib.PlayerSprite);
    }

    public void RecieveDamage(float amount)
    {
        health -= amount;
        CheckDead();
    }

    public void Update()
    {
        KeyBinds();
        Render();
    }

    public void AddCurrency(float amount)
    {
        Coins += amount;
    }

    private void CheckDead()
    {
        if (health <= 0)
        {
            Console.WriteLine("You are dead!");
        }
        else
        {
            Console.WriteLine("New hp is {0}", health);
        }
    }

    private void UpdateSprite(Object source, ElapsedEventArgs e)
    {
        if (!moving) return;

        Console.WriteLine(player.sprite);
        switch (player.sprite)
        {
            case SpriteNumber.One:
                player.sprite = SpriteNumber.Two;
                break;
            case SpriteNumber.Two:
                player.sprite = SpriteNumber.Three;
                break;
            case SpriteNumber.Three:
                player.sprite = SpriteNumber.Four;
                break;
            case SpriteNumber.Four:
                player.sprite = SpriteNumber.One;
                break;
        }
    }

    private void Render()
    {
        int x = (int)player.sprite;
        int y = (int)player.dir;
        Raylib.DrawTexturePro(spriteSheet, new(x, y, (int)spriteSize.X, (int)spriteSize.Y), player.playerRectangle, new(0, 0), 0, Color.WHITE);
    }

    private void KeyBinds()
    {
        KeyboardKey key = (KeyboardKey)Raylib.GetKeyPressed();

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT_SHIFT))
        {
            spriteTimer.Interval = delayMilliSeconds / 2;
        }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT_SHIFT))
        {
            spriteTimer.Interval = delayMilliSeconds - 150;
        }

        int speed = baseSpeed;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT))
        {
            speed *= 2;
        }

        moving = false;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            player.dir = Direction.East;
            Move(speed, 0);
            moving = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            player.dir = Direction.West;
            Move(-speed, 0);
            moving = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            player.dir = Direction.South;
            Move(0, speed);
            moving = true;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            player.dir = Direction.North;
            Move(0, -speed);
            moving = true;
        }

        if (key == KeyboardKey.KEY_I)
        {
            Inv.Open = !Inv.Open;
        }
    }

    public void Attack(ref List<Enemy> enemyList)
    {
        // if (ranged) throw new NotImplementedException();
        // else {}

        foreach (Enemy e in enemyList)
        {
            if (Vector2.Distance(e.Position, position) < Inv.equipped.Range)
            {
                e.RecieveDamage(Inv.equipped.Damage);
            }
        }
    }

    private void Move(int x, int y)
    {
        position = new(x, y);
    }
}
