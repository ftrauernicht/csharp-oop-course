public class PercentageDiscount : IDiscountStrategy
{
    private readonly int _percent;

    public PercentageDiscount(int percent)
    {
        _percent = percent;
    }

    public int Apply(int basePrice)
    {
        return basePrice - (basePrice * _percent / 100);
    }
}
