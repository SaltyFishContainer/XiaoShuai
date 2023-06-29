using System.IO;
using UnityEditor;
using Lingdar77.Functional;
using UnityEngine;
#if UNITY_EDITOR
public class CustomCreateScriptItemsPatch
{
    private static string scriptPath;
    [MenuItem("Assets/Create/Scripts/Custom Script Object", false, 80)]
    private static void CreateCustomScriptObject()
    {
        if (scriptPath == null)
            Script.TryGetScirptPath(typeof(CustomCreateScriptItemsPatch), out scriptPath);
        var path = Path.Join(Path.GetDirectoryName(scriptPath), @"\Templates\ScriptObjectTemplate.md");
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "NewScriptObject.cs");
    }
}
#endif