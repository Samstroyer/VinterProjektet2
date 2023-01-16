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

    public int CooldownMS
    {
        get
        {
            return CooldownMS;
        }
        set
        {
            cooldown.Interval = value;
        }
    }

    private System.Timers.Timer cooldown = new();

    public int Damage { get; set; }
}
