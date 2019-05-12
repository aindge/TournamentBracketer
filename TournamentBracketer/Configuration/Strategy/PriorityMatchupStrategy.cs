using System.Collections.Generic;
using System.Linq;
using TournamentBracketer.Bracket;
using TournamentBracketer.Optimizer.OptimizationRule;

namespace TournamentBracketer.Configuration.Strategy
{
    public class PriorityMatchupStrategy : OptionalStrategy
    {
        public override int ExpectedArguments => 4;

        public PriorityMatchupStrategy(ConfigurationContext ctx, IEnumerable<string> args) : base(ctx, args)
        {
        }

        protected override IOptimizationRule GetRule()
        {
            var args = Arguments.ToList();

            var entrant1 = new Entrant(args[0], args[1]);
            var entrant2 = new Entrant(args[2], args[3]);

            return new PriorityMatchupRule(entrant1, entrant2);
        }
    }
}