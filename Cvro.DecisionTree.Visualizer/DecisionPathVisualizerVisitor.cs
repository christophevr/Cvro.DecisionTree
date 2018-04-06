using System.Collections.Generic;
using System.Linq;

namespace DecisionTree.Visualizer
{
    internal class DecisionPathVisualizerVisitor<TIn, TOut> : DecisionVisualizerVisitor<TIn, TOut>
    {
        private readonly Stack<bool> _pathStack;
        private Decision<TIn, TOut> _nextDecisionInPath;

        public DecisionPathVisualizerVisitor(DecisionPath decisionPath)
        {
            var results = decisionPath.Path.Reverse();
            _pathStack = new Stack<bool>(results);
        }

        public override void Visit(DecisionQuery<TIn, TOut> decisionQuery)
        {
            if (_pathStack.Count != 0 && _nextDecisionInPath == null || _nextDecisionInPath == decisionQuery)
            {
                var isPositiveDecision = _pathStack.Pop();
                _nextDecisionInPath = isPositiveDecision ? decisionQuery.Positive : decisionQuery.Negative;

                GraphBuilder.AddPositiveEdgeStatement(decisionQuery, decisionQuery.Positive, thickLine: isPositiveDecision);
                decisionQuery.Positive.Accept(this);

                GraphBuilder.AddNegativeEdgeStatement(decisionQuery, decisionQuery.Negative, thickLine: !isPositiveDecision);
                decisionQuery.Negative.Accept(this);
            }
            else
            {
                GraphBuilder.AddPositiveEdgeStatement(decisionQuery, decisionQuery.Positive);
                decisionQuery.Positive.Accept(this);

                GraphBuilder.AddNegativeEdgeStatement(decisionQuery, decisionQuery.Negative);
                decisionQuery.Negative.Accept(this);
            }
        }
    }
}