using System;
using System.IO;
using System.Text;

namespace DecisionTree.Visualizer
{
    public class DecisionVisualizerVisitor<TIn, TOut> : IDecisionVisitor<TIn, TOut>
    {
        private readonly DecisionGraphBuilder<TIn, TOut> _graphBuilder = new DecisionGraphBuilder<TIn, TOut>();
        
        public void Visit(DecisionQuery<TIn, TOut> decisionQuery)
        {
            _graphBuilder.AddPositiveEdgeStatement(decisionQuery);
            decisionQuery.Positive.Accept(this);

            _graphBuilder.AddNegativeEdgeStatement(decisionQuery);
            decisionQuery.Negative.Accept(this);
        }

        public void Visit(DecisionResult<TIn, TOut> decisionResult)
        {
            const string fillColor = "yellow";
            _graphBuilder.AddResultNodeStatement(decisionResult, fillColor);
        }


        public string RenderToGraphviz()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream, Encoding.Unicode, 1024, leaveOpen: true))
                {
                    _graphBuilder.Graph.WriteTo(streamWriter);
                }

                stream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(stream, Encoding.Unicode))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}