using Raylib_cs;
using System;

public class Game
{
    bool inMenu;
    Mode gamemode;
    Menu currentMenu;

    private enum Mode
    {
        Menu,
        Play
    }

    public Game()
    {
        inMenu = true;
        gamemode = Mode.Menu;
        currentMenu = new StartMenu();
    }

    public void Start()
    {
        while (!Raylib.WindowShouldClose())
        {
            switch (gamemode)
            {
                case Mode.Menu:

                    break;
                case Mode.Play:

                    break;
                default:
                    Console.WriteLine("There is no gamemode set!");
                    Console.WriteLine("'gamemode' is now set to menu.");
                    gamemode = Mode.Menu;
                    break;
            }
        }
    }
}
