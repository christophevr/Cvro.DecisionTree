using System;
using System.Linq;

namespace DecisionTree
{
    public abstract class Decision<TIn, TOut>
    {
        public abstract TOut Evaluate(TIn input);
        public abstract void Accept(IDecisionVisitor<TIn, TOut> visitor);
        public abstract DecisionResultWithPath<TOut> EvaluateWithPath(TIn input, DecisionPath decisionPath);
        public virtual DecisionResultWithPath<TOut> EvaluateWithPath(TIn input)
        {
            return EvaluateWithPath(input, new DecisionPath());
        }
    }
}
