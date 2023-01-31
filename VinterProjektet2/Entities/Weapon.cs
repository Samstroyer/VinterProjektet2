using System.Numerics;
using System.Timers;

/* 
The different types of weapons I want to implement:

The fast firerate but low damage weapon,
The slow firerate but high damage weapon,
*/


public abstract class Weapon
{
    public int Range { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public List<Projectile>? projectiles;

    public bool ready;
    private System.Timers.Timer cooldown;

    private int DamageNumber;
    public int Damage
    {
        get
        {
            return Use(DamageNumber);
        }
        set
        {
            DamageNumber = value;
        }
    }

    //When a weapon is created it is created with a cooldown
    public Weapon(int cd)
    {
        cooldown = new();
        cooldown.AutoReset = false;
        cooldown.Elapsed += ResetReady;
        cooldown.Interval = cd;
        cooldown.Start();
    }

    //Resets cooldown when called
    private void ResetReady(Object source, ElapsedEventArgs e)
    {
        ready = true;
    }

    //Use is to get the damage if the weapon is ready
    private int Use(int damage)
    {
        if (!ready) return 0;
        ready = false;
        cooldown.Start();
        return damage;
    }

    //For ranged, could probably be an interface
    public virtual void FireProjectile(Player.Direction dir, Vector2 position) { }
}
