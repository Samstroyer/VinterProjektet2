using Raylib_cs;

public class Castle
{
    private int width;
    private int height;
    private Texture2D floor;
    private Texture2D grass;

    public Dictionary<string, Wall> Walls { get; private set; }

    public Castle()
    {
        width = Raylib.GetScreenWidth();
        height = Raylib.GetScreenHeight();

        Walls = new();
        Walls.Add("North", new Wall(Wall.Side.North));
        Walls.Add("East", new Wall(Wall.Side.East));
        Walls.Add("South", new Wall(Wall.Side.South));
        Walls.Add("West", new Wall(Wall.Side.West));

        Raylib.ImageResize(ref ImageLib.TempImgCastleFloor, width - 200, height - 200);
        Raylib.ImageResize(ref ImageLib.TempImgGrass, width, height);

        floor = Raylib.LoadTextureFromImage(ImageLib.TempImgCastleFloor);
        grass = Raylib.LoadTextureFromImage(ImageLib.TempImgGrass);

        Raylib.UnloadImage(ImageLib.TempImgCastleFloor);
        Raylib.UnloadImage(ImageLib.TempImgGrass);
    }

    public void Run()
    {
        RenderCastle();
        RenderWalls();
    }

    private void RenderCastle()
    {
        Raylib.DrawTexture(grass, 0, 0, Color.WHITE);
        Raylib.DrawTexture(floor, 100, 100, Color.WHITE);

    }

    private void RenderWalls()
    {
        foreach (var wall in Walls)
        {
            wall.Value.Draw();
        }

    }
}
