using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


[System.Serializable]
public struct GameOption
{
    public string buttonTittle;
    public GameNode node;
}

[CreateAssetMenu(menuName = "Script Objects/GameNode")]
public class GameNode : ScriptableObject
{
    public VideoClip clip;
    public string description;
    public GameOption[] options;
    public bool isEnded;

}
