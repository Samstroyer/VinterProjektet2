using System.Timers;

//An interface so that UpdateSprite exists for all enemies

public interface ISprite
{
    private void UpdateSprite(Object source, ElapsedEventArgs e) { }
}