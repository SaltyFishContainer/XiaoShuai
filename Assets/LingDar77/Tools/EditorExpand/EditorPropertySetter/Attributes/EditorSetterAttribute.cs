using UnityEngine;
public class EditorSetterAttribute : PropertyAttribute
{
    public string Name { get; private set; }
    public bool isDirty = false;

    public EditorSetterAttribute(string name)
    {
        this.Name = name;
    }
}