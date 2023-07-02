using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLink : MonoBehaviour
{
    private RectTransform trans;
    private Vector2 size;

    private void Awake()
    {
        trans = GetComponent<RectTransform>();
        size = trans.sizeDelta;
    }

    public void reset()
    {
        if (trans)
            trans.sizeDelta = size;
    }
}
