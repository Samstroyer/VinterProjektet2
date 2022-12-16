using Raylib_cs;

public class Castle
{
    private int width;
    private int height;
    private Texture2D floor;
    private Texture2D grass;

    private enum CastleUpgrades
    {
        None,
        StageOne,
        StageTwo,
        StageThree,
        StageFour,
        StageFive,
        StageSix
    }

    public Castle()
    {
        width = Raylib.GetScreenWidth();
        height = Raylib.GetScreenHeight();
        Image tempImgCastleFloor = Raylib.LoadImage("Textures/CastleFloor.png");
        Image tempImgGrass = Raylib.LoadImage("Textures/Grass.png");

        Raylib.ImageResize(ref tempImgCastleFloor, width - 200, height - 200);
        Raylib.ImageResize(ref tempImgGrass, width, height);

        floor = Raylib.LoadTextureFromImage(tempImgCastleFloor);
        grass = Raylib.LoadTextureFromImage(tempImgGrass);

        Raylib.UnloadImage(tempImgCastleFloor);
        Raylib.UnloadImage(tempImgGrass);
    }

    public void Render()
    {
        Raylib.DrawTexture(grass, 0, 0, Color.WHITE);
        Raylib.DrawTexture(floor, 100, 100, Color.WHITE);
    }

    // public void RenderCastle()
}
