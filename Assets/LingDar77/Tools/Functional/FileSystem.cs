using UnityEditor;

namespace Lingdar77.Functional
{
#if UNITY_EDITOR
    public sealed class Script
    {
        public static bool TryGetScirptPath(System.Type scriptClassType, out string sciptPath)
        {
            var results = AssetDatabase.FindAssets(scriptClassType.Name);
            if (results.Length != 0)
            {
                sciptPath = AssetDatabase.GUIDToAssetPath(results[0]);
                return true;
            }
            sciptPath = null;
            return false;
        }
    }
#endif
}