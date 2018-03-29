namespace DecisionTree
{
    public class DecisionResultWithPath<TOut>
    {
        public DecisionResultWithPath(TOut result, DecisionPath decisionPath)
        {
            Result = result;
            DecisionPath = decisionPath;
        }

        public TOut Result { get; }
        public DecisionPath DecisionPath { get; }
    }
}