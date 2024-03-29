using System.Numerics;
using Raylib_cs;

//To make it "simple" I will have some static weapons (WeaponLib.cs)
//A.K.A implemented manually and limited
//No procedural weapons sadly...

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
        //This code is to load all the weapons in an dictionary to later be able and access them
        Type type = typeof(WeaponLib);

        foreach (var item in type.GetFields())
        {
            Weapon weapon = (Weapon)item.GetValue(this);

            ownedWeapons.Add(weapon.Name, new(weapon, false));
        }

        //Set Fist and Bow to be available on start
        ownedWeapons["Fist"] = new(ownedWeapons["Fist"].weapon, true);
        ownedWeapons["Bow"] = new(ownedWeapons["Bow"].weapon, true);
    }

    public void Display()
    {
        //There are better ways shown than taking a scrrenshot
        //, but it will take too long trying to fix it now (I tried)
        Raylib.DrawTexture(background, 0, 0, Color.WHITE);
        Raylib.DrawTexture(ImageLib.InventoryTexture, 0, 0, Color.WHITE);
    }

    public void Update(ref Player player)
    {
        //Draws the inventory
        Vector2 mousePos = Raylib.GetMousePosition();
        int rangedSize = Raylib.MeasureText(RangedButton.prompt, 24);
        int meleeSize = Raylib.MeasureText(MeleeButton.prompt, 24);

        //If ranged is true, show ranged inventory
        if (player.ranged)
        {
            Raylib.DrawRectangleRec(RangedButton.rec, Color.GREEN);
            Raylib.DrawRectangleRec(MeleeButton.rec, Color.RED);

            RangedChooser(ref player);
        }
        //Else show melee inventory
        else
        {
            Raylib.DrawRectangleRec(RangedButton.rec, Color.RED);
            Raylib.DrawRectangleRec(MeleeButton.rec, Color.GREEN);

            MeleeChooser(ref player);
        }

        Raylib.DrawText(RangedButton.prompt, 325 - rangedSize / 2, 245, 24, Color.BLACK);
        Raylib.DrawText(MeleeButton.prompt, 595 - rangedSize / 2, 245, 24, Color.BLACK);

        bool mousePressed = Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT);

        //Switch ranged and melee
        if (Raylib.CheckCollisionPointRec(mousePos, RangedButton.rec) && mousePressed)
        {
            player.ranged = true;
        }
        else if (Raylib.CheckCollisionPointRec(mousePos, MeleeButton.rec) && mousePressed)
        {
            player.ranged = false;
        }

        //Show bank account :)
        string text = $"Money: {player.Coins}";
        int textSize = Raylib.MeasureText(text, 48);
        Raylib.DrawText(text, 450 - textSize / 2, 450, 48, Color.YELLOW);
    }

    private void MeleeChooser(ref Player player)
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        Color recLines;
        int textSize = 0;
        string text;

        Rectangle tempGuide;

        //Fist
        tempGuide = new(50, 700, 100, 100);
        recLines = ownedWeapons["Fist"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["Fist"].owned ? "Sold" : ownedWeapons["Fist"].weapon.Price.ToString();
        if (ownedWeapons["Fist"].weapon == player.Inv.equipped) recLines = Color.ORANGE;
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(tempGuide, 10, recLines);
        Raylib.DrawTexture(ImageLib.FistTexture, 50, 700, Color.WHITE);
        Raylib.DrawText(text, 100 - textSize / 2, 820, 24, recLines);

        if (Raylib.CheckCollisionPointRec(mousePos, tempGuide)
        && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            if (player.Coins > ownedWeapons["Fist"].weapon.Price
            && !ownedWeapons["Fist"].owned)
            {
                player.Coins -= ownedWeapons["Fist"].weapon.Price;
                ownedWeapons["Fist"] = new(ownedWeapons["Fist"].weapon, true);
                player.Inv.equipped = ownedWeapons["Fist"].weapon;
            }
            else if (ownedWeapons["Fist"].owned) player.Inv.equipped = ownedWeapons["Fist"].weapon;

        //Cleaver
        tempGuide = new(200, 700, 100, 100);
        recLines = ownedWeapons["Cleaver"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["Cleaver"].owned ? "Sold" : ownedWeapons["Cleaver"].weapon.Price.ToString();
        if (ownedWeapons["Cleaver"].weapon == player.Inv.equipped) recLines = Color.ORANGE;
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(tempGuide, 10, recLines);
        Raylib.DrawTexture(ImageLib.CleaverTexture, 200, 700, Color.WHITE);
        Raylib.DrawText(text, 250 - textSize / 2, 820, 24, recLines);

        if (Raylib.CheckCollisionPointRec(mousePos, tempGuide)
        && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            if (player.Coins > ownedWeapons["Cleaver"].weapon.Price
            && !ownedWeapons["Cleaver"].owned)
            {
                player.Coins -= ownedWeapons["Cleaver"].weapon.Price;
                ownedWeapons["Cleaver"] = new(ownedWeapons["Cleaver"].weapon, true);
                player.Inv.equipped = ownedWeapons["Cleaver"].weapon;
            }
            else if (ownedWeapons["Cleaver"].owned) player.Inv.equipped = ownedWeapons["Cleaver"].weapon;

        //Dagger
        tempGuide = new(350, 700, 100, 100);
        recLines = ownedWeapons["Dagger"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["Dagger"].owned ? "Sold" : ownedWeapons["Dagger"].weapon.Price.ToString();
        if (ownedWeapons["Dagger"].weapon == player.Inv.equipped) recLines = Color.ORANGE;
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(tempGuide, 10, recLines);
        Raylib.DrawTexture(ImageLib.DaggerTextuer, 350, 700, Color.WHITE);
        Raylib.DrawText(text, 400 - textSize / 2, 820, 24, recLines);

        if (Raylib.CheckCollisionPointRec(mousePos, tempGuide)
        && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            if (player.Coins > ownedWeapons["Dagger"].weapon.Price
            && !ownedWeapons["Dagger"].owned)
            {
                player.Coins -= ownedWeapons["Dagger"].weapon.Price;
                ownedWeapons["Dagger"] = new(ownedWeapons["Dagger"].weapon, true);
                player.Inv.equipped = ownedWeapons["Dagger"].weapon;
            }
            else if (ownedWeapons["Dagger"].owned) player.Inv.equipped = ownedWeapons["Dagger"].weapon;
    }

    private void RangedChooser(ref Player player)
    {
        Vector2 mousePos = Raylib.GetMousePosition();

        Color recLines;
        int textSize = 0;
        string text;

        Rectangle tempGuide;

        //Bow
        tempGuide = new(50, 700, 100, 100);
        recLines = ownedWeapons["Bow"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["Bow"].owned ? "Sold" : ownedWeapons["Bow"].weapon.Price.ToString();
        if (ownedWeapons["Bow"].weapon == player.Inv.equipped) recLines = Color.ORANGE;
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(tempGuide, 10, recLines);
        Raylib.DrawTexture(ImageLib.BowTexture, 50, 700, Color.WHITE);
        Raylib.DrawText(text, 100 - textSize / 2, 820, 24, recLines);

        if (Raylib.CheckCollisionPointRec(mousePos, tempGuide)
        && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            if (player.Coins > ownedWeapons["Bow"].weapon.Price
            && !ownedWeapons["Bow"].owned)
            {
                player.Coins -= ownedWeapons["Bow"].weapon.Price;
                ownedWeapons["Bow"] = new(ownedWeapons["Bow"].weapon, true);
                player.Inv.equipped = ownedWeapons["Bow"].weapon;
            }
            else if (ownedWeapons["Bow"].owned) player.Inv.equipped = ownedWeapons["Bow"].weapon;

        //AssaultRifle
        tempGuide = new(200, 700, 100, 100);
        recLines = ownedWeapons["AssaultRifle"].owned ? Color.GREEN : Color.RED;
        text = ownedWeapons["AssaultRifle"].owned ? "Sold" : ownedWeapons["AssaultRifle"].weapon.Price.ToString();
        if (ownedWeapons["AssaultRifle"].weapon == player.Inv.equipped) recLines = Color.ORANGE;
        textSize = Raylib.MeasureText(text, 24);
        Raylib.DrawRectangleLinesEx(tempGuide, 10, recLines);
        Raylib.DrawTexture(ImageLib.AssaultRifleTexture, 200, 700, Color.WHITE);
        Raylib.DrawText(text, 250 - textSize / 2, 820, 24, recLines);

        if (Raylib.CheckCollisionPointRec(mousePos, tempGuide)
        && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            if (player.Coins > ownedWeapons["AssaultRifle"].weapon.Price
            && !ownedWeapons["AssaultRifle"].owned)
            {
                player.Coins -= ownedWeapons["AssaultRifle"].weapon.Price;
                ownedWeapons["AssaultRifle"] = new(ownedWeapons["AssaultRifle"].weapon, true);
                player.Inv.equipped = ownedWeapons["AssaultRifle"].weapon;
            }
            else if (ownedWeapons["AssaultRifle"].owned) player.Inv.equipped = ownedWeapons["AssaultRifle"].weapon;
    }
}
