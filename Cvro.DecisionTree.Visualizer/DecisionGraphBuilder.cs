using System.Collections.Generic;
using System.Collections.Immutable;
using Shields.GraphViz.Models;

namespace DecisionTree.Visualizer
{
    public class DecisionGraphBuilder<TIn, TOut>
    {
        public Graph Graph { get; protected set; } = Graph.Directed;

        public void AddNegativeEdgeStatement(Decision<TIn, TOut> decision, Decision<TIn, TOut> negativeDecision, bool thickLine = false)
        {
            var fromNode = $"{decision} [{decision.GetHashCode()}]";
            var toNode = $"{negativeDecision} [{decision.GetHashCode()}]";
            var properties = new Dictionary<Id, Id>
            {
                { "label", false.ToString() },
                { "color", "red" }
            };

            if (thickLine)
                properties.Add("penwidth", "10");

            Graph = Graph.Add(new EdgeStatement(fromNode, toNode, properties.ToImmutableDictionary()));
        }
        public void AddPositiveEdgeStatement(Decision<TIn, TOut> decision, Decision<TIn, TOut> positiveDecision, bool thickLine = false)
        {
            var fromNode = $"{decision} [{decision.GetHashCode()}]";
            var toNode = $"{positiveDecision} [{decision.GetHashCode()}]";
            var properties = new Dictionary<Id, Id>
            {
                { "label", true.ToString() },
                { "color", "green" }
            };

            if (thickLine)
                properties.Add("penwidth", "10");

            Graph = Graph.Add(new EdgeStatement(fromNode, toNode, properties.ToImmutableDictionary()));
        }
        
        public void AddResultNodeStatement(DecisionResult<TIn, TOut> decision, string fillColor)
        {
            var node = $"{decision} [{decision.GetHashCode()}]";
            var properties = new Dictionary<Id, Id>
            {
                { "fillcolor", fillColor},
                { "style", "filled"}
            };
            Graph = Graph.Add(new NodeStatement(node, properties.ToImmutableDictionary()));
        }
    }
}