namespace ExpenseTracker;

public class Buchung
{
    public decimal Betrag { get; private set; }
    public DateTime Datum { get; set; }
    public Buchungsart Art { get; set; }
    public string? Kategorie { get; set; }
    public string? Beschreibung { get; set; }

    public enum Buchungsart
    {
        Einnahme,
        Ausgabe
    }

    public Buchung(decimal betrag, Buchungsart art, string? kategorie, string? beschreibung)
    {
        Betrag = betrag;
        Datum = DateTime.Now;
        Art = art;
        Beschreibung = beschreibung;
        Kategorie = kategorie;
    }

    public static int ShowBuchung(List<Buchung> buchungen)
    {
        int counter = 0;
        foreach (var buchung in buchungen)
        {
            Console.WriteLine(
                $"#{counter + 1} | {buchung.Datum} | {buchung.Art} | {buchung.Betrag:C} | {buchung.Kategorie} | {buchung.Beschreibung}");
            counter++;
        }

        return counter;
    }
}