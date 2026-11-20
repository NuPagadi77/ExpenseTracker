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
                    Console.Write("Betrag eingeben: ");
                    string? input = Console.ReadLine();
                    if (!decimal.TryParse(input, out decimal betrag))
                    {
                        Console.WriteLine("Bitte einen gültigen Betrag eingeben!");
                    }
                    else
                    {
                        Console.Write("Buchungsart eingeben(Einnahme/Ausgabe): ");
                        string? art = Console.ReadLine();
                        Console.Write("Kategorie eingeben: ");
                        string? kategorie = Console.ReadLine();
                        Console.Write("Beschreibung eingeben: ");
                        string? beschreibung = Console.ReadLine();

                        Buchung buchung = new(betrag, art, kategorie, beschreibung);

                        if (art?.ToLower() == "einzahlung")
                        {
                            bankAccount.MakeDeposit(buchung);
                            buchungen.Add(buchung);
                            Console.WriteLine($"Buchung {buchung.Betrag:C}  wurde erfasst");
                        }
                        else if (art?.ToLower() == "auszahlung")
                        {
                            if (bankAccount.MakeWithdrawal(buchung))
                            {
                                buchungen.Add(buchung);
                                Console.WriteLine($"Buchung {buchung.Betrag:C}  wurde erfasst");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Falsche Buchungsart");
                        }
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
                    Console.WriteLine("Aktueller Kontostand: ");
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
}