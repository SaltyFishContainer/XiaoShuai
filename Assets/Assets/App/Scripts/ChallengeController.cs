using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Challenge
{
    public Transform activeView;
    public Transform discativeView;
    public Transform acitiveDescp;
    public Image activeImage;
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
                challenge.activeImage.color = new Color(challenge.activeImage.color.r, challenge.activeImage.color.g, challenge.activeImage.color.b, challenge.node.isReached ? 0.6274f : 0.1568f);
            }
        }
    }
}
