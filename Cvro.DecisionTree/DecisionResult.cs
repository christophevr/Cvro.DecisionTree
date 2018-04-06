using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a decision result. An end node in a decision tree.
    /// </summary>
    /// <typeparam name="TIn">Input</typeparam>
    /// <typeparam name="TOut">Output</typeparam>
    public class DecisionResult<TIn, TOut> : Decision<TIn, TOut>
    {
        /// <summary>
        /// A factory that creates the result
        /// </summary>
        public Expression<Func<TIn, TOut>> CreateResult { get; set; }

        /// <inheritdoc />
        public override TOut Evaluate(TIn input)
        {
            AssertCanEvaluate();

            var createResult = CreateResult.Compile();
            return createResult.Invoke(input);
        }

        /// <inheritdoc />
        public override DecisionResultWithPath<TOut> EvaluateWithPath(TIn input, DecisionPath decisionPath)
        {
            var output = Evaluate(input);
            return new DecisionResultWithPath<TOut>(output, decisionPath);
        }

        /// <inheritdoc />
        public override void Accept(IDecisionVisitor<TIn, TOut> visitor)
        {
            visitor.Visit(this);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return CreateResult.ToString();
        }

        private void AssertCanEvaluate()
        {
            if (CreateResult == null)
                throw new DecisionException($"'{nameof(CreateResult)}' cannot be null");
        }
    }
}