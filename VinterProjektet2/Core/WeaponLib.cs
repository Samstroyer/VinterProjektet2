public static class WeaponLib
{
    public static Ranged Bow = new(500)
    {
        Damage = 55,
        Seeking = false,
        Price = 50,
        Name = "Bow"
    };

    public static Ranged AssaultRifle = new(350)
    {
        Damage = 25,
        Seeking = true,
        Price = 250,
        Name = "AssaultRifle"
    };

    public static Melee Cleaver = new(1000)
    {
        Damage = 60,
        Range = 100,
        Price = 150,
        Name = "Cleaver",
        Piercing = true
    };

    public static Melee Dagger = new(300)
    {
        Damage = 20,
        Range = 20,
        Price = 50,
        Name = "Dagger",
        Piercing = false
    };

    public static Melee Fist = new(200)
    {
        Damage = 10,
        Range = 40,
        Price = 0,
        Name = "Fist",
        Piercing = false
    };
}
