namespace ExpenseTracker;

public class Buchung
{
    public int ID { get; set; }
    public decimal Betrag { get; set; }
    public DateTime Datum { get; set; }
    public string? Art { get; set; }
    public string? Kategorie { get; set; }
    public string? Beschreibung { get; set; }

}