// Course 9 - General Store: running out of money is a real, named
// failure (InsufficientFundsException), not a silent bug.

var account = new BankAccount(20);
var store = new Store();

store.Purchase(account, "Bread", 5);
Console.WriteLine($"Balance: {account.Balance}");

try
{
    store.Purchase(account, "Sword", 50);
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}

Console.WriteLine($"Balance after failed purchase: {account.Balance}");

// --- Optional: finally always runs, success or failure ---
try
{
    store.Purchase(account, "Potion", 12);
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}
finally
{
    Console.WriteLine("Transaction attempt finished.");
}

// --- Challenge: a built-in exception type for a different kind of mistake ---
try
{
    account.Deposit(-10);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Deposit rejected: {ex.Message}");
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}
