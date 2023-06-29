using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressTreeNodeComponent : MonoBehaviour
{
    [HideInInspector] public GameNode node;
    [SerializeField] private TextMeshProUGUI text;
    public void Confirm()
    {
        text.text = node.overall;
    }
}
