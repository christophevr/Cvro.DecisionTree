namespace Cvro.DecisionTree
{
    public interface IDecisionVisitor<TInput, TOutput>
    {
        void Visit(DecisionQuery<TInput, TOutput> decisionQuery);
        void Visit(DecisionResult<TInput, TOutput> decisionResult);
    }
}