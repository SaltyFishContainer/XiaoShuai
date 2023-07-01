using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Challenge
{
    public Transform activeView;
    public Transform discativeView;
    public Transform acitiveDescp;
    public GameNode node;
}
public class ChallengeController : MonoBehaviour
{
    [SerializeField] private Challenge[] challenges;

    public void Refresh(bool state)
    {
        if (state)
        {
            foreach (var challenge in challenges)
            {
                challenge.activeView.gameObject.SetActive(challenge.node.isReached);
                challenge.discativeView.gameObject.SetActive(!challenge.node.isReached);
                challenge.acitiveDescp.gameObject.SetActive(challenge.node.isReached);
            }
        }
    }
}
