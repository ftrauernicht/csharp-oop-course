public class Store
{
    public void Purchase(BankAccount account, string itemName, int price)
    {
        account.Withdraw(price);
        Console.WriteLine($"Bought {itemName} for {price} coins.");
    }
}
