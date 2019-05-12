using TournamentBracketer.Bracket;

namespace TournamentBracketer.Optimizer.OptimizationRule
{
    public class DefaultRule : IOptimizationRule
    {
        public int Grade(BracketTree bracket)
        {
            return 0;
        }
    }
}