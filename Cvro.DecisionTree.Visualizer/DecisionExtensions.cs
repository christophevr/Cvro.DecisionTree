using System.Drawing;
using System.IO;

namespace DecisionTree.Visualizer
{
    public static class DecisionExtensions
    {
        public static void RenderToImage<TIn, TOut>(this Decision<TIn, TOut> decision, Image image)
        {
            var visitor = new DecisionVisualizerVisitor<TIn, TOut>();
            decision.Accept(visitor);

            visitor.RenderToImage(image);
        }

        public static void RenderToImage<TIn, TOut>(this Decision<TIn, TOut> decision, Image image, DecisionPath decisionPath)
        {
            var visitor = new DecisionPathVisualizerVisitor<TIn, TOut>(decisionPath);
            decision.Accept(visitor);

            visitor.RenderToImage(image);
        }

        public static void RenderToSvg<TIn, TOut>(this Decision<TIn, TOut> decision, Stream svgStream)
        {
            var visitor = new DecisionVisualizerVisitor<TIn, TOut>();
            decision.Accept(visitor);

            visitor.RenderToSvg(svgStream);
        }

        public static void RenderToSvg<TIn, TOut>(this Decision<TIn, TOut> decision, Stream svgStream, DecisionPath decisionPath)
        {
            var visitor = new DecisionPathVisualizerVisitor<TIn, TOut>(decisionPath);
            decision.Accept(visitor);

            visitor.RenderToSvg(svgStream);
        }
    }
}