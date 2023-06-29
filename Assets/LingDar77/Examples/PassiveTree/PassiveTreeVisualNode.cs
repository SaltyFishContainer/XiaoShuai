using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Script Objects/PassiveTreeVisualNode")]
public class PassiveTreeVisualNode : ScriptableObject
{
    [Header("Location")]
    public PassiveTreeVisualNode parent;
    [Tooltip("In degree")]
    public float angle;
    public Vector2 connectionSize = Vector2.one;
    public Dictionary<PassiveTreeVisualNode, Transform> links = new Dictionary<PassiveTreeVisualNode, Transform>();
    [Header("Description")]
    public string titile;
    public string description;

}
