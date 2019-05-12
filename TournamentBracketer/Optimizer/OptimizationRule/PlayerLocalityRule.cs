using System;
using System.Linq;
using TournamentBracketer.Bracket;
using TournamentBracketer.Optimizer.OptimizationRule;

namespace TournamentBracketer.Optimizer
{
    public class PlayerLocalityRule : IOptimizationRule
    {
        private const int Scaling = 1;

        public int Grade(BracketTree bracket)
        {
            throw new NotImplementedException();
        }
    }
}