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
        public void RenderToString_ReturnsExpectedString()
        {
            // GIVEN
            var decisionTree = new DecisionQuery<EmployeeInfo, BonusCalculation>
            {
                Test = employee => employee.YearsEmployed < 5,
                Positive = new DecisionResult<EmployeeInfo, BonusCalculation> { CreateResult = employee => new BonusCalculation { Bonus = employee.YearsEmployed * 100 } },
                Negative = new DecisionQuery<EmployeeInfo, BonusCalculation>
                {
                    Test = employee => employee.YearsEmployed < 10,
                    Positive = new DecisionResult<EmployeeInfo, BonusCalculation> { CreateResult = employee => new BonusCalculation { Bonus = employee.YearsEmployed * 200M } },
                    Negative = new DecisionResult<EmployeeInfo, BonusCalculation> { CreateResult = employee => new BonusCalculation { Bonus = employee.YearsEmployed * 300M } }
                }
            };

            // WHEN
            var graph = decisionTree.RenderToString();

            // THEN
            graph.Should().Be(DecisionVisualizerResources.ExpectedGraph);
        }
    }
}