using System.Numerics;
using Raylib_cs;

public class Core
{
    Mode gamemode;
    Menu currentMenu;
    List<(Rectangle rec, string prompt)> currentButtons;
    Game castle;

    private enum Mode
    {
        Menu,
        Play
    }

    public Core()
    {
        gamemode = Mode.Menu;
        currentMenu = new StartMenu();

        currentButtons = currentMenu.menuButtons;
    }

    public void Start()
    {
        while (!Raylib.WindowShouldClose())
        {
            DisplayCurrent();
        }
    }

    private void DisplayCurrent()
    {
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
        castle = new();
        castle.Start();
        gamemode = Mode.Menu;
    }

    private void CurrentMenu()
    {
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
