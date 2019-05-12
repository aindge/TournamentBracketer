using System;
using System.Collections.Generic;
using System.Linq;
using TournamentBracketer.Bracket;
using TournamentBracketer.Configuration.Strategy;

namespace TournamentBracketer.Configuration
{
    public class ConfigurationManager
    {
        private readonly IEnumerable<string> _lines;
        public ConfigurationManager(IEnumerable<string> lines)
        {
            _lines = lines;
        }

        public ConfigurationContext Configure()
        {
            var parseMode = ParseMode.None;
            var lineNumber = 0;
            var context = new ConfigurationContext();

            foreach (var line in _lines)
            {
                lineNumber++;
                if (line.Trim().StartsWith('#')) continue; // It's a comment

                var tokens = line.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 0) continue; // Whitespace

                if (Enum.TryParse<ParseMode>(tokens[0], out var mode))
                {
                    if(tokens.Length > 1)
                        throw new InvalidOperationException("Mode change should not have arguments");
                    parseMode = mode;
                    continue; // Mode change should be on its own line.
                }

                switch (parseMode)
                {
                    case ParseMode.Entrants:
                        if (tokens.Length != 2)
                            throw new InvalidOperationException($"Wrong number of arguments for entrant. Expected 2. Line {lineNumber}: {string.Join(" ", tokens)}");
                        context.Entrants.Add(new Entrant(tokens[0], tokens[1]));
                        break;

                    case ParseMode.Options:
                        var parameters = new object[] {context, tokens.Skip(1)};
                        var strategy = Activator.CreateInstance(OptionalStrategy.GetStrategyType(tokens[0]), parameters) as OptionalStrategy;
                        if (strategy is null)
                            throw new InvalidOperationException($"No rules found for keyword {tokens[0]}. Line {lineNumber}");
                        if (!strategy.ValidateArguments())
                            throw new InvalidOperationException($"Wrong number of arguments for rule {tokens[0]}. Expected {strategy.ExpectedArguments}, got {tokens.Length - 1}");
                        strategy.AddRule(context);
                        break;

                    case ParseMode.None:
                        throw new InvalidOperationException("Parse mode never specified.");
                }
            }

            return context;
        }

        private enum ParseMode
        {
            None, Entrants, Options
        }
    }

     
}