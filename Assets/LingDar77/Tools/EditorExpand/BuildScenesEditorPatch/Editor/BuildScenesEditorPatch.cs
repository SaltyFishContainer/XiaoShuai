using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Lingdar77.Functional;
using System;
using UnityEngine;

#if UNITY_EDITOR
namespace Lingdar77.EdtorPatches
{
    [InitializeOnLoad]
    public class BuildScenesEditorPatch
    {
        [SerializeField]
        private static EditorBuildSettingsScene[] settingsScenes;
        private static string writtenPath;
        private static List<string> scenes;
        private static bool finishedUpdate = false;
        private static bool isListenUpdate = false;

        static BuildScenesEditorPatch()
        {
            if (Script.TryGetScirptPath(typeof(BuildScenesEditorPatch), out var path))
            {
                writtenPath = Path.Combine(path + @"/", "GameScenes.cs");
            }
            settingsScenes = EditorBuildSettings.scenes;
            EditorBuildSettings.sceneListChanged += () => settingsScenes = EditorBuildSettings.scenes;
            EditorBuildSettings.sceneListChanged += Flow.Debounce(BuildSceneNames, 1000);

        }

        private static void UpdateAssets()
        {
            if (finishedUpdate)
            {
                AssetDatabase.Refresh();
                finishedUpdate = false;
                isListenUpdate = false;
                EditorApplication.update -= UpdateAssets;
            }
        }

        [MenuItem("File/Build Scene Names &^b", false, 210)]
        public static void BuildSceneNames()
        {
            if (!isListenUpdate)
            {
                isListenUpdate = true;
                EditorApplication.update += UpdateAssets;
            }

            scenes = new List<string>();

            foreach (var scene in settingsScenes)
            {
                if (scene.enabled)
                {
                    scenes.Add(Path.GetFileNameWithoutExtension(scene.path));
                }
            }

            if (scenes.Count != 0 && writtenPath != null)
            {

                var f = File.CreateText(writtenPath);
                f.WriteLine("namespace Lingdar77.Functional");
                f.WriteLine("{");

                f.WriteLine("    public class GameScenes");
                f.WriteLine("    {");
                f.WriteLine("        public static readonly string[] names;");

                for (var i = 0; i != scenes.Count; ++i)
                {
                    var scene = scenes[i];
                    var name = "";
                    var words = scene.ToUpper().Split(' ');
                    foreach (var word in words)
                    {
                        name += word + '_';
                    }
                    name = name.Substring(0, name.Length - 1);

                    f.WriteLine("        public static readonly string " + name + " = \"" + scene + "\";");
                    f.WriteLine("        public static readonly uint " + name + "_INDEX = " + i + ";");
                }
                f.WriteLine("        static GameScenes()");
                f.WriteLine("        {");
                f.WriteLine("            names = new string[" + scenes.Count + "];");
                for (var i = 0; i != scenes.Count; ++i)
                {
                    var scene = scenes[i];
                    f.WriteLine("            names[" + i + "] = \"" + scene + "\";");
                }
                f.WriteLine("        }");

                f.WriteLine("    }");
                f.WriteLine("}");

                f.Close();
                Debug.Log("Generated " + scenes.Count + " Scene Names.");
                finishedUpdate = true;
            }
        }
    }
}

#endif