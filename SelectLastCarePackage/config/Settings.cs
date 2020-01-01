using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace undancer.SelectLastCarePackage.config
{
    public class Settings : IConfiguration
    {
        public List<GameHistory> List { get; set; } = new List<GameHistory>();

        [JsonIgnore] private int _currentIndex = -1;
        [JsonIgnore] public bool SkipFlag { get; set; }

        public void InitializeDefaultValues()
        {
        }

        private void EnsureHistoryExist()
        {
            while (true)
            {
                if (_currentIndex < 0)
                {
                    var baseName = SaveGame.Instance.BaseName;
                    _currentIndex = List.FindIndex(history => history.BaseName == baseName);
                    if (_currentIndex < 0)
                    {
                        var history = new GameHistory {BaseName = baseName};
                        List.Add(history);
                        continue;
                    }
                }

                break;
            }
        }

        public void AddHistory(object carePackage)
        {
            EnsureHistoryExist();
            var cycles = GameUtil.GetCurrentCycle();
            List[_currentIndex].AddHistory(cycles, carePackage);
        }

        public void Clean()
        {
            var baseNameList = SaveLoader.GetAllFiles()
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
            List.RemoveAll(history => !baseNameList.Contains(history.BaseName));
        }

        [CanBeNull]
        public CarePackageInfo GetHistory()
        {
            EnsureHistoryExist();
            var cycles = GameUtil.GetCurrentCycle();
            var info = List[_currentIndex].GetHistory(cycles);
            List[_currentIndex].ShrinkHistory(cycles);
            return info;
        }
    }
}