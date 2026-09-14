// Optional: a machine that doesn't override Attack() at all. Calling
// Attack() on a Grazer runs Machine's own generic version unchanged --
// `virtual` means a method CAN be overridden, not that it MUST be.
public class Grazer : Machine
{
    public Grazer() : base("Grazer", 50)
    {
    }
}
