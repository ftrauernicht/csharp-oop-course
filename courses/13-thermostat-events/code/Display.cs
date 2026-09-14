public class Display
{
    public void ShowTemperature(object? sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"Display: it's now {e.NewTemperature} degrees.");
    }
}
