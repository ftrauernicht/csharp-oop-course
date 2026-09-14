// A static class: never instantiated with `new`, its one method is
// called directly on the class name. Appropriate here -- a factory holds
// no state of its own, it just decides which object to build.
public static class DiscountStrategyFactory
{
    public static IDiscountStrategy Create(string customerType)
    {
        if (customerType == "regular")
        {
            return new NoDiscount();
        }
        else if (customerType == "member")
        {
            return new PercentageDiscount(10);
        }
        else if (customerType == "vip")
        {
            return new FixedAmountDiscount(50);
        }
        else if (customerType == "seasonal")
        {
            return new SeasonalDiscount();
        }
        else
        {
            throw new ArgumentException($"Unknown customer type: {customerType}");
        }
    }
}
