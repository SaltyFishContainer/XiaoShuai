using UnityEngine;
#if UNITY_EDITOR
public class ShowAsFunctionAttribute : PropertyAttribute
{
    public string Name { get; private set; }
    public string Excute { get; private set; }

    public ShowAsFunctionAttribute(string displayName, string functionName)
    {
        this.Name = displayName;
        this.Excute = functionName;
    }
}
#endif