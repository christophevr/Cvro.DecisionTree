using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    public class DecisionQuery<TInput, TOutput> : Decision<TInput, TOutput>
    {
        public Expression<Func<TInput, bool>> Test { get; set; }
        public Decision<TInput, TOutput> Positive { get; set; }
        public Decision<TInput, TOutput> Negative { get; set; }

        public override TOutput Evaluate(TInput input)
        {
            AssertCanEvaluate();

            var result = GetResult(input);

            if (result)
                return Positive.Evaluate(input);
            else
                return Negative.Evaluate(input);
        }
        
        public override Tuple<TOutput, DecisionPath> EvaluateWithPath(TInput input, DecisionPath decisionPath)
        {
            AssertCanEvaluate();

            var result = GetResult(input);
            decisionPath.AddStep(result);

            if (result)
                return Positive.EvaluateWithPath(input, decisionPath);
            else
                return Negative.EvaluateWithPath(input, decisionPath);
        }

        public override void Accept(IDecisionVisitor<TInput, TOutput> visitor)
        {
            visitor.Visit(this);
        }

        private bool GetResult(TInput input)
        {
            var test = Test.Compile();
            return test.Invoke(input);
        }

        private void AssertCanEvaluate()
        {
            if (Test == null)
                throw new DecisionException($"'{nameof(Test)}' cannot be null");
            if (Positive == null)
                throw new DecisionException($"'{nameof(Positive)}' cannot be null");
            if (Negative == null)
                throw new DecisionException($"'{nameof(Negative)}' cannot be null");
        }
    }
}