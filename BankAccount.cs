namespace ExpenseTracker;

public class BankAccount
{
    public decimal Balance { get; private set; }

    public void MakeDeposit(Buchung? buchung)
    {
        if (buchung != null)
        {
            Balance += buchung.Betrag;
        }
        else
        {
            Console.WriteLine("Ungültige Eingabe!");
        }
    }

    public bool MakeWithdrawal(Buchung? buchung)
    {
        if (buchung == null)
        {
            Console.WriteLine("Auszahlung nicht möglich, Buchung ist nicht vollständig!");
            return false;
        }
        
        if (buchung.Betrag <= Balance)
        {
            Balance -= buchung.Betrag;
            return true;
        }

        Console.WriteLine("Auszahlung nicht möglich. Unzureichender Saldo!");
        return false;
    }

    public void PrintBalance()
    {
        Console.WriteLine($"{Balance:C}");
    }
}