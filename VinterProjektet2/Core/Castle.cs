using Raylib_cs;

public class Castle
{
    private int width;
    private int height;
    private Texture2D floor;
    private Texture2D grass;

    Wall wall = new();

    public Castle()
    {
        width = Raylib.GetScreenWidth();
        height = Raylib.GetScreenHeight();


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
        wall.Render();
    }
}
