using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.Dark
{
    public class ButtonAnimationFix : MonoBehaviour
    {
        private Button fixButton;

        void Start()
        {
            fixButton = gameObject.GetComponent<Button>();
            fixButton.onClick.AddListener(Fix);
        }

        public void Fix()
        {
            // We need to disable and enable the object, otherwise it'll stuck on highlighted anim.
            // This 'bug' is there since Unity 5, yet no fix available from Unity
            fixButton.gameObject.SetActive(false);
            fixButton.gameObject.SetActive(true);
        }
    }
}