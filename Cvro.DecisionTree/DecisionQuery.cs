using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    public class DecisionQuery<TIn, TOut> : Decision<TIn, TOut>
    {
        public Expression<Func<TIn, bool>> Test { get; set; }
        public Decision<TIn, TOut> Positive { get; set; }
        public Decision<TIn, TOut> Negative { get; set; }

        public override TOut Evaluate(TIn input)
        {
            AssertCanEvaluate();

            var result = GetResult(input);

            if (result)
                return Positive.Evaluate(input);
            else
                return Negative.Evaluate(input);
        }
        
        public override Tuple<TOut, DecisionPath> EvaluateWithPath(TIn input, DecisionPath decisionPath)
        {
            AssertCanEvaluate();

            var result = GetResult(input);
            decisionPath.AddStep(result);

            if (result)
                return Positive.EvaluateWithPath(input, decisionPath);
            else
                return Negative.EvaluateWithPath(input, decisionPath);
        }

        public override void Accept(IDecisionVisitor<TIn, TOut> visitor)
        {
            visitor.Visit(this);
        }

        private bool GetResult(TIn input)
        {
            var test = Test.Compile();
            return test.Invoke(input);
        }

        public override string ToString()
        {
            return $"{Test} [{HashHelper.CalculateShortHash(Test.ToString())}]";
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