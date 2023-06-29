using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GraphDragger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform target;
    private bool isRightButtonPressing = false;
    private Vector3 lastMousePos;
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
            target.localPosition += deltaPos;
            lastMousePos = Input.mousePosition;
            target.localScale += new Vector3(Input.mouseScrollDelta.y * .05f, Input.mouseScrollDelta.y * .05f);
        }
    }
}
