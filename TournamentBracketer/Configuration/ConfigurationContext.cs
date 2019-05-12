using System.Collections.Generic;
using System.Security;
using TournamentBracketer.Bracket;
using TournamentBracketer.Optimizer;
using TournamentBracketer.Optimizer.OptimizationRule;

namespace TournamentBracketer.Configuration
{
    public class ConfigurationContext
    {
        public List<Entrant> Entrants { get; } = new List<Entrant>();

        public List<IOptimizationRule> Rules { get; } = new List<IOptimizationRule>();

        public ConfigurationContext() { }

        public ConfigurationContext(IEnumerable<Entrant> entrants)
        {
            Entrants.AddRange(entrants);
        }
    }
}