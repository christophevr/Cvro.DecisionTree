﻿using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    public class DecisionResult<TIn, TOut> : Decision<TIn, TOut>
    {
        public Expression<Func<TIn, TOut>> CreateResult { get; set; }

        public override TOut Evaluate(TIn input)
        {
            AssertCanEvaluate();

            var createResult = CreateResult.Compile();
            return createResult.Invoke(input);
        }

        public override DecisionResultWithPath<TOut> EvaluateWithPath(TIn input, DecisionPath decisionPath)
        {
            var output = Evaluate(input);
            return new DecisionResultWithPath<TOut>(output, decisionPath);
        }

        public override void Accept(IDecisionVisitor<TIn, TOut> visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"{CreateResult} [{GetHashCode()}]";
        }

        private void AssertCanEvaluate()
        {
            if (CreateResult == null)
                throw new DecisionException($"'{nameof(CreateResult)}' cannot be null");
        }
    }
}