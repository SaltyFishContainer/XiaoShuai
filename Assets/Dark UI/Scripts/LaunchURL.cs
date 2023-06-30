using UnityEngine;

namespace Michsky.UI.Dark
{
    public class LaunchURL : MonoBehaviour
    {
        public string URL;

        public void urlLinkOrWeb()
        {
            Application.OpenURL(URL);
        }
    }
}