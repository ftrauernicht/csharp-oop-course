public class BankAccountTests
{
    [Fact]
    public void Deposit_IncreasesBalance()
    {
        // Arrange
        var account = new BankAccount(100);

        // Act
        account.Deposit(50);

        // Assert
        Assert.Equal(150, account.Balance);
    }

    [Fact]
    public void Withdraw_DecreasesBalance_WhenFundsAreSufficient()
    {
        var account = new BankAccount(100);

        account.Withdraw(30);

        Assert.Equal(70, account.Balance);
    }

    [Fact]
    public void Withdraw_ThrowsInsufficientFundsException_WhenBalanceTooLow()
    {
        var account = new BankAccount(20);

        Assert.Throws<InsufficientFundsException>(() => account.Withdraw(50));
    }

    [Fact]
    public void Withdraw_LeavesBalanceUnchanged_WhenItThrows()
    {
        var account = new BankAccount(20);

        try
        {
            account.Withdraw(50);
        }
        catch (InsufficientFundsException)
        {
        }

        Assert.Equal(20, account.Balance);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Deposit_ThrowsArgumentException_ForNegativeAmounts(int amount)
    {
        var account = new BankAccount(100);

        Assert.Throws<ArgumentException>(() => account.Deposit(amount));
    }
}
