namespace DecisionTree
{
    /// <summary>
    /// An abstract base class for a decision
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public abstract class Decision<TIn, TOut>
    {
        /// <summary>
        /// Evaluates the decision
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>The output</returns>
        public abstract TOut Evaluate(TIn input);

        /// <summary>
        /// Evaluates the decision, and also returns the decision path
        /// </summary>
        /// <param name="input">The input</param>
        /// <param name="decisionPath">The decision path</param>
        /// <returns>The output, accompanied by the decision path</returns>
        public abstract DecisionResultWithPath<TOut> EvaluateWithPath(TIn input, DecisionPath decisionPath);

        /// <summary>
        /// Evaluates the decision, and also returns the decision path
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>The output, accompanied by the decision path</returns>
        public virtual DecisionResultWithPath<TOut> EvaluateWithPath(TIn input)
        {
            return EvaluateWithPath(input, new DecisionPath());
        }

        /// <summary>
        /// Accepts a visitor
        /// </summary>
        /// <param name="visitor">The visitor</param>
        public abstract void Accept(IDecisionVisitor<TIn, TOut> visitor);

        /// <summary>
        /// Returns the decision in a readable format
        /// </summary>
        /// <returns></returns>
        public abstract override string ToString();
    }
}
