using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressTreeNodeComponent : MonoBehaviour
{
    [HideInInspector] public GameNode node;
    [HideInInspector] public GameController gameController;
    [HideInInspector] public ProgressTreeGraphController graphController;
    [SerializeField] private TextMeshProUGUI text;
    private ToggleHide toggleHide;
    public void Confirm()
    {
        text.text = node.overall;
        toggleHide = graphController.GetComponent<ToggleHide>();
    }

    public void Skip2CurrentNode()
    {
        gameController.PlayAt(node);
        toggleHide.PerformToggleHide();
    }
}
