namespace DecisionTree
{
    public interface IDecisionVisitor<TIn, TOut>
    {
        void Visit(DecisionQuery<TIn, TOut> decisionQuery);
        void Visit(DecisionResult<TIn, TOut> decisionResult);
    }
}