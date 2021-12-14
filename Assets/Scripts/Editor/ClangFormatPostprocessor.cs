using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Runtime.InteropServices;


namespace kagekirin.clangformat
{
    class ClangFormatPostprocessor : AssetPostprocessor
    {
        static string RootPath
        {
            get {
                return Path.GetDirectoryName(Application.dataPath);
            }
        }

        void OnPreprocessAsset()
        {
            ClangFormatSettings clangFormatSettings           = ClangFormatSettings.GetOrCreateSettings();
            ClangFormatStyleSettings clangFormatStyleSettings = ClangFormatStyleSettings.GetOrCreateSettings();

            if (assetPath.EndsWith(".cs"))
            {
                var fullPath = Path.Combine(RootPath, assetPath);
                // Debug.Log($"clang-formatting: {fullPath}");

                try
                {
                    using (var process = new System.Diagnostics.Process())
                    {
                        process.StartInfo.UseShellExecute        = true;
                        process.StartInfo.FileName               = clangFormatSettings.m_InstallPath;
                        process.StartInfo.Arguments              = @"-i ";
                        process.StartInfo.Arguments              += $"--style=\"{clangFormatStyleSettings.m_Style}\" ";
                        process.StartInfo.Arguments              += $"--fallback-style=\"{clangFormatStyleSettings.m_FallbackStyle}\" ";
                        process.StartInfo.Arguments              += fullPath;
                        process.StartInfo.CreateNoWindow         = true;
                        process.StartInfo.RedirectStandardOutput = false;

                        process.Start();
                        process.WaitForExit();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
    }
} // kagekirin.clangformat
