using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace ProjectInitializer
{

    public class CreateFoldersWindow : EditorWindow
    {

        private FolderStructure folderStructure;
        private Vector2 scrollPos = new Vector2();

        [MenuItem("Tools/Project Initializer/Create Folders &#f")]
        private static void SetUpFolders()
        {
            CreateFoldersWindow window = ScriptableObject.CreateInstance<CreateFoldersWindow>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 750);
            window.ShowAuxWindow();
        }

        private static void CreateAllFolders(FolderStructure folderStructure)
        {
            folderStructure.folders.ForEach(folder =>
            {
                if (!Directory.Exists("Assets/" + folder.name))
                {
                    Directory.CreateDirectory("Assets/" + folder.name);
                }
                folder.subFolders.ForEach(subFolder =>
                {
                    if (!Directory.Exists("Assets/" + folder.name + "/" + subFolder))
                    {
                        Directory.CreateDirectory("Assets/" + folder.name + "/" + subFolder);
                    }
                });
            });

            AssetDatabase.Refresh();
        }


        void OnGUI()
        {
            EditorGUILayout.LabelField("Select folder structure");
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(0));

            folderStructure = EditorGUILayout.ObjectField(folderStructure, typeof(FolderStructure), false) as FolderStructure;
            if (folderStructure)
            {
                var editor = Editor.CreateEditor(folderStructure);
                editor.OnInspectorGUI();
                InspectorElement.FillDefaultInspector(rootVisualElement, new SerializedObject(folderStructure), editor);
            }

            EditorGUILayout.EndScrollView();
            GUILayout.Space(30);
            if (folderStructure && GUILayout.Button("Generate"))
            {
                CreateAllFolders(folderStructure);
                this.Close();
            }
        }
    }
}