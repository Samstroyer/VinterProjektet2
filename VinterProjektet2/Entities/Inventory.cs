using System.Numerics;
using Raylib_cs;

//To make it "simple" I will have some static weapons
//A.K.A implemented manually

public class Inventory
{
    public bool Open { get; set; } = false;
    public List<(Weapon weapon, bool owned)> ownedWeapons = new();

    public Weapon equipped = WeaponLib.Fists;

    public Texture2D background;

    private (Rectangle rec, string prompt) RangedButton = new(new(225, 200, 200, 100), "Ranged");
    private (Rectangle rec, string prompt) MeleeButton = new(new(475, 200, 200, 100), "Melee");

    bool store = false;

    public Inventory()
    {
        Type type = typeof(WeaponLib);


        foreach (var item in type.GetFields())
        {
            ownedWeapons.Add(new((Weapon)item.GetValue(this), false));
        }
    }

    public void Display()
    {
        Raylib.DrawTexture(background, 0, 0, Color.WHITE);
        Raylib.DrawTexture(ImageLib.InventoryTexture, 0, 0, Color.WHITE);
    }

    public void Update(ref Player player)
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        int rangedSize = Raylib.MeasureText(RangedButton.prompt, 24);
        int meleeSize = Raylib.MeasureText(MeleeButton.prompt, 24);

        if (player.ranged)
        {
            Raylib.DrawRectangleRec(RangedButton.rec, Color.GREEN);
            Raylib.DrawRectangleRec(MeleeButton.rec, Color.RED);
        }
        else
        {
            Raylib.DrawRectangleRec(RangedButton.rec, Color.RED);
            Raylib.DrawRectangleRec(MeleeButton.rec, Color.GREEN);
        }

        Raylib.DrawText(RangedButton.prompt, 325 - rangedSize / 2, 245, 24, Color.BLACK);
        Raylib.DrawText(MeleeButton.prompt, 595 - rangedSize / 2, 245, 24, Color.BLACK);

        bool mousePressed = Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT);

        if (Raylib.CheckCollisionPointRec(mousePos, RangedButton.rec) && mousePressed)
        {
            player.ranged = true;
        }
        else if (Raylib.CheckCollisionPointRec(mousePos, MeleeButton.rec) && mousePressed)
        {
            player.ranged = false;
        }

        if (store) DisplayStore(ref player);
    }

    private void DisplayStore(ref Player player)
    {

    }
}
