public class TemperatureChangedEventArgs : EventArgs
{
    public int NewTemperature { get; }

    public TemperatureChangedEventArgs(int newTemperature)
    {
        NewTemperature = newTemperature;
    }
}
