public class NoDiscount : IDiscountStrategy
{
    public int Apply(int basePrice)
    {
        return basePrice;
    }
}
