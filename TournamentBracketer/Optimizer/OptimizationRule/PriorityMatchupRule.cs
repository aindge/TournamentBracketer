using System;
using System.Linq;
using TournamentBracketer.Bracket;

namespace TournamentBracketer.Optimizer.OptimizationRule
{
    public class PriorityMatchupRule : IOptimizationRule
    {
        private const int Scaling = -3;

        public Entrant Entrant1 { get; }
        public Entrant Entrant2 { get; }

        public PriorityMatchupRule(Entrant entrant1, Entrant entrant2)
        {
            Entrant1 = entrant1;
            Entrant2 = entrant2;
        }

        public int Grade(BracketTree bracket)
        {
            var node1 = bracket.Find(Entrant1);
            var node2 = bracket.Find(Entrant2);
            var commonAncestor = node1;

            var found = false;
            while (!found && commonAncestor != null)
            {
                commonAncestor = commonAncestor.Parent;

                if (commonAncestor.Find(node2.Entrant.Value) != null)
                {
                    found = true;
                }
            }

            if(commonAncestor is null)
                throw new InvalidOperationException("Common ancestor is null!");

            return ((node1.Depth - commonAncestor.Depth) + (node2.Depth - commonAncestor.Depth)) * Scaling;
        }
    }
}