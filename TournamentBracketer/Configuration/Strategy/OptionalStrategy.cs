using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TournamentBracketer.Optimizer.OptimizationRule;

namespace TournamentBracketer.Configuration.Strategy
{
    public abstract class OptionalStrategy
    {
        protected ConfigurationContext Context { get; }

        protected IEnumerable<string> Arguments { get; set; }

        public virtual int ExpectedArguments => 0;

        protected OptionalStrategy(ConfigurationContext ctx, IEnumerable<string> args)
        {
            Context = ctx;
            Arguments = args;
        }

        public bool ValidateArguments()
        {
            return Arguments.Count() == ExpectedArguments;
        }

        public void AddRule(ConfigurationContext ctx)
        {
            ctx.Rules.Add(GetRule());
        }

        protected abstract IOptimizationRule GetRule();

        public static Type GetStrategyType(string keyword)
        {
            return keyword == "Optional" ? null // Prevent return of this class
                : Assembly.GetAssembly(typeof(OptionalStrategy)).GetType($"{typeof(OptionalStrategy).Namespace}.{keyword}Strategy");
        }
    }
}