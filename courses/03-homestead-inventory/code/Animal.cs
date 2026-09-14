// Challenge solution: the same validated-property pattern as Crop,
// applied to a second, independent class.
public class Animal
{
    private int _happiness;

    public string Name { get; }

    public int Happiness
    {
        get => _happiness;
        set
        {
            if (value < 0)
            {
                _happiness = 0;
            }
            else if (value > 100)
            {
                _happiness = 100;
            }
            else
            {
                _happiness = value;
            }
        }
    }

    public Animal(string name)
    {
        Name = name;
        Happiness = 50;
    }

    public void Pet()
    {
        Happiness += 20;
        Console.WriteLine($"{Name} is happier now! Happiness: {Happiness}");
    }
}
