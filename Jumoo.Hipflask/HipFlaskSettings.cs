using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml;
using System.Xml.Linq;

using Umbraco.Core.IO;

using System.Net; 

namespace Jumoo.Hipflask
{
    public class HipFlaskSettings
    {
        XElement settings;

        public HipFlaskSettings()
        {
            Init(); 
        }

        private void Init()
        {
            string settingsFile = IOHelper.MapPath("~/config/hipflask.config");
            settings = XElement.Load(settingsFile);
        }

        public List<string> GetHipsters()
        {
            List<string> hipsters = new List<string>();

            foreach (XElement hipster in settings.Elements("hipster"))
            {
                hipsters.Add(hipster.Attribute("name").Value);
            }

            return hipsters;
        }

        public string GetHipsterURL(string name)
        {
            XElement hipster = settings.Elements().First(x => x.Attribute("name").Value == name);

            if (hipster != null)
            {
                return hipster.Attribute("url").Value;
            }

            return "";
        }

        public string GetType(string name)
        {
            XElement hipster = settings.Elements().First(x => x.Attribute("name").Value == name);

            if (hipster != null)
            {
                return hipster.Attribute("type").Value;
            }

            return "zip";
        }

        public Dictionary<string, string> GetFolders(string name)
        {
            Dictionary<string, string> folders = new Dictionary<string, string>();

            XElement hipster = settings.Elements().First(x => x.Attribute("name").Value == name);

            if (hipster != null && hipster.Element("folders") != null)
            {
                foreach (XElement folder in hipster.Element("folders").Elements())
                {
                    folders.Add(folder.Attribute("from").Value, folder.Attribute("to").Value);
                }
            }

            return folders;

        }

        public Dictionary<string, string> GetFiles(string name)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();

            XElement hipster = settings.Elements().First(x => x.Attribute("name").Value == name);

            if (hipster != null && hipster.Element("files") != null)
            {
                foreach (XElement eFile in hipster.Element("files").Elements())
                {
                    files.Add(eFile.Attribute("from").Value, eFile.Attribute("to").Value);
                }
            }

            return files;
        }

        public bool CheckForUpdate()
        {
            string updateUrl = settings.Attribute("update").Value;

            XElement updatefile = XElement.Load(updateUrl);

            if (updatefile.Name == "hipflask")
            {
                string currentVersion = settings.Attribute("version").Value;
                string newVersion = updatefile.Attribute("version").Value;

                if (newVersion != currentVersion)
                {
                    return true; 
                }
            }

            return false; 
        }

        public void UpdateSettingsFile()
        {
            string updateUrl = settings.Attribute("update").Value;
            string settingsFile = IOHelper.MapPath("~/config/hipflask.config");

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(updateUrl, settingsFile);
                Init(); 
            }
        }

    }
}