using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Michsky.UI.Dark
{
    public class GamepadTriggerEvent : MonoBehaviour
    {
        [Header("EVENTS")]
        public UnityEvent leftTriggerEvent;
        public UnityEvent rightTriggerEvent;
        public float cooldownTimer = 0.25f;

        [Header("INPUT")]
        public string leftTriggerInput = "Xbox Left Trigger";
        public string rightTriggerInput = "Xbox Right Trigger";

        bool canClick = true;

        void Update()
        {
            float l = Input.GetAxisRaw(leftTriggerInput);
            float r = Input.GetAxisRaw(rightTriggerInput);

            if (l == 1 && canClick == false)
                StartCoroutine("TimedEvent");

            if (r == 1 && canClick == false)
                StartCoroutine("TimedEvent");

            if (l == 1 && canClick == true)
            {
                leftTriggerEvent.Invoke();
                canClick = false;
            }

            if (r == 1 && canClick == true)
            {
                rightTriggerEvent.Invoke();
                canClick = false;
            }
        }

        IEnumerator TimedEvent()
        {
            yield return new WaitForSeconds(cooldownTimer);
            canClick = true;
            StopCoroutine("TimedEvent");
        }
    }
}