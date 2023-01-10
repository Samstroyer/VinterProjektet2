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
        castle = new();

        player = new();

        ChooseDifficulty();
        LoadDifficulty();

        enemyList = roundGenerator.GetEnemyList(currentRound, difficulty);
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
        //Checks if there are enemies left, 
        if (enemyList.Count <= 0)
        {
            //If none, clear the array and start next round
            enemyList.Clear();
            currentRound++;
            enemyList = roundGenerator.GetEnemyList(currentRound, difficulty);
        }
    }

    private void RoundLogic()
    {
        //Check if there are enemies left
        //Otherwise spawn new and go to next round
        CheckEnemies();

        if (enemyList.Count < 1) return;

        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            if (enemyList[i].IsDead)
            {
                enemyList[i].Unload();
                enemyList.RemoveAt(i);
                continue;
            }

            //Draw update
            enemyList[i].UpdateEnemy(player.position);
            enemyList[i].Attack(ref player);
        }
    }

    private void PlayRound()
    {
        //Render castle and draw walls (castle upgrade)
        castle.Run();

        //Does all the checks for the game and updates enemies
        RoundLogic();

        //Player is handled separately
        PlayerLogic();
    }

    private void PlayerLogic()
    {
        player.Update();
        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        {
            player.Attack(ref enemyList);
        }
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
