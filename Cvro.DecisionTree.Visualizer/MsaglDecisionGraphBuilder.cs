using Microsoft.Msagl.Drawing;

namespace DecisionTree.Visualizer
{
    public class MsaglDecisionGraphBuilder<TIn, TOut>
    {
        public Graph Graph { get; } = new Graph();

        public void AddNegativeEdgeStatement(Decision<TIn, TOut> decision, Decision<TIn, TOut> negativeDecision, bool thickLine = false)
        {
            var fromNode = decision.ToString();
            var toNode = negativeDecision.ToString();

            var edge = Graph.AddEdge(fromNode, toNode);
            edge.Label = new Label(false.ToString());
            edge.Attr.Color = Color.Red;

            if (thickLine)
                edge.Attr.Weight = 10;
        }

        public void AddPositiveEdgeStatement(Decision<TIn, TOut> decision, Decision<TIn, TOut> positiveDecision, bool thickLine = false)
        {
            var fromNode = decision.ToString();
            var toNode = positiveDecision.ToString();

            var edge = Graph.AddEdge(fromNode, toNode);
            edge.Label = new Label(false.ToString());
            edge.Attr.Color = Color.Green;

            if (thickLine)
                edge.Attr.Weight = 10;
        }

        public void AddResultNodeStatement(DecisionResult<TIn, TOut> decision, string fillColor)
        {
            var resultNode = decision.ToString();

            var node = Graph.AddNode(resultNode);
            node.Attr.FillColor = Color.Yellow; // TODO
        }
    }
}