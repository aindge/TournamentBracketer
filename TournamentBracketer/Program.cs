using System;
using System.IO;
using TournamentBracketer.Common;
using TournamentBracketer.Configuration;

namespace TournamentBracketer
{
    class Program
    {
        private const string TestFile = @"C:\Users\woops\OneDrive\Documents\Tournament1SampleConfig.txt";

        static void Main(string[] args)
        {
            Console.Write("Enter a file location: ");

            var location = Console.ReadLine();

            if (location == "")
            {
                location = TestFile;
            }

            Console.WriteLine($"using {location}");

            var lines = File.ReadAllLines(location);
            
                var context = new ConfigurationManager(lines).Configure();

                var bracket = new Bracket.BracketTree(context.Entrants);

                var optimizer = new Optimizer.Optimizer(context, bracket);

                optimizer.Optimize(500000);

                bracket.Root.PrintPretty("", true);

            Console.ReadLine();
        }
    }
}
