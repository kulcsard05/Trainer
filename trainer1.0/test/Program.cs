using System;

namespace test
{
    internal class Program
    {
        private static int selectedMenuIndex = 1;

        public static void Main(string[] args)
        {
            Console.WriteLine("Adja meg a dátumot: ");
            string datumString = Console.ReadLine();
            //Dátum bekérése és ellenörzése
            DateTime datum;
            if (DateTime.TryParseExact(datumString, "yyyy.MM.dd", null, System.Globalization.DateTimeStyles.None, out datum))
            {
                Console.Clear();//konzol törlése

                menu();

                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey();
                    Console.Clear();

                    AdjustSelectedIndex(key);

                    menu();

                } while (key.Key != ConsoleKey.Enter);

                HandleSelectedOption(datum);

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Érvénytelen dátumformátum. A program leáll.");//hiba
            }
        }
        //menüpontok
        private static void menu()
        {
            Console.WriteLine("Válasszon menüpontot:");
            menuItem("1. futás", 1);
            menuItem("2. séta", 2);
            menuItem("3. úszás", 3);
        }

        private static void menuItem(string text, int menuIndex)
        {
            //menü kezelés és választott menüpont szinezése
            if (menuIndex == selectedMenuIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{text} [Választva]");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(text);
            }
        }

        private static void AdjustSelectedIndex(ConsoleKeyInfo key)
        {//nyilakkal való navigáció
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    selectedMenuIndex = (selectedMenuIndex % 3) + 1;
                    break;
                case ConsoleKey.UpArrow:
                    selectedMenuIndex = (selectedMenuIndex - 2 + 3) % 3 + 1;
                    break;
            }
        }

        private static void HandleSelectedOption(DateTime datum)
        {
            string valasztas = " ";
            switch (selectedMenuIndex)
            {
                case 1:
                    valasztas = "futás";
                    break;
                case 2:
                    valasztas = "séta";
                    break;
                case 3:
                    valasztas = "úszás";
                    break;
            }
            //idő bekérése
            Console.WriteLine($"Adja meg az időt percben a(z) {valasztas} menüpontban: ");
            int idoPerc;
            if (int.TryParse(Console.ReadLine(), out idoPerc))
            {
                string filePath = "data.txt";
                string data = $"{datum.ToString("yyyy.MM.dd")} {valasztas} {idoPerc} perc";

                File.AppendAllText(filePath, data + Environment.NewLine);
                Console.WriteLine("Adatok írva a fájlba.");

                Console.WriteLine("\nFájlban lévő adatok:");
                file(filePath);
            }
            else
            {
                Console.WriteLine("Érvénytelen időformátum. A program leáll.");
            }
        }
        //fileba írás
        private static void file(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
