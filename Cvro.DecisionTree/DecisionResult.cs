using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    public class DecisionResult<TInput, TOutput> : Decision<TInput, TOutput>
    {
        public Expression<Func<TInput, TOutput>> CreateResult { get; set; }

        public override TOutput Evaluate(TInput input)
        {
            AssertCanEvaluate();

            var createResult = CreateResult.Compile();
            return createResult.Invoke(input);
        }

        public override Tuple<TOutput, DecisionPath> EvaluateWithPath(TInput input, DecisionPath decisionPath)
        {
            var output = Evaluate(input);
            return new Tuple<TOutput, DecisionPath>(output, decisionPath);
        }

        public override void Accept(IDecisionVisitor<TInput, TOutput> visitor)
        {
            visitor.Visit(this);
        }

        private void AssertCanEvaluate()
        {
            if (CreateResult == null)
                throw new DecisionException($"'{nameof(CreateResult)}' cannot be null");
        }
    }
}