// Challenge solution: a fourth strategy. Neither PriceCalculator nor any
// existing IDiscountStrategy class needed a single line changed for it to
// exist -- only DiscountStrategyFactory grew one more branch.
public class SeasonalDiscount : IDiscountStrategy
{
    public int Apply(int basePrice)
    {
        return basePrice - (basePrice * 20 / 100);
    }
}
