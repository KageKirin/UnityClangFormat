using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.IO;
using System.Collections.Generic;

namespace kagekirin.clangformat
{
    public class ClangFormatSettingsProvider : SettingsProvider
    {
        private SerializedObject m_ClangFormatSettings;
        private SerializedObject m_ClangFormatStyleSettings;

        class Styles
        {
            public static GUIContent installPathString   = new GUIContent("Install Path");
            public static GUIContent styleString         = new GUIContent("Style");
            public static GUIContent fallbackStyleString = new GUIContent("Fallback Style");
            public static GUILayoutOption[] styleOptions = new GUILayoutOption[] {
                GUILayout.Height(200),
            };
            public static GUILayoutOption[] fallbackStyleOptions = new GUILayoutOption[] {
                GUILayout.Height(200),
            };
        }

        public ClangFormatSettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            m_ClangFormatSettings      = ClangFormatSettings.GetSerializedSettings();
            m_ClangFormatStyleSettings = ClangFormatStyleSettings.GetSerializedSettings();
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(m_ClangFormatSettings.FindProperty("m_InstallPath"), Styles.installPathString);
            EditorGUILayout.PropertyField(m_ClangFormatStyleSettings.FindProperty("m_Style"), Styles.styleString, Styles.styleOptions);
            EditorGUILayout.PropertyField(m_ClangFormatStyleSettings.FindProperty("m_FallbackStyle"), Styles.fallbackStyleString, Styles.fallbackStyleOptions);
        }


        [SettingsProvider]
        public static SettingsProvider CreateClangFormatSettingsProvider()
        {
            var provider = new ClangFormatSettingsProvider("Project/Clang Format", SettingsScope.Project);

            // Automatically extract all keywords from the Styles.
            provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            return provider;
        }
    }
} // namespace kagekirin.clangformat
