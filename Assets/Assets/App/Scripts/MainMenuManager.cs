using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject messageBoxQuit;
    [SerializeField] private Button confirmQuitButton;
    [SerializeField] private Button cancelQuitButton;


    private void Awake()
    {
        confirmQuitButton.onClick.AddListener(() => { Application.Quit(); });
        cancelQuitButton.onClick.AddListener(() => { messageBoxQuit.SetActive(false); });
    }
    public void PlayNewGame()
    {
        gameController.StartPlay();
    }
    public void ExitGame()
    {
        messageBoxQuit.SetActive(true);
    }
}
