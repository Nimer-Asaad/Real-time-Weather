using Weather.App.Models;
using Weather.App.Parsing;
using Weather.App.Core;


namespace Weather.App.Core;


public static class ConsoleUi
{
    public static void Run(ParserRegistry registry, WeatherHub hub)
    {
        Console.WriteLine("Enter weather data (JSON or XML). Type 'exit' to quit.\n");


        while (true)
        {
            Console.Write("Enter weather data: ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;
            if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase)) break;


            var parser = registry.Find(input);
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


            hub.Publish(data);
            Console.WriteLine();
        }
    }
}