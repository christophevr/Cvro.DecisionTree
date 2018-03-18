using System;
using System.Collections;

namespace Cvro.DecisionTree
{
    public abstract class Decision<TInput, TOutput>
    {
        public abstract TOutput Evaluate(TInput input);
        public abstract void Accept(IDecisionVisitor<TInput, TOutput> visitor);
        public abstract Tuple<TOutput, DecisionPath> EvaluateWithPath(TInput input, DecisionPath decisionPath);
        public virtual Tuple<TOutput, DecisionPath> EvaluateWithPath(TInput input)
        {
            return EvaluateWithPath(input, new DecisionPath());
        }
    }
}
