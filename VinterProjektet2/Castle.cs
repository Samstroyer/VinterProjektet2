using System.Numerics;
using Raylib_cs;
using System;

public class Castle
{
    const int sizeMult = 3;
    Vector2 mapSize;
    Player player;

    public Castle()
    {
        player = new();
        mapSize = new(Raylib.GetScreenWidth() * sizeMult, Raylib.GetScreenHeight() * sizeMult);
    }

    public void Run()
    {
        Render();
    }

    private void Render()
    {
        Vector2 playerPos = player.pos;

        for (int i = 0; i < Raylib.GetScreenWidth(); i++)
        {

        }
    }

}
