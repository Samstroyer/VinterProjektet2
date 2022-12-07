using Raylib_cs;
using System;

public abstract class Menu
{
    protected List<(Rectangle, string)> menuButtons;
    protected const int fontSize = 48;

    public Menu()
    {
        menuButtons = new();
    }

    public virtual void DrawButtons()
    {
        foreach (var container in menuButtons)
        {

        }
    }
}
