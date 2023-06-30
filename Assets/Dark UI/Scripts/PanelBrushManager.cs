using UnityEngine;

namespace Michsky.UI.Dark
{
    public class PanelBrushManager : MonoBehaviour
    {
        [Header("BRUSH ANIMATION")]
        public Animator brushAnimator;

        public void BrushSplashIn()
        {
            brushAnimator.Play("Transition In");
        }

        public void BrushSplashOut()
        {
            brushAnimator.Play("Transition Out");
        }
    }
}