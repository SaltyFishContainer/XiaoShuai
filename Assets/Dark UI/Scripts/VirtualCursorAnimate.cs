using UnityEngine;
using UnityEngine.EventSystems;

namespace Michsky.UI.Dark
{
    public class VirtualCursorAnimate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("RESOURCES")]
        public VirtualCursor virtualCursor;

        void Start()
        {
            if (virtualCursor == null)
            {
                Debug.Log("Looking for Virtual Cursor automatically.");
                virtualCursor = GameObject.Find("Virtual Cursor").GetComponent<VirtualCursor>();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (virtualCursor != null)
                virtualCursor.AnimateCursorIn();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (virtualCursor != null)
                virtualCursor.AnimateCursorOut();
        }
    }
}