// A crop with a validated WaterLevel: no matter how it's set -- through
// Water(...) or directly -- it can never leave the 0-100 range.
public class Crop
{
    private int _waterLevel;

    public string Name { get; }
    public bool IsHarvestable { get; private set; }

    public int WaterLevel
    {
        get => _waterLevel;
        set
        {
            if (value < 0)
            {
                _waterLevel = 0;
            }
            else if (value > 100)
            {
                _waterLevel = 100;
            }
            else
            {
                _waterLevel = value;
            }

            if (_waterLevel == 100)
            {
                IsHarvestable = true;
            }
        }
    }

    public Crop(string name)
    {
        Name = name;
        WaterLevel = 0;
    }

    public void Water(int amount)
    {
        WaterLevel += amount;
    }

    public void Harvest()
    {
        if (IsHarvestable)
        {
            Console.WriteLine($"Harvested {Name}!");
            WaterLevel = 0;
            IsHarvestable = false;
        }
        else
        {
            Console.WriteLine($"{Name} isn't ready to harvest yet.");
        }
    }
}
