using System;

namespace DecisionTree
{
    public abstract class Decision<TIn, TOut>
    {
        public abstract TOut Evaluate(TIn input);
        public abstract void Accept(IDecisionVisitor<TIn, TOut> visitor);
        public abstract Tuple<TOut, DecisionPath> EvaluateWithPath(TIn input, DecisionPath decisionPath);
        public virtual Tuple<TOut, DecisionPath> EvaluateWithPath(TIn input)
        {
            return EvaluateWithPath(input, new DecisionPath());
        }
    }
}
