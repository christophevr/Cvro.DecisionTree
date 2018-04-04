using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq.Expressions;
using DecisionTree.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace DecisionTree.Visualizer.Tests
{
    [TestFixture]
    public class DecisionExtensionsUnitTest
    {
        private Decision<EmployeeInfo, BonusCalculation> _decisionTree;

        [SetUp]
        public void SetUp()
        {
            _decisionTree = new DecisionBuilder<EmployeeInfo, BonusCalculation>()
                .WithTest(employee => employee.YearsEmployed < 5)
                .WithPositiveResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 100 })
                .WithNegativeQuery(negativeQuery => negativeQuery
                    .WithTest(employee => employee.YearsEmployed < 10)
                    .WithPositiveResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 200M })
                    .WithNegativeResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 300M }))
                .Build();
        }

        [Test]
        public void RenderToSvg_ReturnsNonEmptyStream()
        {
            // GIVEN 
            using (var svgStream = new MemoryStream())
            {
                // WHEN
                _decisionTree.RenderToSvg(svgStream);

                // THEN
                svgStream.Length.Should().NotBe(0);
            }
        }

        [Test]
        public void RenderToSvg_WithDecisionPath_ReturnsNonEmptyStream()
        {
            // GIVEN
            var path = new DecisionPath();
            path.AddStep(false);
            path.AddStep(true);

            using (var svgStream = new MemoryStream())
            {
                // WHEN
                _decisionTree.RenderToSvg(svgStream);

                // THEN
                svgStream.Length.Should().NotBe(0);
            }
        }

        [Test]
        public void RenderToImage_ReturnsNonEmptyStream()
        {
            // GIVEN 
            using (var bitmap = new Bitmap(2000, 1000))
            using (var file = File.Create(@"C:\temp\foo.png"))
            {
                // WHEN
                _decisionTree.RenderToImage(bitmap);

                // THEN
                bitmap.Save(file, ImageFormat.Png);
            }
        }
    }
}