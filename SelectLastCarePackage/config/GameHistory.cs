using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;

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

        public void AddHistory(int cycle, CarePackageInfo carePackage)
        {
            Histories[cycle] = new CarePackageInfo(carePackage.id, carePackage.quantity, null);
        }

        [CanBeNull]
        public CarePackageInfo GetHistory(int cycle)
        {
            var obj = Histories.Where(pair => cycle > pair.Key)
                .OrderByDescending(pair => pair.Key).FirstOrDefault().Value;
            if (obj == null) return null;
            switch (obj)
            {
                case JObject j:
                    return new CarePackageInfo(
                        j.GetValue("id").Value<string>(),
                        j.GetValue("quantity").Value<float>(),
                        null
                    );
                case CarePackageInfo info:
                    return info;
                default:
                    return null;
            }
        }

        public void ShrinkHistory(int cycle)
        {
            foreach (var keyValuePair in Histories.Where(pair => cycle > pair.Key)
                .OrderByDescending(pair => pair.Key)
                .Skip(10))
            {
                Histories.Remove(keyValuePair.Key);
            }
        }
    }
}