using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


[System.Serializable]
public struct GameOption
{
    public GameObject buttonPrefab;
    public GameNode node;
}

[CreateAssetMenu(menuName = "Script Objects/GameNode")]
public class GameNode : ScriptableObject
{
    [Tooltip("In degree")]
    public float angle;
    public Vector2 connectionSize = Vector2.one;
    public VideoClip clip;
    public string description;
    public string overall;
    public GameOption[] options;
    public bool isEnded;
    public bool isReached;

}
