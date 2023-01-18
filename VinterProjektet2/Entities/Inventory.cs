using System.Numerics;
using Raylib_cs;

//To make it "simple" I will have some static weapons
//A.K.A implemented manually

public class Inventory
{
    public bool Open { get; set; } = false;
    public Dictionary<string, (Weapon weapon, bool owned)> ownedWeapons = new();

    public Weapon equipped = WeaponLib.Fist;

    public Texture2D background;

    private (Rectangle rec, string prompt) RangedButton = new(new(225, 200, 200, 100), "Ranged");
    private (Rectangle rec, string prompt) MeleeButton = new(new(475, 200, 200, 100), "Melee");

    bool store = false;

    public Inventory()
    {
        Type type = typeof(WeaponLib);


        foreach (var item in type.GetFields())
        {
            Weapon weapon = (Weapon)item.GetValue(this);

            ownedWeapons.Add(weapon.Name, new(weapon, false));
        }

        ownedWeapons["Fist"] = new(ownedWeapons["Fist"].weapon, true);
    }

    public void Display()
    {
        //There are better ways shown, but it will take too long trying to fix it
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

            RangedChooser(ref player);
        }
        else
        {
            Raylib.DrawRectangleRec(RangedButton.rec, Color.RED);
            Raylib.DrawRectangleRec(MeleeButton.rec, Color.GREEN);

            MeleeChooser(ref player);
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
    }

    private void MeleeChooser(ref Player player)
    {
        Color recLines;
        int textSize = 0;
        string text;

        //Fists

        recLines = ownedWeapons["Fist"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["Fist"].owned ? "Sold" : ownedWeapons["Fist"].weapon.Price.ToString();
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(new(50, 700, 100, 100), 10, recLines);
        Raylib.DrawTexture(ImageLib.FistTexture, 50, 700, Color.WHITE);
        Raylib.DrawText(text, 100 - textSize / 2, 820, 24, recLines);

        //Cleaver
        recLines = ownedWeapons["Cleaver"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["Cleaver"].owned ? "Sold" : ownedWeapons["Cleaver"].weapon.Price.ToString();
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(new(200, 700, 100, 100), 10, recLines);
        Raylib.DrawTexture(ImageLib.CleaverTexture, 200, 700, Color.WHITE);
        Raylib.DrawText(text, 250 - textSize / 2, 820, 24, recLines);

        //Dagger
        recLines = ownedWeapons["Dagger"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["Dagger"].owned ? "Sold" : ownedWeapons["Dagger"].weapon.Price.ToString();
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(new(350, 700, 100, 100), 10, recLines);
        Raylib.DrawTexture(ImageLib.DaggerTextuer, 350, 700, Color.WHITE);
        Raylib.DrawText(text, 400 - textSize / 2, 820, 24, recLines);
    }

    private void RangedChooser(ref Player player)
    {
        Color recLines;
        int textSize = 0;
        string text;

        //Bow
        recLines = ownedWeapons["Bow"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["Bow"].owned ? "Sold" : ownedWeapons["Bow"].weapon.Price.ToString();
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(new(50, 700, 100, 100), 10, recLines);
        Raylib.DrawTexture(ImageLib.BowTexture, 50, 700, Color.WHITE);
        Raylib.DrawText(text, 100 - textSize / 2, 820, 24, recLines);

        //AssaultRifle
        recLines = ownedWeapons["AssaultRifle"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["AssaultRifle"].owned ? "Sold" : ownedWeapons["AssaultRifle"].weapon.Price.ToString();
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(new(200, 700, 100, 100), 10, recLines);
        Raylib.DrawTexture(ImageLib.AssaultRifleTexture, 200, 700, Color.WHITE);
        Raylib.DrawText(text, 250 - textSize / 2, 820, 24, recLines);
    }
}
