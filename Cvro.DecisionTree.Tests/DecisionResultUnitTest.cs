using FluentAssertions;
using NUnit.Framework;

namespace DecisionTree.Tests
{
    [TestFixture]
    public class DecisionResultUnitTest
    {
        [Test]
        public void Evaluate_GivenNullCreateResult_Throws()
        {
            // GIVEN
            var decisionResult = new DecisionResult<SampleInput, SampleOutput>
            {
                CreateResult = null
            };
            var input = new SampleInput();

            // WHEN THEN
            Assert.Throws<DecisionException>(() => decisionResult.Evaluate(input), "'CreateResult' cannot be null");
        }

        [Test]
        public void Evaluate_CreatesExpectedResult()
        {
            // GIVEN
            var output = new SampleOutput();
            var decisionResult = new DecisionResult<SampleInput, SampleOutput>
            {
                CreateResult = sampleInput => output
            };
            var input = new SampleInput();

            // WHEN
            var actualOutput = decisionResult.Evaluate(input);

            // THEN
            actualOutput.Should().Be(output);
        }

        [Test]
        public void EvaluateWithPath_GivenPath_ReturnsExpectedPath()
        {
            // GIVEN
            var output = new SampleOutput();
            var decisionResult = new DecisionResult<SampleInput, SampleOutput>
            {
                CreateResult = sampleInput => output
            };
            var input = new SampleInput();
            var decisionPath = new DecisionPath();

            // WHEN
            var result = decisionResult.EvaluateWithPath(input, decisionPath);

            // THEN
            result.Item2.Should().Be(decisionPath);
        }
    }
}