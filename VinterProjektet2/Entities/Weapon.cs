using System.Timers;
using System;

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

    public Weapon(int cd)
    {
        cooldown = new();
        cooldown.AutoReset = false;
        cooldown.Elapsed += ResetReady;
        cooldown.Interval = cd;
        cooldown.Start();
    }

    private void ResetReady(Object source, ElapsedEventArgs e)
    {
        ready = true;
    }

    private int Use(int damage)
    {
        if (!ready) return 0;
        ready = false;
        cooldown.Start();
        return damage;
    }
}
