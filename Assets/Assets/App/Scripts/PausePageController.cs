using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePageController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private ToggleHide toggleHide;
    private void Awake()
    {
        toggleHide = GetComponent<ToggleHide>();
        toggleHide.onTogglePerformed.AddListener(ToggleHide);
    }
    private void OnDestroy()
    {
        toggleHide.onTogglePerformed.RemoveListener(ToggleHide);
    }
    public void ToggleHide(bool state)
    {
        gameController.SwitchPause();
    }
}
