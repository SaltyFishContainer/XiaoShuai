using System;
using System.Collections;
using System.Collections.Generic;
using Lingdar77.Expand;
using UnityEngine;

public class ProgressTreeGraphController : MonoBehaviour
{
    [SerializeField] private GameNode startNode;
    [SerializeField] private Transform nodePrefab;
    [SerializeField] private Transform nodeLinkPrefab;
    [SerializeField] private Transform spawnRoot;

    public Action onGraphRedraw;

    private float xmax = float.MinValue;
    private float xmin = float.MaxValue;
    private float ymax = float.MinValue;
    private float ymin = float.MaxValue;

    private void Awake()
    {
        Redraw();
    }

    public void Redraw()
    {
        spawnRoot.DestroyAllChildren();
        DrawLine(startNode, spawnRoot);
        DrawNode(startNode, spawnRoot);
        onGraphRedraw?.Invoke();
    }

    private void DrawNode(GameNode node, Transform parent)
    {
        if (!node.isReached)
        {
            return;
        }
        var radians = Mathf.PI * node.angle / 180;
        var current = Instantiate(nodePrefab, parent);
        current.localPosition = new Vector3(Mathf.Cos(radians) * 200 * node.connectionSize.x, Mathf.Sin(radians) * 200 * node.connectionSize.x, 0);
        if (current.TryGetComponent<ProgressTreeNodeComponent>(out var comp))
        {
            comp.node = node;
        }
        foreach (var option in node.options)
        {
            DrawNode(option.node, current);
        }
        xmax = Mathf.Max(xmax, current.position.x);
        xmin = Mathf.Min(xmin, current.position.x);
        ymax = Mathf.Max(ymax, current.position.y);
        ymin = Mathf.Min(ymin, current.position.y);
        current.SetParent(spawnRoot);
    }
    private RectTransform DrawLine(GameNode node, Transform parent)
    {
        if (!node.isReached)
        {
            return null;
        }
        var current = Instantiate(nodeLinkPrefab, parent) as RectTransform;

        if (parent != spawnRoot)
        {
            var rect = parent.GetComponent<RectTransform>();
            var parentLength = rect.sizeDelta.x;
            var parentRotation = Mathf.PI * rect.localRotation.z / 180;
            current.localPosition = new Vector3(Mathf.Cos(parentRotation) * parentLength, Mathf.Sin(parentRotation) * parentLength);
            current.localEulerAngles = new Vector3(0, 0, node.angle - rect.eulerAngles.z);
            current.sizeDelta = new Vector2(current.sizeDelta.x * node.connectionSize.x, current.sizeDelta.y * node.connectionSize.y);
        }
        else
        {
            var rect = current.GetComponent<RectTransform>();
            rect.gameObject.DestroyAllChildren();
            rect.sizeDelta = Vector2.zero;
            rect.localEulerAngles = new Vector3(0, 0, node.angle);

        }
        foreach (var option in node.options)
        {
            DrawLine(option.node, current);
        }
        current.SetParent(spawnRoot);
        return current;
    }
    public Vector2 GetGraphSize()
    {
        var center = transform.Find(nodePrefab.name + "(Clone)").position;
        return new Vector2(2 * Mathf.Max(xmax - center.x, center.x - xmin), 2 * Mathf.Max(ymax - center.y, center.y - ymin));
    }
}
