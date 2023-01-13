using System.Numerics;
using System.Timers;
using Raylib_cs;

public class SkeletonEnemy : Enemy, ISprite
{
    private static Texture2D spriteSheet = ImageLib.SkeletonSprite;
    private Vector2 spriteSize = new(65, 65);

    private enum Direction //What sprite to use (coordinates) - Height
    {
        North = 0,
        East = 195,
        South = 130,
        West = 65
    }

    //The sprite sheet is made of cubes, so I can use this * 65 to get the dimensions
    private short spriteNumber = 1;

    private (Direction dir, Rectangle enemyRectangle) enemy = new()
    {
        dir = Direction.South,
    };

    System.Timers.Timer timer = new(500);

    public SkeletonEnemy()
    {
        timer.Elapsed += UpdateSprite;
        timer.AutoReset = true;
        timer.Enabled = true;

        enemy.enemyRectangle = new(Position.X, Position.Y, spriteSize.X, spriteSize.Y);

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
        return BaseGoldDrop * 1.5f;
    }

    protected void Draw()
    {
        //base.DrawEnemyRectangle();
        Render();
    }

    private void UpdateSprite(Object source, ElapsedEventArgs e)
    {
        if (spriteNumber == 8) spriteNumber = 1;
        else spriteNumber++;
    }

    public override void Unload()
    {
        timer.Close();
    }

    private void Render()
    {
        int x = (int)spriteNumber * 65;
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
        temp *= BaseSpeed * 0.5f;

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
