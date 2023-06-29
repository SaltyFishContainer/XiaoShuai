using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Action<bool> onPointerUpdate;
    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerUpdate?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onPointerUpdate?.Invoke(false);
    }

}
