using UnityEngine;
using UnityEditor;

public class InitDarkUI : MonoBehaviour
{
    [InitializeOnLoad]
	public class InitOnLoad
	{
		static InitOnLoad()
		{
			if (!EditorPrefs.HasKey("DarkUI.Installed"))
			{
				EditorPrefs.SetInt("DarkUI.Installed", 1);
				EditorUtility.DisplayDialog("Hello there!", "Thank you for purchasing Dark UI.\r\rFirst of all, import TextMesh Pro from Package Manager if you haven't already.\r\rYou can contact me at isa.steam@outlook.com for support.", "Got it!");
			}
		}
	}
}