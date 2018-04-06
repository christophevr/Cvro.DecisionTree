namespace DecisionTree.Visualizer
{
    /// <summary>
    /// Extensions on the <see cref="Decision{TIn,TOut}"/> class
    /// </summary>
    public static class DecisionExtensions
    {
        /// <summary>
        /// Maps a Decision Tree to a string
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="decision"></param>
        /// <returns>The string, in DOT language</returns>
        public static string RenderToString<TIn, TOut>(this Decision<TIn, TOut> decision)
        {
            var visitor = new DecisionVisualizerVisitor<TIn, TOut>();
            decision.Accept(visitor);

            return visitor.RenderToDotLanguage();
        }

        /// <summary>
        /// Maps a Decision Tree to a string, highlighting the path
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="decision"></param>
        /// <param name="decisionPath"></param>
        /// <returns>The string, in DOT language</returns>
        public static string RenderToString<TIn, TOut>(this Decision<TIn, TOut> decision, DecisionPath decisionPath)
        {
            var visitor = new DecisionPathVisualizerVisitor<TIn, TOut>(decisionPath);
            decision.Accept(visitor);

            return visitor.RenderToDotLanguage();
        }

    }
}