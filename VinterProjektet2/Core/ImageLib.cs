using Raylib_cs;

//Contains most images and textures in the game

public static class ImageLib
{
    public static Image TempImgCastleFloor = Raylib.LoadImage("Textures/CastleFloor.png");
    public static Image PlayerSprite = Raylib.LoadImage("Textures/CharacterSprite.png");
    public static Image TempImgGrass = Raylib.LoadImage("Textures/Grass.png");

    //The ones that get used a lot will be loaded directly in "final form" 
    public static Texture2D BossSprite = Raylib.LoadTexture("Textures/BossSprite.png");
    public static Texture2D SkeletonSprite = Raylib.LoadTexture("Textures/SkeletonSprite.png");
    public static Texture2D SlimeSprite = Raylib.LoadTexture("Textures/SlimeSprite.png");
    public static Texture2D InventoryTexture = Raylib.LoadTexture("Textures/Inventory.png");

    public static Texture2D ArrowTexture = Raylib.LoadTexture("Textures/Arrow.png");
    public static Texture2D AssaultRifleTexture = Raylib.LoadTexture("Textures/AssaultRifle.png");
    public static Texture2D BowTexture = Raylib.LoadTexture("Textures/Bow.png");
    public static Texture2D CleaverTexture = Raylib.LoadTexture("Textures/Cleaver.png");
    public static Texture2D DaggerTextuer = Raylib.LoadTexture("Textures/Dagger.png");
    public static Texture2D FistTexture = Raylib.LoadTexture("Textures/Fist.png");
}
