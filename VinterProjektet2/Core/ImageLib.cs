using Raylib_cs;

public static class ImageLib
{
    public static Image TempImgCastleFloor = Raylib.LoadImage("Textures/CastleFloor.png");
    public static Image PlayerSprite = Raylib.LoadImage("Textures/CharacterSprite.png");
    public static Image TempImgGrass = Raylib.LoadImage("Textures/Grass.png");

    //The ones that get used a lot will be loaded directly as "final form" 
    //Reduces a lot of lag and stuttering
    public static Texture2D BossSprite = Raylib.LoadTexture("Textures/BossSprite.png");
    public static Texture2D SkeletonSprite = Raylib.LoadTexture("Textures/SkeletonSprite.png");
    public static Texture2D SlimeSprite = Raylib.LoadTexture("Textures/SlimeSprite.png");
    public static Texture2D InventoryTexture = Raylib.LoadTexture("Textures/Inventory.png");
}
