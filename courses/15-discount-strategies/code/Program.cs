// Course 15 - Discount Strategies: turning a tangle of if/else if into
// swappable, testable objects.

var messy = new MessyPriceCalculator();
Console.WriteLine($"Messy, member: {messy.CalculatePrice(100, "member")}");

var calculator = new PriceCalculator();
Console.WriteLine($"Clean, member: {calculator.CalculatePrice(100, new PercentageDiscount(10))}");

// --- Using the factory to pick a strategy from a string ---
var vipDiscount = DiscountStrategyFactory.Create("vip");
Console.WriteLine($"VIP price: {calculator.CalculatePrice(100, vipDiscount)}");

// --- Core exercise: FixedAmountDiscount ---
Console.WriteLine($"Fixed $50 off: {calculator.CalculatePrice(100, new FixedAmountDiscount(50))}");

// --- Challenge: a fourth strategy, wired into the factory ---
var seasonalDiscount = DiscountStrategyFactory.Create("seasonal");
Console.WriteLine($"Seasonal price: {calculator.CalculatePrice(100, seasonalDiscount)}");
