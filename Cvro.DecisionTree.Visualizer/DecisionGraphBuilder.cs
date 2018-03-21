﻿using System.Collections.Generic;
using System.Collections.Immutable;
using Shields.GraphViz.Models;

namespace DecisionTree.Visualizer
{
    public class DecisionGraphBuilder<TInput, TOutput>
    {
        public Graph Graph { get; protected set; } = Graph.Directed;

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

            Graph = Graph.Add(new EdgeStatement(fromNode, toNode, properties.ToImmutableDictionary()));
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

            Graph = Graph.Add(new EdgeStatement(fromNode, toNode, properties.ToImmutableDictionary()));
        }
        
        public void AddResultNodeStatement(DecisionResult<TInput, TOutput> decision, string fillColor)
        {
            var node = decision.ToString();
            var properties = new Dictionary<Id, Id>
            {
                { "fillcolor", fillColor},
                { "style", "filled"}
            };
            Graph = Graph.Add(new NodeStatement(node, properties.ToImmutableDictionary()));
        }
    }
}