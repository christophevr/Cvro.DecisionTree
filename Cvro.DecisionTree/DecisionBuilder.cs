using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    public class DecisionQueryBuilder<TIn, TOut>
    {
        private Decision<TIn, TOut> _positiveDecision;
        private Decision<TIn, TOut> _negativeDecision;
        private Expression<Func<TIn, bool>> _test;

        public DecisionQueryBuilder<TIn, TOut> WithPositiveQuery(Action<DecisionQueryBuilder<TIn, TOut>> positiveDecisionQuery)
        {
            var queryBuilder = new DecisionQueryBuilder<TIn, TOut>();
            positiveDecisionQuery.Invoke(queryBuilder);

            _positiveDecision = queryBuilder.Build();
            return this;
        }

        public DecisionQueryBuilder<TIn, TOut> WithPositiveResult(Expression<Func<TIn, TOut>> positiveDecisionResult)
        {
            _positiveDecision = new DecisionResult<TIn, TOut> { CreateResult = positiveDecisionResult };
            return this;
        }
        public DecisionQueryBuilder<TIn, TOut> WithNegativeResult(Expression<Func<TIn, TOut>> negativeDecisionResult)
        {
            _negativeDecision = new DecisionResult<TIn, TOut> { CreateResult = negativeDecisionResult };
            return this;
        }

        public DecisionQueryBuilder<TIn, TOut> WithNegativeQuery(Action<DecisionQueryBuilder<TIn, TOut>> negativeDecisionQuery)
        {
            var queryBuilder = new DecisionQueryBuilder<TIn, TOut>();
            negativeDecisionQuery.Invoke(queryBuilder);

            _negativeDecision = queryBuilder.Build();
            return this;
        }

        public DecisionQueryBuilder<TIn, TOut> WithTest(Expression<Func<TIn, bool>> test)
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