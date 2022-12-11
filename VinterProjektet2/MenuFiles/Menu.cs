using Raylib_cs;

public abstract class Menu
{
    protected List<(Rectangle rec, string prompt)> menuButtons;
    protected const int fontSize = 48;
    protected const int buttonWidth = 400;

    public Menu()
    {
        menuButtons = new();
    }

    public virtual void DrawButtons()
    {
        foreach (var buttonObject in menuButtons)
        {
            var rec = buttonObject.rec;
            var prompt = buttonObject.prompt;
            Raylib.DrawRectangleRec(rec, Color.RED);

            int textLength = Raylib.MeasureText(prompt, fontSize);
            int width = (Raylib.GetScreenWidth() / 2) - (textLength / 2);
            Raylib.DrawText(prompt, width, (int)rec.y + 20, fontSize, Color.WHITE);
        }
    }
}
