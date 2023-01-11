using System.Numerics;
using System.Timers;
using Raylib_cs;

public class SlimeEnemy : Enemy, ISprite
{
    private Texture2D spriteSheet;
    private Vector2 spriteSize = new(52, 52);
    private enum Direction //What sprite to use (coordinates) - Height
    {
        North = 164,
        East = 112,
        South = 60,
        West = 8
    }
    private enum SpriteNumber //What sprite to use (coordinates) - Width
    {
        One = 8,
        Two = 60,
        Three = 112,
        Four = 164
    }
    private (Direction dir, SpriteNumber sprite, Rectangle enemyRectangle) enemy = new()
    {
        dir = Direction.South,
        sprite = SpriteNumber.One
    };

    System.Timers.Timer timer = new(500);

    public SlimeEnemy()
    {
        timer.Elapsed += UpdateSprite;
        timer.AutoReset = true;
        timer.Enabled = true;

        enemy.enemyRectangle = new(Position.X, Position.Y, spriteSize.X, spriteSize.Y);

        if (ImageLib.SlimeSprite.width != 280 || ImageLib.SlimeSprite.height != 216)
        {
            Raylib.ImageResize(ref ImageLib.SlimeSprite, 280, 216);
        }

        spriteSheet = Raylib.LoadTextureFromImage(ImageLib.SlimeSprite);

        Spawn();
    }

    public override void UpdateEnemy(Vector2 playerPos)
    {
        Advance(playerPos);
        Draw();

        //Good for debugging 
        // DrawEnemyRectangle();
    }

    public override float Loot()
    {
        return BaseGoldDrop * 1;
    }

    protected void Draw()
    {
        //base.DrawEnemyRectangle();
        Render();
    }

    private void UpdateSprite(Object source, ElapsedEventArgs e)
    {
        Console.WriteLine(enemy.sprite);
        switch (enemy.sprite)
        {
            case SpriteNumber.One:
                enemy.sprite = SpriteNumber.Two;
                break;
            case SpriteNumber.Two:
                enemy.sprite = SpriteNumber.Three;
                break;
            case SpriteNumber.Three:
                enemy.sprite = SpriteNumber.Four;
                break;
            case SpriteNumber.Four:
                enemy.sprite = SpriteNumber.One;
                break;
        }
    }

    public override void Unload()
    {
        Raylib.UnloadTexture(spriteSheet);
        timer.Close();
    }

    private void Render()
    {
        int x = (int)enemy.sprite;
        int y = (int)enemy.dir;

        enemy.enemyRectangle.x = Position.X - (spriteSize.X / 2);
        enemy.enemyRectangle.y = Position.Y - (spriteSize.Y / 2);

        Raylib.DrawTexturePro(spriteSheet, new(x, y, (int)spriteSize.X, (int)spriteSize.Y), enemy.enemyRectangle, new(0, 0), 0, Color.WHITE);
    }

    protected void Advance(Vector2 playerPos)
    {
        Vector2 temp = Vector2.Subtract(playerPos, Position);

        temp = Vector2.Normalize(temp);
        ChooseSprite(temp);
        temp *= BaseSpeed;

        Position += temp;
    }

    private void ChooseSprite(Vector2 temp)
    {
        //if the movement is *DOMINATED* in a direction the sprite for that direction will be shown
        if (temp.X > 0.5)
        {
            enemy.dir = Direction.East;
        }
        else if (temp.X < -0.5)
        {
            enemy.dir = Direction.West;
        }

        if (temp.Y >= 0.5)
        {
            enemy.dir = Direction.South;
        }
        else if (temp.Y <= -0.5)
        {
            enemy.dir = Direction.North;
        }
    }
}