public class InsufficientFundsException : Exception
{
    public int Requested { get; }
    public int Available { get; }

    public InsufficientFundsException(int requested, int available)
        : base($"Tried to spend {requested} coins, but only {available} are available.")
    {
        Requested = requested;
        Available = available;
    }
}
