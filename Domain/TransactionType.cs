namespace Domain;

public static class TransactionType
{
    public const string Deposit = "Deposit";
    public const string Withdrawal = "Withdrawal";
    public const string Transfer = "Transfer";
    public const string Bill = "Bill";
    public const string Interest = "Interest";
    public const string Fee = "Fee";

    public static IEnumerable<string> GetTypes()
    {
        return new[] {Deposit, Withdrawal, Transfer, Bill, Interest, Fee};
    }
}