using System;
using TournamentBracketer.Optimizer.OptimizationRule;

namespace TournamentBracketer.Bracket
{
    public struct Entrant
    {
        public Entrant(string name, string player)
        {
            Name = name;
            Player = player;
        }

        public Entrant(Entrant entrant)
        {
            Name = entrant.Name;
            Player = entrant.Player;
        }

        public string Name { get; }

        public string Player { get; }

        public override string ToString()
        {
            return $"({Name}, {Player})";
        }

        public static bool operator ==(Entrant a, object other)
        {
            return a.Equals(other);
        }

        public static bool operator !=(Entrant a, object other)
        {
            return !(a == other);
        }

        public override bool Equals(object obj)
        {
            return obj is Entrant other && (other.Name == Name && other.Player == Player);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Player != null ? Player.GetHashCode() : 0);
            }
        }
    }
}