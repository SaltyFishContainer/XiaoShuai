using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public UnityEvent onPointerIn;
    public UnityEvent onPointerOut;
    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerIn?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerOut?.Invoke();

    }
}
