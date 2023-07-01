using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameController gameController;


    public void PlayNewGame()
    {
        gameController.StartPlay();
    }
    public void ContinueLastGame()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
