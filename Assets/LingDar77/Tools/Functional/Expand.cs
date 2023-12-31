using System;
using System.Collections.Generic;
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
        public static void DestroyAllChildren(this GameObject content, Action<UnityEngine.Object> method)
        {
            var children = new List<Transform>();
            for (var i = 0; i != content.transform.childCount; ++i)
            {
                children.Add(content.transform.GetChild(i));
            }
            foreach(var child in children)
            {
                method(child.gameObject);
            }
        }
        public static void DestroyAllChildren(this Transform content, Action<UnityEngine.Object> method)
        {
           var children = new List<Transform>();
            for (var i = 0; i != content.childCount; ++i)
            {
                children.Add(content.GetChild(i));
            }
            foreach(var child in children)
            {
                method(child.gameObject);
            }
        }
    }
}