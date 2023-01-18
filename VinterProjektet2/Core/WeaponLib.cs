public static class WeaponLib
{
    public static Ranged Bow = new(500, true)
    {
        Damage = 35,
        Seeking = false,
        Price = 50,
        Name = "Bow"
    };

    public static Ranged AssaultRifle = new(350, false)
    {
        Damage = 50,
        Seeking = true,
        Price = 250,
        Name = "AssaultRifle"
    };

    public static Melee Cleaver = new(750)
    {
        Damage = 60,
        Range = 125,
        Price = 150,
        Name = "Cleaver",
        Piercing = true
    };

    public static Melee Dagger = new(150)
    {
        Damage = 20,
        Range = 70,
        Price = 50,
        Name = "Dagger",
        Piercing = false
    };

    public static Melee Fist = new(300)
    {
        Damage = 15,
        Range = 55,
        Price = 0,
        Name = "Fist",
        Piercing = false
    };
}
