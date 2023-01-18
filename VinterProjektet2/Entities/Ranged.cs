using System.Numerics;

public class Ranged : Weapon
{
    public bool Seeking { get; set; }

    public Ranged(int cd, bool hasProjectiles) : base(cd)
    {
        if (hasProjectiles) projectiles = new();
    }

    public override void FireProjectile(Player.Direction dir, Vector2 playerPositon)
    {
        int speedFactor = 3;
        int rotation = 0;
        Vector2 speed = new(0, 0);
        Vector2 offset = new(0, 0);

        switch (dir)
        {
            case Player.Direction.North:
                speed = new(0, -1);
                rotation = 0;
                offset = new(15, 0);
                break;
            case Player.Direction.East:
                speed = new(1, 0);
                rotation = 90;
                offset = new(0, 15);
                break;
            case Player.Direction.South:
                speed = new(0, 1);
                rotation = 180;
                offset = new(-15, 0);
                break;
            case Player.Direction.West:
                speed = new(-1, 0);
                rotation = 270;
                offset = new(0, -15);
                break;
        }

        projectiles.Add(new()
        {
            Speed = speed * speedFactor,
            Damage = this.Damage,
            Position = playerPositon,
            Rotation = rotation,
            Offset = offset
        });
    }
}
