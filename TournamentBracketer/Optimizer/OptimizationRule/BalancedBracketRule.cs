using System.Collections.Generic;
using TournamentBracketer.Bracket;

namespace TournamentBracketer.Optimizer.OptimizationRule
{
    public class BalancedBracketRule : IOptimizationRule
    {
        private const int Scaling = 10;

        public int Grade(BracketTree bracket)
        {
            var nodes = new List<BracketNode>();

            nodes.Add(bracket.Root);

            int? minDepth = null;
            var maxDepth = 0;

            while (nodes.Count > 0)
            {
                var current = nodes[0];
                nodes.RemoveAt(0);

                if (current.IsLeaf)
                {
                    minDepth = current.Depth < minDepth || minDepth is null ? current.Depth : minDepth;
                    maxDepth = current.Depth > maxDepth ? current.Depth : maxDepth;
                }
                else
                {
                    if (current.Left is BracketNode left) nodes.Add(left);
                    if (current.Right is BracketNode right) nodes.Add(right);
                }
            }

            return -Scaling * (maxDepth - minDepth.GetValueOrDefault() + 1);
        }
    }
}