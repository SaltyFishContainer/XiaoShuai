using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private ToggleHide toggleHide;

    private void Awake()
    {

        videoPlayer.loopPointReached += _ =>
        {
            toggleHide.PerformHide();
        };

    }
    public void StartPlay(bool state)
    {
        if (state)
        {
            videoPlayer.time = 0;
            videoPlayer.Play();
        }
    }
}
