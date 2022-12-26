using System;

public class LightEnemy : Enemy
{
    public override void UpdateEnemy()
    {
        Draw();
    }

    protected override void Draw()
    {
        base.Draw();
    }
}
