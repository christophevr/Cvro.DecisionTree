namespace DecisionTree
{
    /// <summary>
    /// A visitor for a <see cref="Decision{TIn,TOut}"/>
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public interface IDecisionVisitor<TIn, TOut>
    {
        /// <summary>
        /// Visits a <see cref="DecisionQuery{TIn,TOut}"/>
        /// </summary>
        /// <param name="decisionQuery"></param>
        void Visit(DecisionQuery<TIn, TOut> decisionQuery);

        /// <summary>
        /// Visits a <see cref="DecisionResult{TIn,TOut}"/>
        /// </summary>
        /// <param name="decisionResult"></param>
        void Visit(DecisionResult<TIn, TOut> decisionResult);
    }
}