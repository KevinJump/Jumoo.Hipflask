using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO; 
using System.IO.Compression;
using System.Net;

using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Core.Logging; 


namespace Jumoo.Hipflask
{
    public class Hipflask
    {
        private string _hipflaskfolder;
        private string _umbracoRoot;
        private HipFlaskSettings _settings;

        public Hipflask()
        {
            _umbracoRoot = IOHelper.MapPath("~/");
            _hipflaskfolder = Path.Combine(_umbracoRoot, "App_Data/temp/hipflask");

            if (!Directory.Exists(_hipflaskfolder))
                Directory.CreateDirectory(_hipflaskfolder);

            _settings = new HipFlaskSettings();
        }

        public void Download(string name)
        {
            switch (_settings.GetType(name))
            {
                case "zip":
                    GetZip(name);
                    break;
                case "file":
                    GetFile(name);
                    break;
            }
        }

        // download and extract a zip file...
        public void GetZip(string name)
        {
            string zipLocation = _settings.GetHipsterURL(name);
            string zipFile = Path.Combine(_hipflaskfolder, name) + ".zip";
            GetFile(zipLocation, zipFile);
        }

        public void GetFile(string name)
        {
            string location = _settings.GetHipsterURL(name);
            string filepath = Path.Combine(_hipflaskfolder, name);
            if (Directory.Exists(filepath))
            {
                Directory.Delete(filepath, true);
            }
            Directory.CreateDirectory(filepath);

            GetFile(location, filepath + "\\" + name + ".temp");
        }

        public void GetFile(string location, string dest)
        {
            if (File.Exists(dest))
                File.Delete(dest);

            Console.WriteLine("Saving {0} to {1}", location, dest);

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(location, dest);

                Console.WriteLine("Downloaded");
            }
        }


        public void Unpack(string name)
        {
            if (_settings.GetType(name) == "zip")
            {

                string zipfile = Path.Combine(_hipflaskfolder, name) + ".zip";
                // now unzip..
                string unzipFolder = Path.Combine(_hipflaskfolder, name);

                if (Directory.Exists(unzipFolder))
                    Directory.Delete(unzipFolder, true);

                ZipFile.ExtractToDirectory(zipfile, unzipFolder);
                Console.WriteLine("Unzipped to {0}", unzipFolder);
            }
        }

        public void Install(string name)
        {
            InstallFolders(name);
            InstallFiles(name);
        }

        private void InstallFolders(string name)
        {
            string unzipFolder = Path.Combine(_hipflaskfolder, name);

            Dictionary<string, string> foldermap = _settings.GetFolders(name);

            foreach (KeyValuePair<string, string> map in foldermap)
            {
                string source = Path.Combine(unzipFolder, map.Key);
                string dest = Path.Combine(_umbracoRoot, map.Value);

                Console.WriteLine("Directory {0} to {1}", source, dest);
                CopyDirectory(source, dest);

            }

        }

        private void InstallFiles(string name)
        {
            string sourceFolder = Path.Combine(_hipflaskfolder, name);


            Dictionary<string, string> filemap = _settings.GetFiles(name);

            foreach (KeyValuePair<string, string> map in filemap)
            {
                Console.WriteLine("File: {0} {1}", map.Key, map.Value);
                string source = Path.Combine(sourceFolder, map.Key);
                string dest = Path.Combine(_umbracoRoot, map.Value);
                File.Copy(source, dest, true);
            }

        }

        private void CopyDirectory(string source, string destination)
        {
            if (!Directory.Exists(destination))
                Directory.CreateDirectory(destination);

            foreach (string file in Directory.GetFiles(source))
            {
                string destFile = Path.Combine(destination, Path.GetFileName(file));
                // Console.WriteLine("Copy {0}, {1}", file, destFile); 
                File.Copy(file, destFile, true);
            }

            // now recurse in..
            foreach (string dir in Directory.GetDirectories(source))
            {
                string destDir = Path.Combine(destination, Path.GetFileName(dir));
                CopyDirectory(dir, destDir);
            }
        }


    }
}