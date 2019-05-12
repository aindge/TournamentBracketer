using System.Collections.Generic;
using TournamentBracketer.Optimizer.OptimizationRule;

namespace TournamentBracketer.Configuration.Strategy
{
    public class MultiEntrantDepthStrategy : OptionalStrategy
    {
        public MultiEntrantDepthStrategy(ConfigurationContext ctx, IEnumerable<string> args) : base(ctx, args)
        {
        }

        protected override IOptimizationRule GetRule()
        {
            return new MultiEntrantDepthRule();
        }
    }
}