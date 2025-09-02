using System.Text;
using Weather.App.Models;
using Weather.App.Parsing;
using Weather.App.Core;

namespace Weather.App.Core
{
    public static class ConsoleUi
    {
        public static void Run(ParserRegistry registry, WeatherHub hub)
        {
            PrintHeader();

            while (true)
            {
                var input = ReadBlock();                    // يدعم أسطر متعددة
                if (string.IsNullOrWhiteSpace(input))       // تجاهل الفراغ
                    continue;

                if (IsExit(input))                          // خروج
                    break;

                var parser = registry.Find(input);          // Strategy: اختيار البارسر
                if (parser is null)
                {
                    Console.WriteLine("Unrecognized format. Please provide JSON or XML.\n");
                    continue;
                }

                if (!parser.TryParse(input, out var data) || data is null)
                {
                    Console.WriteLine("Failed to parse input.\n");
                    continue;
                }

                hub.Publish(data);                          // Observer: نشر التحديث
                Console.WriteLine();
            }
        }

        private static string ReadBlock()
        {
            Console.Write("Enter weather data: ");
            var sb = new StringBuilder();

            // نبدأ قراءة الأسطر حتى سطر فارغ أو 'exit'
            while (true)
            {
                var line = Console.ReadLine();
                if (line is null) break;                         // EOF (إغلاق الإدخال)
                if (IsExit(line)) return "exit";                 // خروج فوري
                if (line.Length == 0) break;                     // سطر فارغ = نهاية الإدخال

                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        private static bool IsExit(string s)
            => s.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase);

        private static void PrintHeader()
        {
            Console.WriteLine("Enter weather data (JSON or XML).");
            Console.WriteLine("Paste multi-line, then press ENTER on an empty line to submit.");
            Console.WriteLine("Type 'exit' alone to quit.\n");
        }
    }
}
