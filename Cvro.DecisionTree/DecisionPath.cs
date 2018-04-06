using System.Collections.Generic;

namespace DecisionTree
{
    /// <summary>
    /// Represents the path taken when evaluating a decision tree
    /// </summary>
    public class DecisionPath
    {
        private readonly IList<bool> _path = new List<bool>();

        /// <summary>
        /// The decision tree path
        /// </summary>
        public IEnumerable<bool> Path => _path;

        /// <summary>
        /// Adds a new step to the path
        /// </summary>
        /// <param name="step"></param>
        public void AddStep(bool step)
        {
            _path.Add(step);
        }
    }
}