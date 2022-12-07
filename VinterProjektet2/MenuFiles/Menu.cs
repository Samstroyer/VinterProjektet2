using Raylib_cs;
using System;

public abstract class Menu
{
    protected List<Rectangle> menuButtons;

    public Menu()
    {
        menuButtons = new();
    }
}
