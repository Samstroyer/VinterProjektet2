using System.Numerics;
using Raylib_cs;

public class Game
{
    Player player;
    Difficulty difficulty;
    RoundGenerator roundGenerator;
    Castle castle;
    List<Enemy> enemyList;

    private int currentRound = 0;

    public Game()
    {
        roundGenerator = new();

        enemyList = roundGenerator.GetEnemyList(currentRound, difficulty);
        player = new();

        ChooseDifficulty();
        LoadDifficulty();

        castle = new();
    }

    public void Start()
    {
        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            PlayRound();

            Raylib.EndDrawing();
        }
    }

    private void CheckEnemies()
    {
        if (enemyList.Count <= 0)
        {
            enemyList.Clear();
            currentRound++;
            enemyList = roundGenerator.GetEnemyList(currentRound, difficulty);
        }
    }

    private void PlayRound()
    {
        CheckEnemies();

        castle.Render();

        player.Update();
    }

    //why not play with regions
    #region todo 
    private void LoadDifficulty()
    {

    }
    #endregion todo

    private void ChooseDifficulty()
    {
        int width = Raylib.GetScreenWidth();
        int size = 200;
        List<(Rectangle rec, string prompt)> difficultyButtons = new()
        {
            new(new((width / 2) - (size / 2), 400, size, 50), "Easy"),
            new(new((width / 2) - (size / 2), 500, size, 50), "Medium"),
            new(new((width / 2) - (size / 2), 600, size, 50), "Hard")
        };

        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.GRAY);

            Vector2 mousePos = Raylib.GetMousePosition();
            foreach (var buttonObject in difficultyButtons)
            {
                bool hover = Raylib.CheckCollisionPointRec(mousePos, buttonObject.rec);
                bool clicked = Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON);
                Color col = Color.RED;

                if (hover && clicked)
                {
                    SetDifficulty(buttonObject.prompt);
                    return;
                }
                else if (hover)
                {
                    col = Color.GREEN;
                }

                Raylib.DrawRectangleRec(buttonObject.rec, col);
            }

            Raylib.EndDrawing();
        }
    }

    private void SetDifficulty(string difficultyName)
    {
        switch (difficultyName)
        {
            case "Easy":
                difficulty = Difficulty.Easy;
                break;
            case "Medium":
                difficulty = Difficulty.Medium;
                break;
            case "Hard":
                difficulty = Difficulty.Hard;
                break;
            default:
                Console.WriteLine($"No difficulty found, Error : {difficulty}");
                Console.WriteLine("Setting difficulty to normal, how hard can it be :)");
                difficulty = Difficulty.Medium;
                break;
        }
    }
}
