using Raylib_cs;

public class StartMenu : Menu
{
    public StartMenu() : base()
    {
        InitButtons();
    }

    private void InitButtons()
    {
        // "Play" and "Help" will be the buttons here
        int width = Raylib.GetScreenWidth();
        int height = Raylib.GetScreenHeight();

        //Play button - the only one created now
        int xPos = (width / 2) - (buttonWidth / 2);
        Rectangle playButtonRec = new(xPos, 300, buttonWidth, 100);
        menuButtons.Add(new(playButtonRec, "Play"));
    }

    public override void DrawButtons()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.GRAY);

        base.DrawButtons();
        Hover();

        Raylib.EndDrawing();
    }

    protected override void Hover()
    {
        base.Hover();
    }
}
