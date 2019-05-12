using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TournamentBracketer.Bracket
{
    public class BracketTree
    {
        public BracketNode Root { get; }

        public BracketTree(IEnumerable<Entrant> entrants)
        {
            foreach (var entrant in entrants)
            {
                if (Root is null)
                {
                    Root = new BracketNode(){Entrant = entrant};
                }
                else
                {
                    var leaf = AllLeaves().ToList().OrderBy(l => l.Depth).First(); // TODO: May be slow
                    
                    leaf.Split(entrant);
                }
            }
        }

        public BracketNode Find(Entrant entrant)
        {
            return Root.Find(entrant);
        }

        public IEnumerable<BracketNode> AllLeaves()
        {
            var nodes = new List<BracketNode> {Root};

            while (nodes.Count > 0)
            {
                var current = nodes[0];
                nodes.RemoveAt(0);

                if (current.IsLeaf)
                {
                    yield return current;
                }
                else
                {
                    if (current.Left is BracketNode left) nodes.Add(left);
                    if (current.Right is BracketNode right) nodes.Add(right);
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            FormatString(sb, Root);

            return sb.ToString();
        }

        public void FormatString(StringBuilder sb, BracketNode node)
        {
            if (node is null) return;

            FormatString(sb, node.Left);
            sb.AppendLine(node.GetFormattedString());
            FormatString(sb, node.Right);
        }
    }
}