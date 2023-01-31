using Raylib_cs;
using System;

//W.I.P

public class Wall
{
    public bool Exists { get; set; } = false;
    public Rectangle wallBorder { get; set; }

    //Thaught to be the HP of the walls
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
        //Sets the wall size
        wallBorder = new(100, 100, 700, 700);
    }

    public void Render()
    {
        //Render the wall
        Raylib.DrawRectangleLinesEx(wallBorder, 10, Color.RED);
    }
}
