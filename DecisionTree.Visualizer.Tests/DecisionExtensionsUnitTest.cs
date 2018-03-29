using System;
using System.Linq.Expressions;
using DecisionTree.Tests;
using FluentAssertions;
using NUnit.Framework;

namespace DecisionTree.Visualizer.Tests
{
    [TestFixture]
    public class DecisionExtensionsUnitTest
    {
        [Test]
        public void RenderToString_ReturnsNotEmptyString()
        {
            // GIVEN
            var decisionTree = new DecisionBuilder<EmployeeInfo, BonusCalculation>()
                .WithTest(employee => employee.YearsEmployed < 5)
                .WithPositiveResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 100 })
                .WithNegativeQuery(negativeQuery => negativeQuery
                    .WithTest(employee => employee.YearsEmployed < 10)
                    .WithPositiveResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 200M })
                    .WithNegativeResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 300M }))
                .Build();

            // WHEN
            var graph = decisionTree.RenderToString();

            // THEN
            graph.Should().NotBeNullOrEmpty();
        }


        [Test]
        public void RenderToString_WithDecisionPath_ReturnsNotEmptyString()
        {
            // GIVEN
            var path = new DecisionPath();
            path.AddStep(false);
            path.AddStep(true);

            var decisionTree = new DecisionBuilder<EmployeeInfo, BonusCalculation>()
                .WithTest(employee => employee.YearsEmployed < 5)
                .WithPositiveResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 100 })
                .WithNegativeQuery(negativeQuery => negativeQuery
                    .WithTest(employee => employee.YearsEmployed < 10)
                    .WithPositiveResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 200M })
                    .WithNegativeResult(employee => new BonusCalculation { Bonus = employee.YearsEmployed * 300M }))
                .Build();

            // WHEN
            var graph = decisionTree.RenderToString(path);

            // THEN
            graph.Should().NotBeNullOrEmpty();
        }
    }
}