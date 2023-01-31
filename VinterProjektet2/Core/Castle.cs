using Raylib_cs;

public class Castle
{
    private int width;
    private int height;
    private Texture2D floor;
    private Texture2D grass;

    //Create a wall - not implemented yet.
    //This is supposed to be an obstacle for the monsters so they have to path around it.
    Wall wall = new();

    public Castle()
    {
        width = Raylib.GetScreenWidth();
        height = Raylib.GetScreenHeight();

        // Line 19-27: load image, resize and unload
        Raylib.ImageResize(ref ImageLib.TempImgCastleFloor, width - 200, height - 200);
        Raylib.ImageResize(ref ImageLib.TempImgGrass, width, height);

        floor = Raylib.LoadTextureFromImage(ImageLib.TempImgCastleFloor);
        grass = Raylib.LoadTextureFromImage(ImageLib.TempImgGrass);

        Raylib.UnloadImage(ImageLib.TempImgCastleFloor);
        Raylib.UnloadImage(ImageLib.TempImgGrass);
    }

    public void Run()
    {
        //Render the castle (wood and grass images for now)
        RenderCastle();
        RenderWalls();
    }

    private void RenderCastle()
    {
        //Images mentioned above
        Raylib.DrawTexture(grass, 0, 0, Color.WHITE);
        Raylib.DrawTexture(floor, 100, 100, Color.WHITE);
    }

    private void RenderWalls()
    {
        //Render theoretical walls (yet to implement)
        wall.Render();
    }
}
