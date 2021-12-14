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
            if (assetPath.EndsWith(".cs"))
            {
                var fullPath = Path.Combine(RootPath, assetPath);
                // Debug.Log($"clang-formatting: {fullPath}");

                try
                {
                    using (var process = new System.Diagnostics.Process())
                    {
                        process.StartInfo.UseShellExecute = true;
#if UNITY_EDITOR_WIN
                        process.StartInfo.FileName = @"clang-format.exe";
#elif UNITY_EDITOR_OSX
                        process.StartInfo.FileName = @"/usr/local/bin/clang-format";
#else // UNITY_EDITOR_LINUX
                        process.StartInfo.FileName = @"/usr/local/bin/clang-format";
#endif
                        process.StartInfo.Arguments              = @"-i " + fullPath;
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
