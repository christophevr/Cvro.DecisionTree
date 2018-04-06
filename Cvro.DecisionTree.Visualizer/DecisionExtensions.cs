namespace DecisionTree.Visualizer
{
    public static class DecisionExtensions
    {
        public static string RenderToString<TIn, TOut>(this Decision<TIn, TOut> decision)
        {
            var visitor = new DecisionVisualizerVisitor<TIn, TOut>();
            decision.Accept(visitor);

            return visitor.RenderToDotLanguage();
        }

        public static string RenderToString<TIn, TOut>(this Decision<TIn, TOut> decision, DecisionPath decisionPath)
        {
            var visitor = new DecisionPathVisualizerVisitor<TIn, TOut>(decisionPath);
            decision.Accept(visitor);

            return visitor.RenderToDotLanguage();
        }

    }
}