using System.Collections.Generic;
using System.Linq;

namespace undancer.SelectLastCarePackage.config
{
    public class GameHistory
    {
        private Dictionary<int, object> _histories = new Dictionary<int, object>();
        public string BaseName { get; set; }

        public Dictionary<int, object> Histories
        {
            get => _histories;
            set => _histories = value;
        }

        public void AddHistory(int cycle, object carePackage)
        {
            Histories[cycle] = carePackage;
        }

        public object GetHistory(int cycle)
        {
            var value =
                Histories.Where(pair => cycle > pair.Key)
                    .OrderByDescending(pair => pair.Key)
                    .FirstOrDefault()
                    .Value;
            return value;
        }
    }
}