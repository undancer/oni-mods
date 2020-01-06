using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Klei;
using Newtonsoft.Json.Linq;
using Steamworks;
using undancer.Commons;

namespace undancer.SelectLastCarePackage.config
{
    public class GameHistory
    {
        private Dictionary<int, CarePackage> _histories = new Dictionary<int, CarePackage>();
        public string BaseName { get; set; }
        
        public string Version { get; set; }

        public Dictionary<int, CarePackage> Histories
        {
            get => _histories;
            set => _histories = value;
        }

        public void AddHistory(int cycle, CarePackageInfo carePackage)
        {
            if (carePackage != null)
            {
                Version = ModVersion.Version;
                Histories[cycle] = new CarePackage(carePackage.id, carePackage.quantity);
            }
        }

        [CanBeNull]
        public CarePackageInfo GetHistory(int cycle)
        {
            object obj = null;
            if (Histories != null && Histories.Count > 0)
            {
                foreach (var pair in Histories)
                {
                    Debug.Log($"{pair.Key} -> {pair.Value.id} {pair.Value.quantity}");
                }

                try
                {
                    obj = Histories
                        .Where(pair => cycle > pair.Key)
                        .OrderByDescending(pair => pair.Key)
                        .First()
                        .Value;
                    if (obj != null)
                    {
                        
                    }
                }
                catch (Exception e)
                {
                    obj = null;
                    Debug.LogException(e);
                }
            }

            if (obj == null) return null;
            switch (obj)
            {
                case CarePackage package:
                    return Assets.GetPrefab(package.id.ToTag()) != null ? new CarePackageInfo(package.id, package.quantity, null) : null;
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