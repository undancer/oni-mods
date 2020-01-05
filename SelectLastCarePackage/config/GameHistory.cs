using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using undancer.Commons;

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
            if (carePackage != null)
            {
                Histories[cycle] = new CarePackageInfo(carePackage.id, carePackage.quantity, null);
            }
        }

        [CanBeNull]
        public CarePackageInfo GetHistory(int cycle)
        {
            object obj;
            try
            {
                obj = Histories.Where(pair => cycle > pair.Key)
                    .OrderByDescending(pair => pair.Key)
                    .FirstOrDefault()
                    .Value;
            }
            catch (Exception e)
            {
                ChatBotUtils.PostException($"{Assembly.GetAssembly(GetType()).GetName().Name},error1", e);
                Debug.LogError(e);
                obj = null;
            }

            if (obj == null) return null;
            switch (obj)
            {
                case JObject j:
                    try
                    {
                        return new CarePackageInfo(
                            j.GetValue("id").Value<string>(),
                            j.GetValue("quantity").Value<float>(),
                            null
                        );
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        ChatBotUtils.PostException($"{Assembly.GetAssembly(GetType()).GetName().Name},error2", e);
                        Debug.LogError(e);
                        return null;
                    }
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