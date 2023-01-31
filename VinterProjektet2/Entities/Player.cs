using System.Numerics;
using System.Timers;
using Raylib_cs;

public unsafe class Player : ISprite
{
    public bool ranged = false;
    private bool moving = false;
    private int baseSpeed = 2;
    private float health = 100;

    public float Coins { get; set; } = 0;

    //Inventory keeps track of weapon equipped, damage it has etc
    public Inventory Inv { get; set; } = new();

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

    public enum Direction //What sprite to use (coordinates) - Height
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

        //Fix texture for player
        Raylib.ImageResize(ref ImageLib.PlayerSprite, 80, 104);
        spriteSheet = Raylib.LoadTextureFromImage(ImageLib.PlayerSprite);
        Raylib.UnloadImage(ImageLib.PlayerSprite);
    }

    //Recieve damage function
    public void RecieveDamage(float amount)
    {
        health -= amount;
        CheckDead();
    }

    //Update the player
    public void Update()
    {
        KeyBinds();
        Render();
    }

    //When killing an enemy this is called to add currency to bank :)
    public void AddCurrency(float amount)
    {
        Coins += amount;
    }

    //Check if player is alive
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

    //Update the sprite
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

    //Render the player
    private void Render()
    {
        int x = (int)player.sprite;
        int y = (int)player.dir;

        Rectangle offsetRect = new(player.playerRectangle.x - 10, player.playerRectangle.y - 15, player.playerRectangle.width, player.playerRectangle.height);
        Raylib.DrawTexturePro(spriteSheet, new(x, y, (int)spriteSize.X, (int)spriteSize.Y), offsetRect, new(0, 0), 0, Color.WHITE);
    }

    //Check all the projectiles shot
    public void CheckProjectiles(ref List<Enemy> enemyList)
    {
        if (Inv.equipped.projectiles != null || Inv.equipped.projectiles?.Count > 0)
            for (int i = Inv.equipped.projectiles.Count - 1; i >= 0; i--)
            {
                Inv.equipped.projectiles[i].Update();
                foreach (Enemy e in enemyList)
                {
                    if (i >= 0 && i < Inv.equipped.projectiles.Count)
                        if (Vector2.Distance(Inv.equipped.projectiles[i].Position, e.Position) < 15)
                        {
                            e.RecieveDamage(Inv.equipped.projectiles[i].Damage);
                            Inv.equipped.projectiles.RemoveAt(i);
                        }
                }
                if (i >= 0 && i < Inv.equipped.projectiles.Count)
                    if (!Raylib.CheckCollisionRecs(new(-50, -50, 1000, 1000), Inv.equipped.projectiles[i].Hitbox))
                        Inv.equipped.projectiles.RemoveAt(i);
            }
    }

    //Keybinds for the player
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

    //Attack code
    public void Attack(ref List<Enemy> enemyList)
    {
        //If it is on cooldown, return
        if (!Inv.equipped.ready) return;

        //If ranged it will spawn projectiles for bow and for AK crash
        if (ranged)
        {
            if (Inv.equipped == WeaponLib.Bow)
            {
                Inv.equipped.FireProjectile(player.dir, position);
            }
            else if (Inv.equipped == WeaponLib.AssaultRifle)
            {
                throw new Exception("This is a medieval game, tryhard!");
            }
        }

        //If not ranged it will attack as melee
        if (!ranged)
        {
            // Works only with melee weapons
            foreach (Enemy e in enemyList)
            {
                if (Vector2.Distance(e.Position, position) < Inv.equipped.Range)
                {
                    e.RecieveDamage(Inv.equipped.Damage);

                    Melee temp = (Melee)Inv.equipped;
                    if (!temp.Piercing) return;
                }
            }
        }
    }

    //Moving code
    private void Move(int x, int y)
    {
        position = new(x, y);
    }
}
