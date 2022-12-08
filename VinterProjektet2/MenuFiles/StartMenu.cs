using Raylib_cs;
using System;

public class StartMenu : Menu
{
    public StartMenu() : base() { }

    private void InitButtons()
    {
        string[] buttonPrompts = new string[] {
            "Play",
            "Help"
        };

        int buttonAmount = buttonPrompts.Count();
        int spacePerButton = Raylib.GetScreenHeight() / buttonAmount;

        int xOffset = spacePerButton / 2;

        foreach (string prompt in buttonPrompts)
        {
            menuButtons.Add(new());
        }



    }
}
