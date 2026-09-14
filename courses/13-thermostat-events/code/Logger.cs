public class Logger
{
    public void LogChange(object? sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"Logger: temperature changed to {e.NewTemperature} degrees.");
    }
}
