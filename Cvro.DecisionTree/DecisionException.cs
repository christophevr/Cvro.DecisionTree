using System;

namespace DecisionTree
{
    /// <inheritdoc />
    /// <summary>
    /// An exception thrown by evaluating a <see cref="DecisionException"/>
    /// </summary>
    public class DecisionException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of a <see cref="DecisionException" />
        /// </summary>
        public DecisionException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of a <see cref="T:DecisionTree.DecisionException" /> with the specified message
        /// </summary>
        /// <param name="message"></param>
        public DecisionException(string message) 
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of a <see cref="T:DecisionTree.DecisionException" /> with the specified message and innerException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DecisionException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}