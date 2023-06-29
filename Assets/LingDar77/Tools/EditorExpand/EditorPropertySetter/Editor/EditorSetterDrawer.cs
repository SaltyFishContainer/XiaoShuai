using UnityEditor;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EditorSetterAttribute))]
public class EditorSetterDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(position, property, label, true);
        var parent = property.serializedObject.targetObject;
        var type = parent.GetType();
        var setter = attribute as EditorSetterAttribute;
        if (EditorGUI.EndChangeCheck())
        {
            //when changes detected, the property value is not updated yet, we need to notify next time
            setter.isDirty = true;
        }
        else if (setter.isDirty)
        {
            PropertyInfo pi = type.GetProperty(setter.Name);
            pi?.SetValue(parent, fieldInfo.GetValue(parent), null);
            setter.isDirty = false;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return property.isExpanded ? 20 * property.CountInProperty() : 20;
    }

}
#endif
