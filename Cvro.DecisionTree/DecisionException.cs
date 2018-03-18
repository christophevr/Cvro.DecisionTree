using System;

namespace Cvro.DecisionTree
{
    public class DecisionException : Exception
    {
        public DecisionException()
        {
        }

        public DecisionException(string message) : base(message)
        {
        }

        public DecisionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}