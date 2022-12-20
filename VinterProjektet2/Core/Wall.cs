using Raylib_cs;

public class Wall
{
    public enum Side
    {
        North = 0,
        East = 90,
        South = 180,
        West = 270
    }

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

    public Upgrades Upgrade { get; private set; }
    public int Hitpoints { get; private set; }
    public Side Placing { get; private set; }

    public Wall(Side direction)
    {
        Placing = direction;
    }

    public void Draw()
    {
        switch (Placing)
        {
            case Side.North:
                Raylib.DrawRectangle(100, 100, 700, 20, Color.RED);
                break;
            case Side.East:
                Raylib.DrawRectangle(780, 100, 20, 700, Color.RED);
                break;
            case Side.South:
                Raylib.DrawRectangle(100, 800, 700, 20, Color.RED);
                break;
            case Side.West:
                Raylib.DrawRectangle(100, 100, 20, 700, Color.RED);
                break;
        }
    }
}
