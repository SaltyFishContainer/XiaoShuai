using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

namespace Michsky.UI.Dark
{
    [RequireComponent(typeof(AudioSource))]
    public class UIElementSound : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        [Header("RESOURCES")]
        public AudioSource audioSource;
        public AudioClip hoverSound;
        public AudioClip clickSound;
        public AudioClip notificationSound;

        [Header("SETTINGS")]
        public bool enableHoverSound = true;
        public bool enableClickSound = true;

        void Start()
        {
            if (audioSource == null)
            {
                try
                {
                    audioSource = gameObject.GetComponent<AudioSource>();
                    audioSource.playOnAwake = false;
                }

                catch
                {
                    Debug.LogError("UI Element Sound - Cannot initalize AudioSource due to missing resources.", this);
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (enableHoverSound == true && audioSource != null)
                audioSource.PlayOneShot(hoverSound);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (enableClickSound == true && audioSource != null)
                audioSource.PlayOneShot(clickSound);
        }

        public void Notification()
        {
            if (audioSource != null)
                audioSource.PlayOneShot(notificationSound);
        }
    }
}