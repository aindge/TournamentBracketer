using TournamentBracketer.Bracket;

namespace TournamentBracketer.Optimizer.OptimizationRule
{
    public interface IOptimizationRule
    {
        int Grade(BracketTree bracket);
    }
}