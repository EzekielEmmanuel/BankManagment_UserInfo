namespace Domain;

public static class BankAccountType
{
    public const string Checking = "Checking";
    public const string Savings = "Savings";
    public const string MoneyMarket = "MoneyMarket";
    public const string HomeMortgage = "HomeMortgage";
    public const string CreditCard = "CreditCard";

    public static IEnumerable<string> GetTypes()
    {
        return new[] {Checking, Savings, MoneyMarket, HomeMortgage, CreditCard};
    }

    // public enum Type {
    //     Checking, 
    //     Savings, 
    //     MoneyMarket, 
    //     HomeMortgage, 
    //     CreditCard
    // }
    // public static Type ToEnum(string value)
    // { 
    //     switch (value)
    //     {
    //         case Checking:
    //             return Type.Checking;
    //         case Savings:
    //             return Type.Savings;
    //         case MoneyMarket:
    //             return Type.MoneyMarket;
    //         case HomeMortgage:
    //             return Type.HomeMortgage;
    //         case CreditCard:
    //             return Type.CreditCard;
    //         default: 
    //             return Type.Checking;
    //     } 
    // }
    // public static string FromEnum(Type value)
    // {
    //     switch (value)
    //     {
    //         case Type.Checking :
    //             return Checking;
    //         case Type.Savings :
    //             return Savings;
    //         case Type.MoneyMarket :
    //             return MoneyMarket;
    //         case Type.HomeMortgage :
    //             return HomeMortgage;
    //         case Type.CreditCard :
    //             return CreditCard;
    //         default: 
    //             return Checking;
    //     } 
    // }
}