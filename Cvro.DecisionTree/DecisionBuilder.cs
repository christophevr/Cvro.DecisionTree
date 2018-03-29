using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    public class DecisionBuilder<TIn, TOut>
    {
        private Decision<TIn, TOut> _positiveDecision;
        private Decision<TIn, TOut> _negativeDecision;
        private Expression<Func<TIn, bool>> _test;

        public DecisionBuilder<TIn, TOut> WithPositiveQuery(Action<DecisionBuilder<TIn, TOut>> positiveDecisionQuery)
        {
            var queryBuilder = new DecisionBuilder<TIn, TOut>();
            positiveDecisionQuery.Invoke(queryBuilder);

            _positiveDecision = queryBuilder.Build();
            return this;
        }

        public DecisionBuilder<TIn, TOut> WithPositiveResult(Expression<Func<TIn, TOut>> positiveDecisionResult)
        {
            _positiveDecision = new DecisionResult<TIn, TOut> { CreateResult = positiveDecisionResult };
            return this;
        }
        public DecisionBuilder<TIn, TOut> WithNegativeResult(Expression<Func<TIn, TOut>> negativeDecisionResult)
        {
            _negativeDecision = new DecisionResult<TIn, TOut> { CreateResult = negativeDecisionResult };
            return this;
        }

        public DecisionBuilder<TIn, TOut> WithNegativeQuery(Action<DecisionBuilder<TIn, TOut>> negativeDecisionQuery)
        {
            var queryBuilder = new DecisionBuilder<TIn, TOut>();
            negativeDecisionQuery.Invoke(queryBuilder);

            _negativeDecision = queryBuilder.Build();
            return this;
        }

        public DecisionBuilder<TIn, TOut> WithTest(Expression<Func<TIn, bool>> test)
        {
            _test = test;
            return this;
        }
        
        public Decision<TIn, TOut> Build()
        {
            return new DecisionQuery<TIn, TOut>
            {
                Test = _test,
                Positive = _positiveDecision,
                Negative = _negativeDecision
            };
        }
    }
}