using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace DecisionTree.Tests
{
    [TestFixture]
    public class DecisionQueryUnitTest
    {
        [Test]
        public void Evaluate_GivenPositiveTest_EvaluatesPositive()
        {
            // GIVEN
            var positiveDecision = new Mock<Decision<SampleInput, SampleOutput>>();
            var negativeDecision = new Mock<Decision<SampleInput, SampleOutput>>();
            var decisionQuery = new DecisionQuery<SampleInput, SampleOutput>
            {
                Test = _ => true,
                Positive = positiveDecision.Object,
                Negative = negativeDecision.Object
            };
            var input = new SampleInput();

            // WHEN
            decisionQuery.Evaluate(input);

            // THEN
            positiveDecision.Verify(decision => decision.Evaluate(input), Times.Once);
            negativeDecision.Verify(decision => decision.Evaluate(input), Times.Never);
        }

        [Test]
        public void Evaluate_GivenNegativeTest_EvaluatesNegative()
        {
            // GIVEN
            var positiveDecision = new Mock<Decision<SampleInput, SampleOutput>>();
            var negativeDecision = new Mock<Decision<SampleInput, SampleOutput>>();
            var decisionQuery = new DecisionQuery<SampleInput, SampleOutput>
            {
                Test = _ => false,
                Positive = positiveDecision.Object,
                Negative = negativeDecision.Object
            };
            var input = new SampleInput();

            // WHEN
            decisionQuery.Evaluate(input);

            // THEN
            positiveDecision.Verify(decision => decision.Evaluate(input), Times.Never);
            negativeDecision.Verify(decision => decision.Evaluate(input), Times.Once);
        }

        [Test]
        public void Evaluate_GivenNullTest_Throws()
        {
            // GIVEN
            var positiveDecision = new Mock<Decision<SampleInput, SampleOutput>>();
            var negativeDecision = new Mock<Decision<SampleInput, SampleOutput>>();
            var decisionQuery = new DecisionQuery<SampleInput, SampleOutput>
            {
                Test = null,
                Positive = positiveDecision.Object,
                Negative = negativeDecision.Object
            };
            var input = new SampleInput();

            // WHEN THEN
            Assert.Throws<DecisionException>(() => decisionQuery.Evaluate(input), "'Test' cannot be null");
        }

        [Test]
        public void Evaluate_GivenNullPositive_Throws()
        {
            // GIVEN
            var negativeDecision = new Mock<Decision<SampleInput, SampleOutput>>();
            var decisionQuery = new DecisionQuery<SampleInput, SampleOutput>
            {
                Test = _ => true,
                Positive = null,
                Negative = negativeDecision.Object
            };
            var input = new SampleInput();

            // WHEN THEN
            Assert.Throws<DecisionException>(() => decisionQuery.Evaluate(input), "'Positive' cannot be null");
        }

        [Test]
        public void Evaluate_GivenNullNegative_Throws()
        {
            // GIVEN
            var positiveDecision = new Mock<Decision<SampleInput, SampleOutput>>();
            var decisionQuery = new DecisionQuery<SampleInput, SampleOutput>
            {
                Test = _ => true,
                Positive = positiveDecision.Object,
                Negative = null
            };
            var input = new SampleInput();

            // WHEN THEN
            Assert.Throws<DecisionException>(() => decisionQuery.Evaluate(input), "'Positive' cannot be null");
        }

        [Test]
        public void EvaluateWithPath_GivenPositiveTest_ReturnsExpectedPath()
        {
            // GIVEN
            var decisionQuery = new DecisionQuery<SampleInput, SampleOutput>
            {
                Test = _ => true,
                Positive = new DecisionResult<SampleInput, SampleOutput> { CreateResult = _ => new SampleOutput() },
                Negative = new DecisionResult<SampleInput, SampleOutput> { CreateResult = _ => new SampleOutput() }
            };
            var input = new SampleInput();

            // WHEN
            var result = decisionQuery.EvaluateWithPath(input);

            // THEN
            result.Item2.Path.Should().HaveCount(1);
            result.Item2.Path.Single().Should().BeTrue();
        }

        [Test]
        public void EvaluateWithPath_GivenNegativeTest_ReturnsExpectedPath()
        {
            // GIVEN
            var decisionQuery = new DecisionQuery<SampleInput, SampleOutput>
            {
                Test = _ => false,
                Positive = new DecisionResult<SampleInput, SampleOutput> { CreateResult = _ => new SampleOutput() },
                Negative = new DecisionResult<SampleInput, SampleOutput> { CreateResult = _ => new SampleOutput() }
            };
            var input = new SampleInput();

            // WHEN
            var result = decisionQuery.EvaluateWithPath(input);

            // THEN
            result.Item2.Path.Should().HaveCount(1);
            result.Item2.Path.Single().Should().BeFalse();
        }
    }

}
