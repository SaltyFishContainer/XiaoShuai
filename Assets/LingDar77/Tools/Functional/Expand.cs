using UnityEngine;


namespace Lingdar77.Expand
{
    public static class GameObjectExpand
    {
        public static void DestroyAllChildren(this GameObject content)
        {
            for (var i = 0; i != content.transform.childCount; ++i)
            {
                GameObject.Destroy(content.transform.GetChild(i).gameObject);
            }
        }
        public static void DestroyAllChildren(this Transform content)
        {
            for (var i = 0; i != content.childCount; ++i)
            {
                GameObject.Destroy(content.GetChild(i).gameObject);
            }
        }
    }
}