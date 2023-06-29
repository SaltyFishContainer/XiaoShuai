using System;
using System.Collections.Generic;
using Lingdar77.Expand;
using UnityEngine;

public class PassiveTreeManager : MonoBehaviour
{
    [SerializeField]
    private Transform nodePrefab;
    [SerializeField]
    private Transform nodeLinkPrefab;
    [SerializeField]
    private PassvieTreeVisualNodeCollection visualNodeCollection;
    private Dictionary<PassiveTreeVisualNode, HashSet<PassiveTreeVisualNode>> map;
    private PassiveTreeVisualNode root;
    private float xmax = float.MinValue;
    private float xmin = float.MaxValue;
    private float ymax = float.MinValue;
    private float ymin = float.MaxValue;

    public Action onRedraw;
    private void Start()
    {
        ParseVisualNodeCollection();
        Draw();
    }

    public void Draw()
    {
        xmax = float.MinValue;
        xmin = float.MaxValue;
        ymax = float.MinValue;
        ymin = float.MaxValue;
        gameObject.DestroyAllChildren();
        DrawLine(root, transform);
        DrawNode(root, transform);
        onRedraw?.Invoke();
    }
    private void DrawNode(PassiveTreeVisualNode node, Transform parent)
    {
        var radians = Mathf.PI * node.angle / 180;
        var current = Instantiate(nodePrefab, parent);
        current.localPosition = new Vector3(Mathf.Cos(radians) * 200 * node.connectionSize.x, Mathf.Sin(radians) * 200 * node.connectionSize.x, 0);

        if (current.TryGetComponent<PassiveTreeVisualNodeComponent>(out var comp))
        {
            comp.visualNode = node;
            comp.previousNode = parent.GetComponent<PassiveTreeVisualNodeComponent>();
            if (parent == transform)
            {
                //root 
                comp.actived = true;
            }
        }
        foreach (var child in map[node])
        {
            DrawNode(child, current);
        }
        xmax = Mathf.Max(xmax, current.position.x);
        xmin = Mathf.Min(xmin, current.position.x);
        ymax = Mathf.Max(ymax, current.position.y);
        ymin = Mathf.Min(ymin, current.position.y);
        current.SetParent(transform);
    }

    private RectTransform DrawLine(PassiveTreeVisualNode node, Transform parent)
    {
        var current = Instantiate(nodeLinkPrefab, parent) as RectTransform;
        current.gameObject.name += node.angle;
        if (parent != transform)
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
        foreach (var child in map[node])
        {
            node.links[child] = DrawLine(child, current);
        }
        current.SetParent(transform);
        return current;
    }
    private void ParseVisualNodeCollection()
    {
        map = new Dictionary<PassiveTreeVisualNode, HashSet<PassiveTreeVisualNode>>();
        foreach (var node in visualNodeCollection.collection)
        {
            if (node.parent == null)
            {
                root = node;
                continue;
            }
            if (map.ContainsKey(node.parent))
            {
                map[node.parent].Add(node);
            }
            else
            {
                var set = new HashSet<PassiveTreeVisualNode>();
                map[node.parent] = set;
                set.Add(node);
            }
            if (!map.ContainsKey(node))
                map.Add(node, new HashSet<PassiveTreeVisualNode>());

        }
    }
    public Vector2 GetGraphSize()
    {
        var center = transform.Find(nodePrefab.name + "(Clone)").position;
        return new Vector2(2 * Mathf.Max(xmax - center.x, center.x - xmin), 2 * Mathf.Max(ymax - center.y, center.y - ymin));
    }
}
