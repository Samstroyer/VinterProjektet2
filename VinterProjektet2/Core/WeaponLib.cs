public static class WeaponLib
{
    public static Ranged Bow = new(500)
    {
        Damage = 55,
        Seeking = false,
        Price = 50,
        Name = "Bow"
    };

    public static Ranged Seeker = new(200)
    {
        Damage = 10,
        Seeking = true,
        Price = 250,
        Name = "Seeker"
    };

    public static Melee Cleever = new(1000)
    {
        Damage = 60,
        Range = 100,
        Price = 150,
        Name = "Cleever",
        Piercing = true
    };

    public static Melee Dagger = new(300)
    {
        Damage = 20,
        Range = 20,
        Price = 50,
        Name = "Dagger"
    };

    public static Melee Fists = new(200)
    {
        Damage = 10,
        Range = 40,
        Price = 0,
        Name = "Fists"
    };
}
