using System.Collections.Generic;
using TournamentBracketer.Optimizer;
using TournamentBracketer.Optimizer.OptimizationRule;

namespace TournamentBracketer.Configuration.Strategy
{
    public class PlayerLocalityStrategy : OptionalStrategy
    {
        public PlayerLocalityStrategy(ConfigurationContext ctx, IEnumerable<string> args) : base(ctx, args)
        {

        }

        protected override IOptimizationRule GetRule()
        {
            return new PlayerLocalityRule();
        }
    }
}