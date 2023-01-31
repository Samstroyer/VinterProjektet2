using System.Numerics;
using Raylib_cs;

public class Core
{
    Mode gamemode;
    Menu currentMenu;
    List<(Rectangle rec, string prompt)> currentButtons;
    Game game;

    private enum Mode
    {
        //This enum keeps track if you are playing or in the menu
        Menu,
        Play
    }

    public Core()
    {
        //Init all variables and make Current-Menu to Start-Menu
        gamemode = Mode.Menu;
        currentMenu = new StartMenu();

        //Create the buttons necessary to use the menu
        currentButtons = currentMenu.menuButtons;
    }

    public void Start()
    {
        //Game loop
        while (!Raylib.WindowShouldClose())
        {
            DisplayCurrent();
        }
    }

    private void DisplayCurrent()
    {
        //Display current menu + fallback
        //If menu becomes dead it resurects in the default block
        switch (gamemode)
        {
            case Mode.Menu:
                CurrentMenu();
                break;

            case Mode.Play:
                StartGame();
                break;

            default:
                Console.WriteLine("There is no gamemode set!");
                Console.WriteLine("'gamemode' is now set to menu.");
                gamemode = Mode.Menu;
                break;
        }
    }

    private void StartGame()
    {
        //Start the game
        game = new();
        game.Start();
        gamemode = Mode.Menu;
    }

    private void CurrentMenu()
    {
        //Draw buttons and check clicks.
        currentMenu.DrawButtons();

        if (!Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT)) return;

        Vector2 mousePos = Raylib.GetMousePosition();
        foreach (var buttonObject in currentButtons)
        {
            if (Raylib.CheckCollisionPointRec(mousePos, buttonObject.rec))
            {
                switch (buttonObject.prompt)
                {
                    case "Play":
                        gamemode = Mode.Play;
                        break;
                }
            }
        }
    }
}
