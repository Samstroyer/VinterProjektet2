using System.Numerics;
using Raylib_cs;

public class Projectile
{
    public Vector2 Position { get; set; }
    public Vector2 Speed { get; set; }
    public int Rotation { get; set; }
    public int Damage { get; set; }
    public Vector2 Offset { get; set; }

    public Projectile()
    {
        Position = new();
    }

    public void Update()
    {
        Render();
        Move();
    }

    private void Move()
    {
        Position += Speed;
    }

    private void Render()
    {
        Raylib.DrawTextureEx(ImageLib.ArrowTexture, Position - Offset, Rotation, 1, Color.WHITE);
    }
}
