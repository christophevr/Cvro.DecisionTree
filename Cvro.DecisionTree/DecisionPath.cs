using System.Collections.Generic;

namespace DecisionTree
{
    public class DecisionPath
    {
        private readonly IList<bool> _path = new List<bool>();

        public IEnumerable<bool> Path => _path;

        public void AddStep(bool step)
        {
            _path.Add(step);
        }
    }
}