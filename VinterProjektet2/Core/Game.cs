using System.Numerics;
using Raylib_cs;

public class Game
{
    Mode gamemode;
    Menu currentMenu;
    List<(Rectangle rec, string prompt)> currentButtons;
    Castle castle;

    private enum Mode
    {
        Menu,
        Play
    }

    public Game()
    {
        gamemode = Mode.Menu;
        currentMenu = new StartMenu();
        castle = new();

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
