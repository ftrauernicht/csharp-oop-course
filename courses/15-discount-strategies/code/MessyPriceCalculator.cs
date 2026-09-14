// The problem this chapter fixes: every new customer type means editing
// this method again, and the discount math for one customer type can't
// be tested without going through all the others' branches too.
public class MessyPriceCalculator
{
    public int CalculatePrice(int basePrice, string customerType)
    {
        if (customerType == "regular")
        {
            return basePrice;
        }
        else if (customerType == "member")
        {
            return basePrice - (basePrice * 10 / 100);
        }
        else if (customerType == "vip")
        {
            return basePrice - 50;
        }
        else
        {
            throw new ArgumentException($"Unknown customer type: {customerType}");
        }
    }
}
