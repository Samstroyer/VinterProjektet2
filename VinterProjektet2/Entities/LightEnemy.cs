using System.Numerics;
using System;

public class LightEnemy : Enemy
{
    public LightEnemy()
    {
        Spawn();
    }

    public override void UpdateEnemy(Vector2 playerPos)
    {
        base.UpdateEnemy(playerPos);
        Draw();
    }

    protected override void Draw()
    {
        base.Draw();
    }
}
