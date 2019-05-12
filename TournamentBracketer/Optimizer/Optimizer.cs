using System;
using TournamentBracketer.Bracket;
using TournamentBracketer.Configuration;

namespace TournamentBracketer.Optimizer
{
    public class Optimizer
    {
        private readonly ConfigurationContext _context;
        private readonly BracketTree _bracket;

        public Optimizer(ConfigurationContext context, BracketTree bracket)
        {
            _context = context;
            _bracket = bracket;
        }

        public void Optimize(int timesRepeated)
        {
            for (int i = 0; i < timesRepeated; i++)
            {
                if (i > 0 && i % (timesRepeated/10) == 0)
                {
                    Console.WriteLine($"{(int)((float)i/timesRepeated * 100)}% complete.");
                }
                FindRandomNode(out var node1, out var node2);

                var oldGrade = Grade();

                SwapEntrants(node1, node2);

                var newGrade = Grade();

                if(newGrade < oldGrade)
                    SwapEntrants(node1, node2);
            }
        }

        public int Grade()
        {
            var grade = 0;
            foreach (var rule in _context.Rules)
            {
                grade += rule.Grade(_bracket);
            }

            return grade;
        }

        private void FindRandomNode(out BracketNode node1, out BracketNode node2)
        {
            var rand = new Random();

            node1 = _bracket.Root;

            while (!node1.IsLeaf)
            {
                node1 = rand.Next() % 2 == 0 ? node1.Left : node1.Right;
            }

            node2 = _bracket.Root;

            while (!node2.IsLeaf)
            {
                node2 = rand.Next() % 2 == 0 ? node2.Left : node2.Right;
            }
        }

        private void SwapEntrants(BracketNode node1, BracketNode node2)
        {
            var temp = node1.Entrant;
            node1.Entrant = node2.Entrant;
            node2.Entrant = temp;
        }
    }
}