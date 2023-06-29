using System;
using System.Collections;
using System.Collections.Generic;
using Lingdar77;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassiveTreeVisualNodeComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private GameObject descriptionPrefab;
    public PassiveTreeVisualNode visualNode;
    private RectTransform currentTransform;
    private Type logicType;
    private static RectTransform nodeDescription;
    public bool actived = false;
    public PassiveTreeVisualNodeComponent previousNode;

    private void Start()
    {
        currentTransform = GetComponent<RectTransform>();
        if (nodeDescription == null)
        {
            nodeDescription = Instantiate(descriptionPrefab).GetComponent<RectTransform>();
            nodeDescription.gameObject.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData e)
    {
        nodeDescription.SetParent(transform);
        var description = nodeDescription.GetComponent<NodeDescription>();
        description.visualNode = visualNode;
        description.Refresh();
        nodeDescription.anchoredPosition = nodeDescription.rect.size * new Vector2(.5f, -.5f) + new Vector2(32f, -32f);
        nodeDescription.gameObject.SetActive(true);
        nodeDescription.SetParent(transform.parent);
    }

    public void OnPointerExit(PointerEventData e)
    {
        nodeDescription.gameObject.SetActive(false);
    }

}
