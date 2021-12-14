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

        class Styles
        {
            public static GUIContent installPathString = new GUIContent("Install Path");
        }

        public ClangFormatSettingsProvider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            m_ClangFormatSettings = ClangFormatSettings.GetSerializedSettings();
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(m_ClangFormatSettings.FindProperty("m_InstallPath"), Styles.installPathString);
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
