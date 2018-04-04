using System;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Shields.GraphViz.Components;

namespace DecisionTree.Visualizer
{
    public class DecisionVisualizerVisitor<TIn, TOut> : IDecisionVisitor<TIn, TOut>
    {
        protected readonly MsaglDecisionGraphBuilder<TIn, TOut> GraphBuilder = new MsaglDecisionGraphBuilder<TIn, TOut>();

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

        public void RenderToImage(Image image)
        {
            var renderer = new GraphRenderer(GraphBuilder.Graph);
            renderer.CalculateLayout();
            renderer.Render(image);
        }

        public void RenderToSvg(Stream svgStream)
        {
            var writer = new SvgGraphWriter(svgStream, GraphBuilder.Graph);
            writer.Write();
        }
    }
}