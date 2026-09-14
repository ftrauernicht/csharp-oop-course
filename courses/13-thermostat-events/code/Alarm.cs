// Challenge solution: a third, independent subscriber. Thermostat never
// needed a single line changed to support it.
public class Alarm
{
    private readonly int _threshold;

    public Alarm(int threshold)
    {
        _threshold = threshold;
    }

    public void CheckTemperature(object? sender, TemperatureChangedEventArgs e)
    {
        if (e.NewTemperature > _threshold)
        {
            Console.WriteLine($"Alarm: {e.NewTemperature} degrees is above the {_threshold}-degree threshold!");
        }
    }
}
