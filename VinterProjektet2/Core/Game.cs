using System.Text.Json;
using System.Numerics;
using System.Timers;
using Raylib_cs;

public class Game
{
    Player player;
    Difficulty difficulty;
    RoundGenerator roundGenerator;
    Castle castle;
    List<Enemy> enemyList;

    System.Timers.Timer eventTimer;
    bool displayCurrentRound = true;

    private int currentRound = 0;


    public Game()
    {
        string json = File.ReadAllText("./Difficulties/Difficulties.json");
        roundGenerator = JsonSerializer.Deserialize<RoundGenerator>(json);

        eventTimer = new(2500);
        eventTimer.AutoReset = false;
        eventTimer.Elapsed += Event;

        castle = new();

        player = new();

        ChooseDifficulty();
        LoadDifficulty();

        enemyList = roundGenerator.GetEnemyList(currentRound, difficulty);

        eventTimer.Start();
    }

    public void Event(Object source, ElapsedEventArgs e)
    {
        //This event updates the display to be off, 
        //I do not want it to display the current round all the time
        displayCurrentRound = false;
    }

    public void Start()
    {
        while (true)
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            PlayRound();

            Raylib.EndDrawing();

            if ((KeyboardKey)Raylib.GetKeyPressed() == KeyboardKey.KEY_I)
            {
                //This is tested to not have memory leaks
                player.Inv.Open = !player.Inv.Open;
                Raylib.TakeScreenshot("LatestSS.png");

                Raylib.UnloadTexture(player.Inv.background);
                player.Inv.background = Raylib.LoadTexture("LatestSS.png");
            }
        }
    }

    private void CheckEnemies()
    {
        //Checks if there are enemies left, 
        if (enemyList.Count <= 0)
        {
            //If none, start next round and display it
            currentRound++;
            enemyList = roundGenerator.GetEnemyList(currentRound, difficulty);

            displayCurrentRound = true;
            eventTimer.Start();
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
                player.AddCurrency(enemyList[i].Loot());
                enemyList[i].Unload();
                enemyList.RemoveAt(i);
                Console.WriteLine(player.Coins);
                continue;
            }

            //Draw update
            enemyList[i].UpdateEnemy(player.position);
            enemyList[i].Attack(ref player);
        }
    }

    private void PlayRound()
    {
        if (player.Inv.Open)
        {
            player.Inv.Display();
            player.Inv.Update(ref player);
        }
        else
        {
            //Render castle and draw walls (castle upgrade)
            castle.Run();

            //Does all the checks for the game and updates enemies
            RoundLogic();
            if (displayCurrentRound)
            {
                ShowCurrentRound();
            }

            //Player is handled separately
            PlayerLogic();
        }
    }

    private void ShowCurrentRound()
    {
        int fontSize = 48;
        string prompt = $"Current round: {currentRound}";
        int size = Raylib.MeasureText(prompt, fontSize);

        int tempX = (Raylib.GetScreenWidth() / 2) - (size / 2);

        Raylib.DrawText($"Current round: {currentRound}", tempX, 20, fontSize, Color.RED);
    }

    private void PlayerLogic()
    {
        player.CheckProjectiles(ref enemyList);
        player.Update();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && player.canAttack)
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
