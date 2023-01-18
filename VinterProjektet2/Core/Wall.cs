using Raylib_cs;
using System;

public class Wall
{
    public bool Exists { get; set; } = false;
    public Rectangle wallBorder { get; set; }

    public enum Upgrades
    {
        Zero = 0,
        One = 100,
        Two = 200,
        Three = 300,
        Four = 500,
        Five = 1000,
        Six = 2000
    }
    private Upgrades upgradeLevel;

    public int Hitpoints
    {
        get
        {
            return (int)upgradeLevel;
        }
        set
        {
            return;
        }
    }

    public Wall()
    {
        wallBorder = new(100, 100, 700, 700);
    }

    public void Render()
    {
        Raylib.DrawRectangleLinesEx(wallBorder, 10, Color.RED);
    }
}
