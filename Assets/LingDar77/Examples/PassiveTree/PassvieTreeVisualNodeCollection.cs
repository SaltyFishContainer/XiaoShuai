using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/PassvieTreeVisualNodeCollection")]
public class PassvieTreeVisualNodeCollection : ScriptableObject
{
    public List<PassiveTreeVisualNode> collection;
}
