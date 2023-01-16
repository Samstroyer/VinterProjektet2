public static class WeaponLib
{
    public static Ranged Bow = new()
    {
        Damage = 55,
        CooldownMS = 500,
        Seeking = false,
        Price = 50,
        Name = "Bow"
    };

    public static Ranged Seeker = new()
    {
        Damage = 10,
        CooldownMS = 200,
        Seeking = true,
        Price = 250,
        Name = "Seeker"
    };

    public static Melee Cleever = new()
    {
        Damage = 60,
        CooldownMS = 1000,
        Range = 40,
        Price = 150,
        Name = "Cleever"
    };

    public static Melee Dagger = new()
    {
        Damage = 20,
        CooldownMS = 300,
        Range = 20,
        Price = 40,
        Name = "Dagger"
    };

    public static Melee Fists = new()
    {
        Damage = 10,
        CooldownMS = 200,
        Range = 10,
        Price = 0,
        Name = "Fists"
    };
}
