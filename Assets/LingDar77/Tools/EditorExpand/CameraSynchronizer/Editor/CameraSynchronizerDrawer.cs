using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ShowAsFunctionAttribute))]
public class CameraSynchronizerDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var prop = attribute as ShowAsFunctionAttribute;

        if (GUI.Button(position, prop.Name))
        {
            var parent = property.serializedObject.targetObject;
            var type = parent.GetType();
            var excute = type.GetMethod(prop.Excute);
            object[] o = { };
            excute.Invoke(parent, o);
        }
    }
}
#endif