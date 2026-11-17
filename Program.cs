using System.Globalization;
using System.Runtime.InteropServices.JavaScript;

namespace ExpenseTracker;

class Program
{
    static void Main(string[] args)
    {
        List<Buchung> _buchungen = new();
        int option;
        do
        {
            Console.Clear();
            ShowMenu();

            option = ReadInt("Option auswählen: ");
            switch (option)
            {
                case 1:
                    Console.WriteLine("Buchung wird erfasst: ");
                    Console.Write("Betrag eingeben: ");
                    string input = Console.ReadLine();
                    if (!decimal.TryParse(input, out decimal betrag))
                    {
                        Console.WriteLine("Bitte einen gültigen Betrag eingeben!");
                    }
                    else
                    {
                        Console.Write("Buchungsart eingeben(Ein/Auszahlung): ");
                        string? art = Console.ReadLine();
                        Console.Write("Kategorie eingeben: ");
                        string? kategorie = Console.ReadLine();
                        Console.Write("Beschreibung eingeben: ");
                        string? beschreibung = Console.ReadLine();
                        Buchung buchung = CreateBuchung(betrag, art, kategorie, beschreibung);
                        _buchungen.Add(buchung);
                        Console.WriteLine($"Buchung {buchung.Betrag:C}  wurde erfasst");
                    }
                    Console.ReadLine();
                    break;
                case 2:

                    Console.WriteLine("Buchung wird angezeigt: ");
                    ShowBuchung(_buchungen);
                   // Console.WriteLine($"Betrag: {ShowBuchung(buchung)}");
                    //Console.WriteLine($"Datum: {buchung.Datum}");
                    Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Saldo wird angezeigt: ");
                    Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("Programm wird beendet!");
                    break;
                default:
                    Console.WriteLine("Falsche Eingabe! Bitte eine gültige Zahl eingeben [1-4]");
                    Console.ReadLine();
                    break;
            }
        } while (option != 4);

    }
    private static void ShowMenu()
    {
        Console.WriteLine("ExpenseTracker");
        Console.WriteLine("1-Buchung erfassen");
        Console.WriteLine("2-Buchung anzeigen");
        Console.WriteLine("3-Saldo anzeigen");
        Console.WriteLine("4-Beenden");
        Console.WriteLine();
    }

    private static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                return number;
            }
            Console.WriteLine("Bitte eine gültige Zahl eingeben.");
        }
    }

    private static Buchung CreateBuchung(decimal betrag, string? art, string? kategorie, string? beschreibung)
    {
        //List<Buchung> _buchung = new();
        Buchung buchung = new Buchung
        {
            Betrag = betrag,
            Datum = DateTime.Now,
            Art = art,
            Beschreibung = beschreibung,
            Kategorie = kategorie
        };
        
        return buchung;
    }

    private static void ShowBuchung(List<Buchung> buchungen)
    {
        foreach (var buchung in buchungen)
        {
            Console.WriteLine($"{buchung.Betrag:C}" );
            Console.WriteLine(buchung.Art);
            Console.WriteLine(buchung.Datum);
        }
    }
}