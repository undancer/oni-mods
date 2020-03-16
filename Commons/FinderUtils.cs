using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace undancer.Commons
{
    public static class FinderUtils
    {
        private static void OpenInMacFileBrowser(string path)
        {
            var macPath = path.Replace("\\", "/");
            var openInsidesOfFolder = Directory.Exists(macPath);
            macPath = Regex.Replace(macPath, "^([\"])?(?<text>.*?)([\"])?$", "\"${text}\"");
            var arguments = (openInsidesOfFolder ? "" : "-R ") + macPath;
            try
            {
                Process.Start("open", arguments);
            }
            catch (Win32Exception e)
            {
                e.HelpLink = "";
            }
        }

        private static void OpenInWinFileBrowser(string path)
        {
            var winPath = path.Replace("/", "\\");
            var openInsidesOfFolder = Directory.Exists(winPath);
            try
            {
                Process.Start("explorer.exe",
                    (openInsidesOfFolder ? "/root," : "/select,") + winPath);
            }
            catch (Win32Exception e)
            {
                e.HelpLink = "";
            }
        }

        public static void OpenInFileBrowser(string path)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.OSXPlayer:
                    OpenInMacFileBrowser(path);
                    break;
                case RuntimePlatform.WindowsPlayer:
                    OpenInWinFileBrowser(path);
                    break;
                default:
                    break;
            }
        }
    }
}