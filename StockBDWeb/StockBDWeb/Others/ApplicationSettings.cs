using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StockBDWeb.Others
{
    public class ApplicationSettings
    {
        public Dictionary<string, string> settings;
        string m_settingsFileName;

        public ApplicationSettings(string settingsFileName = @"c:\www\stockbd\appsettings.txt")
        {
            m_settingsFileName = settingsFileName;
            settings = new Dictionary<string, string>();

            if (File.Exists(settingsFileName))
            {
                string[] lines = System.IO.File.ReadAllLines(settingsFileName);
                foreach (var line in lines)
                {
                    if (line.StartsWith("//")) // a comment
                        continue;
                    var parts = line.Split(new[] { '=' });
                    settings.Add(parts[0].Trim(), parts[1].Trim());
                }
            }
        }
        //save the settings
        public void SaveSettings()
        {
            int sCount = settings.Count;
            string[] lines = new string[sCount];
            int i = 0;
            foreach (KeyValuePair<string, string> entry in settings)
            {
                lines[i] = entry.Key + "=" + entry.Value;
            }
            if (File.Exists(m_settingsFileName))
                File.Delete(m_settingsFileName);

            System.IO.File.WriteAllLines(m_settingsFileName, lines);
        }

        //dispose things
        public void Dispose()
        {
            this.Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            SaveSettings();
            this.Dispose(disposing);

        }

    }
}