using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PassiveTreeControler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Vector2 NodeSize = new Vector2(320, 100);
    private bool isRightButtonPressing = false;
    private Vector3 lastMousePos;
    private PassiveTreeManager manager;


    private void Awake()
    {
        manager = GetComponent<PassiveTreeManager>();
        manager.onRedraw += () =>
        {
            var canvas = transform.parent.GetComponent<RectTransform>();
            GetComponent<RectTransform>().sizeDelta = (manager.GetGraphSize()) / canvas.lossyScale + NodeSize;
        };
    }
    public void OnPointerUp(PointerEventData e)
    {
        if (e.button == PointerEventData.InputButton.Right)
        {
            isRightButtonPressing = false;
        }
    }

    public void OnPointerDown(PointerEventData e)
    {
        if (e.button == PointerEventData.InputButton.Right)
        {
            isRightButtonPressing = true;
            lastMousePos = e.position;
        }
    }

    void LateUpdate()
    {
        if (isRightButtonPressing)
        {
            //TODO Clamp Move && Scale
            var deltaPos = Input.mousePosition - lastMousePos;
            transform.localPosition += deltaPos;
            lastMousePos = Input.mousePosition;
            transform.localScale += new Vector3(Input.mouseScrollDelta.y * .05f, Input.mouseScrollDelta.y * .05f);
        }
    }
}
