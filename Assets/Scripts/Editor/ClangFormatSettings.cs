using UnityEngine;
using UnityEditor;
using System.IO;

namespace kagekirin.clangformat
{
    public class ClangFormatSettings : ScriptableObject
    {
        public const string k_SettingsPath = "Assets/Editor/User/ClangFormatSettings.asset";

        [SerializeField]
        public string m_InstallPath;

        internal static ClangFormatSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<ClangFormatSettings>(k_SettingsPath);

            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<ClangFormatSettings>();
#if UNITY_EDITOR_WIN
                settings.m_InstallPath = @"clang-format.exe";
#elif UNITY_EDITOR_OSX
                settings.m_InstallPath = @"/usr/local/bin/clang-format";
#else // UNITY_EDITOR_LINUX
                settings.m_InstallPath = @"/usr/local/bin/clang-format";
#endif

                if (!File.Exists(k_SettingsPath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(k_SettingsPath));
                    AssetDatabase.CreateAsset(settings, k_SettingsPath);
                }
                AssetDatabase.SaveAssets();
            }

            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }
} // namespace kagekirin.clangformat
