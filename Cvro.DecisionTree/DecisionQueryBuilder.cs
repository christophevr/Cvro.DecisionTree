using System;
using System.Linq.Expressions;

namespace DecisionTree
{
    /// <summary>
    /// Builds a decision tree using a fluent API, starting with a <see cref="DecisionQuery{TIn,TOut}"/>
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public class DecisionQueryBuilder<TIn, TOut>
    {
        private Decision<TIn, TOut> _positiveDecision;
        private Decision<TIn, TOut> _negativeDecision;
        private Expression<Func<TIn, bool>> _test;

        /// <summary>
        /// Sets a <see cref="DecisionQuery{TIn,TOut}"/> as the positive branch
        /// </summary>
        /// <param name="positiveDecisionQuery"></param>
        /// <returns></returns>
        public DecisionQueryBuilder<TIn, TOut> WithPositiveQuery(Action<DecisionQueryBuilder<TIn, TOut>> positiveDecisionQuery)
        {
            var queryBuilder = new DecisionQueryBuilder<TIn, TOut>();
            positiveDecisionQuery.Invoke(queryBuilder);

            _positiveDecision = queryBuilder.Build();
            return this;
        }

        /// <summary>
        /// Sets a <see cref="DecisionResult{TIn,TOut}"/> as the positive branch
        /// </summary>
        /// <param name="positiveDecisionResult"></param>
        /// <returns></returns>
        public DecisionQueryBuilder<TIn, TOut> WithPositiveResult(Expression<Func<TIn, TOut>> positiveDecisionResult)
        {
            _positiveDecision = new DecisionResult<TIn, TOut> { CreateResult = positiveDecisionResult };
            return this;
        }

        /// <summary>
        /// Sets a <see cref="DecisionResult{TIn,TOut}"/> as the negative branch
        /// </summary>
        /// <param name="negativeDecisionResult"></param>
        /// <returns></returns>
        public DecisionQueryBuilder<TIn, TOut> WithNegativeResult(Expression<Func<TIn, TOut>> negativeDecisionResult)
        {
            _negativeDecision = new DecisionResult<TIn, TOut> { CreateResult = negativeDecisionResult };
            return this;
        }

        /// <summary>
        /// Sets a <see cref="DecisionQuery{TIn,TOut}"/> as the negative branch
        /// </summary>
        /// <param name="negativeDecisionQuery"></param>
        /// <returns></returns>
        public DecisionQueryBuilder<TIn, TOut> WithNegativeQuery(Action<DecisionQueryBuilder<TIn, TOut>> negativeDecisionQuery)
        {
            var queryBuilder = new DecisionQueryBuilder<TIn, TOut>();
            negativeDecisionQuery.Invoke(queryBuilder);

            _negativeDecision = queryBuilder.Build();
            return this;
        }

        /// <summary>
        /// Sets the Test for the <see cref="DecisionQuery{TIn,TOut}"/>
        /// </summary>
        /// <param name="test"></param>
        /// <returns></returns>
        public DecisionQueryBuilder<TIn, TOut> WithTest(Expression<Func<TIn, bool>> test)
        {
            _test = test;
            return this;
        }
        
        /// <summary>
        /// Builds the configured <see cref="DecisionQuery{TIn,TOut}"/>
        /// </summary>
        /// <returns></returns>
        public DecisionQuery<TIn, TOut> Build()
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