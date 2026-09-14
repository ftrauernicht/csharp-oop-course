public class BankAccount
{
    public int Balance { get; private set; }

    public BankAccount(int startingBalance)
    {
        Balance = startingBalance;
    }

    public void Deposit(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }

        Balance += amount;
    }

    public void Withdraw(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.");
        }

        if (amount > Balance)
        {
            throw new InsufficientFundsException(amount, Balance);
        }

        Balance -= amount;
    }
}
