using System.Numerics;

public abstract class Enemy
{
    protected Vector2 pos { get; set; }
    protected float speed { get; set; }
    protected float hitpoints { get; set; }
    protected float damage { get; set; }
    protected float shield { get; set; }

    static protected Random randomGenerator = new();

    public virtual int Attack() => (int)damage;
}