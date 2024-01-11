using System;
using System.IO;

namespace test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Adja meg a dátumot: ");
            string datumString = Console.ReadLine();

            DateTime datum;
            if (DateTime.TryParseExact(datumString, "yyyy.MM.dd", null, System.Globalization.DateTimeStyles.None, out datum))
            {
                Console.WriteLine("Válasszon menüpontot:");
                Console.WriteLine("1. futás");
                Console.WriteLine("2. séta");
                Console.WriteLine("3. úszás");
                double menu = Convert.ToDouble(Console.ReadLine());

                string valasztas = " ";
                switch (menu)
                {
                    case 1:valasztas = "futás";
                          break;
                    case 2:
                        valasztas = "séta";
                        break;
                    case 3:
                        valasztas = "úszás";
                        break;
                }
                Console.WriteLine("Adja meg az időt percben: ");
                int idoPerc;
                if (int.TryParse(Console.ReadLine(), out idoPerc))
                {
                    string filePath = "data.txt";
                    string data = $"{datum.ToString("yyyy.MM.dd")} {valasztas} {idoPerc} perc";

                    File.AppendAllText(filePath, data + Environment.NewLine);
                    Console.WriteLine("Adatok írva a fájlba.");

                    Console.WriteLine("\nFájlban lévő adatok:");
                    ReadFileContents(filePath);
                }
                else
                {
                    Console.WriteLine("Érvénytelen időformátum. A program leáll.");
                }
            }
            else
            {
                Console.WriteLine("Érvénytelen dátumformátum. A program leáll.");
            }

            Console.ReadKey();
        }

        private static void ReadFileContents(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
