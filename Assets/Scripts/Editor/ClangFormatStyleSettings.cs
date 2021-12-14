using UnityEngine;
using UnityEditor;
using System.IO;

namespace kagekirin.clangformat
{
    public class ClangFormatStyleSettings : ScriptableObject
    {
        public const string k_SettingsPath = "Assets/Editor/ClangFormatStyleSettings.asset";

        [SerializeField]
        public string m_Style = "file";

        [SerializeField]
        public string m_FallbackStyle = @"
            BasedOnStyle: Microsoft
            BreakBeforeBraces: Allman
            IndentWidth: 4
            TabWidth: 4
            UseTab: Never
            KeepEmptyLinesAtTheStartOfBlocks: false
            MaxEmptyLinesToKeep: 2";

        internal static ClangFormatStyleSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<ClangFormatStyleSettings>(k_SettingsPath);

            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<ClangFormatStyleSettings>();


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
