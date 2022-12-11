using Raylib_cs;

public class Game
{
    bool inMenu;
    Mode gamemode;
    Menu currentMenu;
    Castle castle;

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
        castle = new();
    }

    public void Start()
    {
        while (!Raylib.WindowShouldClose())
        {
            switch (gamemode)
            {
                case Mode.Menu:
                    currentMenu.DrawButtons();
                    break;

                case Mode.Play:
                    castle.Start();
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
