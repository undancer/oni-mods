using System;
using System.Collections.Generic;

namespace ClassLibrary2
{
    public class Config
    {
        public List<Setting> Settings { get; set; } = new List<Setting>();

        public void AddSettings(string name, float times = 1.0f)
        {
            Settings.Add(new Setting {name = name, times = times});
        }

        public float GetSettings(string name)
        {
            try
            {
                return Settings.Find(setting => setting.name.Equals(name)).times;
            }
            catch (Exception)
            {
                return 1f;
            }
        }
    }
}