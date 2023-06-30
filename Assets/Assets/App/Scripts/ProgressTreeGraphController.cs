using System;
using System.Collections;
using System.Collections.Generic;
using Lingdar77;
using Lingdar77.Expand;
using UnityEngine;

public class ProgressTreeGraphController : MonoBehaviour
{
    [SerializeField] private GameNode startNode;
    [SerializeField] private Transform nodePrefab;
    [SerializeField] private Transform nodeLinkPrefab;
    [SerializeField] private Transform spawnRoot;
    [SerializeField] private Vector2 nodeSize = new Vector2(100, 100);
    [SerializeField] private GameController gameController;
    public Action onGraphRedraw;

    private float xmax = float.MinValue;
    private float xmin = float.MaxValue;
    private float ymax = float.MinValue;
    private float ymin = float.MaxValue;
    private RectTransform container;
    private ObjectPool pool;
    private Vector2 rootPos;

    private void Awake()
    {
        container = spawnRoot.parent as RectTransform;
        pool = GetComponent<ObjectPool>();
        rootPos = spawnRoot.position;
        Redraw();
    }

    public void Redraw()
    {
        spawnRoot.DestroyAllChildren(obj =>
        {
            var target = (obj as GameObject);
            if (target.TryGetComponent<NodeLink>(out var link))
            {
                link.reset();
            }
            if (target.TryGetComponent<ObjectPoolChild>(out var child))
                child.CacheObject2Pool();
            else
                Destroy(obj);
        });
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
        var current = pool.GetObject(nodePrefab, parent);
        current.localPosition = new Vector3(Mathf.Cos(radians) * 200 * node.connectionSize.x, Mathf.Sin(radians) * 200 * node.connectionSize.x, 0);
        if (current.TryGetComponent<ProgressTreeNodeComponent>(out var comp))
        {
            comp.node = node;
            comp.gameController = gameController;
            comp.Confirm();
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
        var current = pool.GetObject(nodeLinkPrefab, parent) as RectTransform;

        if (parent != spawnRoot)
        {
            var rect = parent.GetComponent<RectTransform>();
            var parentLength = rect.sizeDelta.x;
            var parentRotation = Mathf.PI * rect.localRotation.z / 180;
            current.localPosition = new Vector3(Mathf.Cos(parentRotation) * parentLength, Mathf.Sin(parentRotation) * parentLength);
            current.localEulerAngles = new Vector3(0, 0, node.angle - rect.eulerAngles.z);
            current.sizeDelta = new Vector2(current.sizeDelta.x * node.connectionSize.x, current.sizeDelta.y * node.connectionSize.y);
            // current.localScale = new Vector3(node.connectionSize.x, node.connectionSize.y, 1);
        }
        else
        {
            var rect = current.GetComponent<RectTransform>();
            rect.DestroyAllChildren();
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
        return new Vector2(xmax - xmin, ymax - ymin) + nodeSize;
    }

}
