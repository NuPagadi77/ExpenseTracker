namespace ExpenseTracker;

public class BankAccount
{
    public decimal Balance { get; private set; }

    public void MakeDeposit(Buchung buchung)
    {
        Balance += buchung.Betrag;
    }

    public bool MakeWithdrawal(Buchung buchung)
    {
        if (buchung.Betrag > Balance)
            return false;

        Balance -= buchung.Betrag;
        return true;
    }

    public void PrintBalance()
    {
        Console.WriteLine($"{Balance:C}");
    }
}