using System;

public class Melee : Weapon
{
    public bool Piercing { get; set; }

    public Melee(int cd) : base(cd)
    {

    }
}
