// Uppgift 2.1 
// Programmering i C#.NET DT071G -- HT2025
// Maamoun Okla , maok1900@student.miun.se

using System;

class Program
{
    static void Main()
    {
        
        Console.WriteLine("Ange ditt födelsedatum (YYYYMMDD eller YYYY-MM-DD):");
        string input = Console.ReadLine()?.Trim() ?? "";

        // Validera och parse datum (strikt format)
        if (!TryParseDate(input, out DateTime birthDate, out string err))
        {
            Console.WriteLine(err);
            return;
        }

        // Stoppa framtida datum
        if (birthDate.Date > DateTime.Today)
        {
            Console.WriteLine("Datumet kan inte ligga i framtiden.");
            return;
        }

        // Zeller’s algoritm för veckodag (0 = lördag, 1 = söndag, 2 = måndag, ... , 6 = fredag)
        int h = ZellerDayOfWeek(birthDate.Day, birthDate.Month, birthDate.Year);

        string weekdaySv = ZellerIndexToSwedish(h);
        Console.WriteLine($"Du är född en {weekdaySv}.");

        // Folktro-text
        PrintFolklore(weekdaySv);
    }

    // Zeller’s kongruens (Gregorianska kalendern)
    static int ZellerDayOfWeek(int d, int m, int year)
    {
        int Y = year;
        int M = m;

        if (M < 3)
        {
            M += 12;   // Jan=13, Feb=14
            Y -= 1;
        }

        int c = Y / 100;     // århundrade
        int y = Y % 100;     // år i seklet

        int h = (d + (13 * (M + 1)) / 5 + y + (y / 4) + (c / 4) + (5 * c)) % 7;
        return h; // 0..6
    }

    // Mappa Zeller-index till svensk veckodag.
    static string ZellerIndexToSwedish(int h)
    {
        return h switch
        {
            0 => "lördag",
            1 => "söndag",
            2 => "måndag",
            3 => "tisdag",
            4 => "onsdag",
            5 => "torsdag",
            6 => "fredag",
            _ => "okänd"
        };
    }

    // Försöker läsa YYYYMMDD eller YYYY-MM-DD till DateTime.
    static bool TryParseDate(string s, out DateTime date, out string error)
    {
        date = default;
        error = "";

        if (string.IsNullOrWhiteSpace(s))
        {
            error = "Ingen inmatning gjordes.";
            return false;
        }

        // Försök tolka båda formaten 
        try
        {
            if (s.Contains("-"))
            {
                // Format YYYY-MM-DD
                string[] parts = s.Split('-');
                if (parts.Length != 3)
                {
                    error = "Fel format! Använd YYYY-MM-DD eller YYYYMMDD.";
                    return false;
                }
                int year = int.Parse(parts[0]);
                int month = int.Parse(parts[1]);
                int day = int.Parse(parts[2]);
                date = new DateTime(year, month, day);
                return true;
            }
            else if (s.Length == 8)
            {
                // Format YYYYMMDD
                int year = int.Parse(s.Substring(0, 4));
                int month = int.Parse(s.Substring(4, 2));
                int day = int.Parse(s.Substring(6, 2));
                date = new DateTime(year, month, day);
                return true;
            }
            else
            {
                error = "Fel format! Använd YYYY-MM-DD eller YYYYMMDD.";
                return false;
            }
        }
        catch
        {
            error = "Ogiltigt datum! Kontrollera år, månad och dag.";
            return false;
        }
    }

    // Skriv ut folktro-text baserat på veckodag.
    static void PrintFolklore(string weekday)
    {
        switch (weekday.ToLower())
        {
            case "måndag":
                Console.WriteLine("Måndagsbarn har fagert skinn.");
                break;
            case "tisdag":
                Console.WriteLine("Tisdagsbarn har älskligt sinn.");
                break;
            case "onsdag":
                Console.WriteLine("Onsdagsbarn är fött till ve.");
                break;
            case "torsdag":
                Console.WriteLine("Torsdagsbarn får mycket se.");
                break;
            case "fredag":
                Console.WriteLine("Fredagsbarn får kärlek och lycka.");
                break;
            case "lördag":
                Console.WriteLine("Lördagsbarn ska mödorna trycka.");
                break;
            case "söndag":
                Console.WriteLine("Söndagsbarn får leva och njuta rikt och vist.");
                break;
        }
    }
}
