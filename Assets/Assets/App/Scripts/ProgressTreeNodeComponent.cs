using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressTreeNodeComponent : MonoBehaviour
{
    [HideInInspector] public GameNode node;
    [HideInInspector] public GameController gameController;
    [SerializeField] private TextMeshProUGUI text;
    public void Confirm()
    {
        text.text = node.overall;
    }

    public void Skip2CurrentNode()
    {
        gameController.PlayAt(node);
    }
}
