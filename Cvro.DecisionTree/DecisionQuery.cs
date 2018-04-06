using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a decision query. A branch in a decision tree.
    /// </summary>
    /// <typeparam name="TIn">Input</typeparam>
    /// <typeparam name="TOut">Output</typeparam>
    public class DecisionQuery<TIn, TOut> : Decision<TIn, TOut>
    {
        /// <summary>
        /// The condition for this query
        /// </summary>
        public Expression<Func<TIn, bool>> Test { get; set; }

        /// <summary>
        /// The positive branch
        /// </summary>
        public Decision<TIn, TOut> Positive { get; set; }

        /// <summary>
        /// The negative branch
        /// </summary>
        public Decision<TIn, TOut> Negative { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Evaluates the decision query
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>The output</returns>
        public override TOut Evaluate(TIn input)
        {
            AssertCanEvaluate();
            var result = GetResult(input);

            if (result)
                return Positive.Evaluate(input);
            else
                return Negative.Evaluate(input);
        }

        /// <inheritdoc />
        public override DecisionResultWithPath<TOut> EvaluateWithPath(TIn input, DecisionPath decisionPath)
        {
            AssertCanEvaluate();

            var result = GetResult(input);
            decisionPath.AddStep(result);

            if (result)
                return Positive.EvaluateWithPath(input, decisionPath);
            else
                return Negative.EvaluateWithPath(input, decisionPath);
        }

        /// <inheritdoc />
        public override void Accept(IDecisionVisitor<TIn, TOut> visitor)
        {
            visitor.Visit(this);
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the condition as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Test.ToString();
        }

        private bool GetResult(TIn input)
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