namespace ExpenseTracker;

class Program
{
    static void Main(string[] args)
    {
        List<Buchung> buchungen = new();
        BankAccount bankAccount = new();
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

                    decimal betrag = ReadPositiveDecimal("Betrag eingeben: ");

                    Console.Write("Buchungsart (Einnahme/Ausgabe): ");
                    string? artInput = Console.ReadLine()?.Trim().ToLowerInvariant();

                    if (TryParseBuchungsart(artInput, out Buchung.Buchungsart art))
                    {
                        string kategorie = ReadRequired("Kategorie eingeben: ");
                        string beschreibung = ReadRequired("Beschreibung eingeben: ");

                        Buchung buchung = new(betrag, art, kategorie, beschreibung);
                        if (art == Buchung.Buchungsart.Einnahme)
                        {
                            bankAccount.MakeDeposit(buchung);
                            buchungen.Add(buchung);
                            Console.WriteLine($"Buchung {buchung.Betrag:C} wurde erfasst");
                            Console.Write("Aktueller Kontostand: ");
                            bankAccount.PrintBalance();
                        }
                        else if (art == Buchung.Buchungsart.Ausgabe)
                        {
                            if (bankAccount.MakeWithdrawal(buchung))
                            {
                                buchungen.Add(buchung);
                                Console.WriteLine($"Buchung {buchung.Betrag:C} wurde erfasst");
                                Console.Write("Aktueller Kontostand: ");
                                bankAccount.PrintBalance();
                            }
                            else
                            {
                                Console.WriteLine("Auszahlung nicht möglich. Unzureichender Saldo!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Art - erwartet: Einnahme oder Ausgabe");
                    }

                    Console.ReadLine();
                    break;
                case 2:
                    int anzahl = Buchung.ShowBuchung(buchungen);
                    if (anzahl == 0)
                    {
                        Console.WriteLine("Keine Buchungen");
                    }

                    Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Aktueller Kontostand: ");
                    bankAccount.PrintBalance();
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
        Console.WriteLine("3-Kontostand anzeigen");
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

    private static string ReadRequired(string message)
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Eingabe darf nicht leer sein.");
            Console.Write(message);
            input = Console.ReadLine();
        }

        return input.Trim();
    }

    private static decimal ReadPositiveDecimal(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            if (decimal.TryParse(input, out decimal value) && value > 0)
                return value;
            Console.WriteLine("Bitte einen Betrag größer 0 eingeben!");
        }
    }

    private static bool TryParseBuchungsart(string? input, out Buchung.Buchungsart art)
    {
        string normalized = input?.Trim().ToLowerInvariant() ?? "";

        switch (normalized)
        {
            case "einnahme":
            case "ein":
                art = Buchung.Buchungsart.Einnahme;
                return true;
            case "ausgabe":
            case "aus":
                art = Buchung.Buchungsart.Ausgabe;
                return true;
            default:
                art = default;
                return false;
        }
    }
}