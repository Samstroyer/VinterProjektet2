using Raylib_cs;

/*
This is the yearly winter project made by Samuel Palmer - TE20A

I watched a video explaining how great it can be to name and declare more variables than necessary to make it easier to read-
or as he said it: "The code should comment itself".
I like that and that is why it is overkill in the names!

Otherwise this game is inspired by a game I played long time ago.
*/

Core game;
Console.WriteLine("Hello, World!\nInitializing window now!");
Setup();
Loop();

void Setup()
{
    Raylib.InitWindow(900, 900, "ORC GAME!");
    Raylib.SetTargetFPS(60);
    game = new Core();
}

void Loop()
{
    game.Start();
}