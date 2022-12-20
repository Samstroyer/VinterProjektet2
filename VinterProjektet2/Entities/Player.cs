using System.Numerics;
using System.Timers;
using Raylib_cs;

public class Player
{
    private bool Ranged = false;
    private bool moving = false;

    private Inventory inventory = new();

    private int baseSpeed = 2;

    private Texture2D spriteSheet;
    private Vector2 spriteSize = new(20, 26);
    private Vector2 windowSize;

    private readonly int delay = 500;
    System.Timers.Timer timer;

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

    public Player()
    {
        timer = new(delay);
        timer.Elapsed += UpdateSprite;
        timer.AutoReset = true;
        timer.Enabled = true;

        windowSize = new(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        player = new()
        {
            dir = Direction.North,
            sprite = SpriteNumber.One,
            playerRectangle = new(windowSize.X / 2, windowSize.Y / 2, 20, 26)
        };

        Raylib.ImageResize(ref ImageLib.PlayerSpriteImage, 80, 104);

        spriteSheet = Raylib.LoadTextureFromImage(ImageLib.PlayerSpriteImage);

        Raylib.UnloadImage(ImageLib.PlayerSpriteImage);
    }

    public void Update()
    {
        KeyBinds();
        Render();
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
            timer.Interval = delay / 2;
        }
        if (Raylib.IsKeyReleased(KeyboardKey.KEY_LEFT_SHIFT))
        {
            timer.Interval = delay - 150;
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


        // For inventory opening or other things
        // switch (key)
        // {
        //     default:
        //         break;
        // }
    }

    private void Move(int x, int y)
    {
        position = new(x, y);
    }
}
