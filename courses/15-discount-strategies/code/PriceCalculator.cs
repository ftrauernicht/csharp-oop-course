public class PriceCalculator
{
    public int CalculatePrice(int basePrice, IDiscountStrategy discount)
    {
        return discount.Apply(basePrice);
    }
}
