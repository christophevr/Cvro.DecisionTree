using System.IO;
using System.Text;

namespace DecisionTree.Visualizer
{
    internal class DecisionVisualizerVisitor<TIn, TOut> : IDecisionVisitor<TIn, TOut>
    {
        protected readonly DecisionGraphBuilder<TIn, TOut> GraphBuilder = new DecisionGraphBuilder<TIn, TOut>();
        
        public virtual void Visit(DecisionQuery<TIn, TOut> decisionQuery)
        {
            GraphBuilder.AddPositiveEdgeStatement(decisionQuery, decisionQuery.Positive);
            decisionQuery.Positive.Accept(this);

            GraphBuilder.AddNegativeEdgeStatement(decisionQuery, decisionQuery.Negative);
            decisionQuery.Negative.Accept(this);
        }

        public void Visit(DecisionResult<TIn, TOut> decisionResult)
        {
            const string fillColor = "yellow";
            GraphBuilder.AddResultNodeStatement(decisionResult, fillColor);
        }


        public string RenderToDotLanguage()
        {
            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream, Encoding.Unicode, 1024, leaveOpen: true))
                {
                    GraphBuilder.Graph.WriteTo(streamWriter);
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