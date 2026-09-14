// The Cat class: a blueprint that groups a cat's data (its properties) and
// the behavior that belongs to it (its methods) into one unit.
public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string FavoriteToy { get; set; }

    public Cat(string name, int age, string favoriteToy)
    {
        Name = name;
        Age = age;
        FavoriteToy = favoriteToy;
    }

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}! Age: {Age}. Favorite toy: {FavoriteToy}.");
    }

    public void HaveBirthday()
    {
        Age++;
        Console.WriteLine($"{Name} just turned {Age}!");
    }

    public bool IsOlderThan(Cat other)
    {
        return Age > other.Age;
    }
}
