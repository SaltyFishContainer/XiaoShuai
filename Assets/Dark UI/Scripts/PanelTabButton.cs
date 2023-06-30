using UnityEngine;
using UnityEngine.EventSystems;

namespace Michsky.UI.Dark
{
    public class PanelTabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        Animator buttonAnimator;

        void Start()
        {
            buttonAnimator = gameObject.GetComponent<Animator>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover to Pressed"))
                buttonAnimator.Play("Normal to Hover");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!buttonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hover to Pressed"))
                buttonAnimator.Play("Hover to Normal");
        }
    }
}
