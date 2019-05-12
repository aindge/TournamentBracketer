using System;
using System.Collections.Generic;

namespace TournamentBracketer.Bracket
{
    public class BracketNode
    {
        public BracketNode() { }

        public BracketNode(BracketNode parent)
        {
            Parent = parent;
        }
        public Entrant? Entrant { get; set; }

        public BracketNode Parent { get; set; }

        public BracketNode Left { get; set; }

        public BracketNode Right { get; set; }

        public void Split(Entrant newEntrant)
        {
            if (Left is null && Right is null)
            {
                Left = new BracketNode(this) { Entrant = Entrant };
                Right = new BracketNode(this) { Entrant = newEntrant };
                Entrant = null;
            }
        }

        public IEnumerable<BracketNode> Ancestors()
        {
            var current = this;
            do
            {
                yield return current;
                current = current.Parent;
            } while (current.Parent != null);
        }

        public int Depth => Parent?.Depth + 1 ?? 0;
        public bool IsLeaf => Left is null && Right is null;

        public BracketNode Find(Entrant entrant)
        {
            return Entrant != null && Entrant.Value == entrant ? this : Left?.Find(entrant) ?? Right?.Find(entrant);
        }

        public string GetFormattedString()
        {
            if (IsLeaf)
            {
                return new string(' ', Depth) + $"-{Entrant.ToString()}";
            }
            else
            {
                return new string(' ', Depth) + "|";
            }
        }

        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "| ";
            }
            Console.WriteLine(Entrant);

            var children = new List<BracketNode>();
            if (this.Left != null)
                children.Add(this.Left);
            if (this.Right != null)
                children.Add(this.Right);

            for (int i = 0; i < children.Count; i++)
                children[i].PrintPretty(indent, i == children.Count - 1);
        }
    }
}