using System.Collections.Generic;
using System.Collections.Immutable;
using Shields.GraphViz.Models;

namespace DecisionTree.Visualizer
{
    public class DecisionVisualizerVisitor<TInput, TOutput> : IDecisionVisitor<TInput, TOutput>
    {
        private readonly DecisionGraphBuilder<TInput, TOutput> _graphBuilder = new DecisionGraphBuilder<TInput, TOutput>();
        
        public void Visit(DecisionQuery<TInput, TOutput> decisionQuery)
        {
            _graphBuilder.AddPositiveEdgeStatement(decisionQuery);
            decisionQuery.Positive.Accept(this);

            _graphBuilder.AddNegativeEdgeStatement(decisionQuery);
            decisionQuery.Negative.Accept(this);
        }

        public void Visit(DecisionResult<TInput, TOutput> decisionResult)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DecisionGraphBuilder<TInput, TOutput>
    {
        private Graph _graph = Graph.Directed;

        public void AddNegativeEdgeStatement(DecisionQuery<TInput, TOutput> decision, bool thickLine = false)
        {
            var fromNode = decision.ToString();
            var toNode = decision.Negative.ToString();
            var properties = new Dictionary<Id, Id>
            {
                { "label", false.ToString() },
                { "color", "red" }
            };

            if (thickLine)
                properties.Add("penwidth", "10");

            _graph = _graph.Add(new EdgeStatement(fromNode, toNode, properties.ToImmutableDictionary()));
        }
        public void AddPositiveEdgeStatement(DecisionQuery<TInput, TOutput> decision, bool thickLine = false)
        {
            var fromNode = decision.ToString();
            var toNode = decision.Positive.ToString();
            var properties = new Dictionary<Id, Id>
            {
                { "label", true.ToString() },
                { "color", "green" }
            };

            if (thickLine)
                properties.Add("penwidth", "10");

            _graph = _graph.Add(new EdgeStatement(fromNode, toNode, properties.ToImmutableDictionary()));
        }
        
        public void AddResultNodeStatement(DecisionResult<TInput, TOutput> decision, string fillColor)
        {
            var node = decision.ToString();
            var properties = new Dictionary<Id, Id>
            {
                { "fillcolor", fillColor},
                { "style", "filled"}
            };
            _graph = _graph.Add(new NodeStatement(node, properties.ToImmutableDictionary()));
        }

    }
}