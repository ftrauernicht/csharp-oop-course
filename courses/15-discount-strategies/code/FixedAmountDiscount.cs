// Core exercise answer key: the same shape as PercentageDiscount.
public class FixedAmountDiscount : IDiscountStrategy
{
    private readonly int _amount;

    public FixedAmountDiscount(int amount)
    {
        _amount = amount;
    }

    public int Apply(int basePrice)
    {
        return basePrice - _amount;
    }
}
