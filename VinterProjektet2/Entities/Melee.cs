using System;

//Melee class, only adds piercing modifier

public class Melee : Weapon
{
    public bool Piercing { get; set; }

    public Melee(int cd) : base(cd)
    {

    }
}
