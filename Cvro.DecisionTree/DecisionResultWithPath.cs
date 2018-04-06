namespace DecisionTree
{
    /// <summary>
    /// Encapsulates the result of a decision tree, along with the path taken to reach the result
    /// </summary>
    /// <typeparam name="TOut"></typeparam>
    public class DecisionResultWithPath<TOut>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DecisionResultWithPath{TOut}"/>
        /// </summary>
        /// <param name="result"></param>
        /// <param name="decisionPath"></param>
        public DecisionResultWithPath(TOut result, DecisionPath decisionPath)
        {
            Result = result;
            DecisionPath = decisionPath;
        }

        /// <summary>
        /// The result
        /// </summary>
        public TOut Result { get; }

        /// <summary>
        /// The decision path
        /// </summary>
        public DecisionPath DecisionPath { get; }
    }
}