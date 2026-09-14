public class Thermostat
{
    public event EventHandler<TemperatureChangedEventArgs>? TemperatureChanged;

    private int _temperature;

    public int Temperature
    {
        get { return _temperature; }
        set
        {
            _temperature = value;
            TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs(_temperature));
        }
    }
}
