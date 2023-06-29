using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.Dark
{
    public class SliderManager : MonoBehaviour
    {
        [Header("TEXTS")]
        public TextMeshProUGUI valueText;

        [Header("SAVING")]
        public bool enableSaving = false;
        public string sliderTag = "Tag Text";
        public float defaultValue = 1;

        [Header("SETTINGS")]
        public bool usePercent = false;
        public bool showValue = true;
        public bool useRoundValue = false;

        private Slider mainSlider;
        float saveValue;

        void Start()
        {
            mainSlider = this.GetComponent<Slider>();

            if (showValue == false)
                valueText.enabled = false;

            if (enableSaving == true)
            {
                if (PlayerPrefs.HasKey(sliderTag + "DarkSliderValue") == false)
                    saveValue = defaultValue;
                else
                    saveValue = PlayerPrefs.GetFloat(sliderTag + "DarkSliderValue");

                mainSlider.value = saveValue;

                mainSlider.onValueChanged.AddListener(delegate
                {
                    saveValue = mainSlider.value;
                    PlayerPrefs.SetFloat(sliderTag + "DarkSliderValue", saveValue);
                });
            }
        }

        void Update()
        {
            if (useRoundValue == true)
            {
                if (usePercent == true)
                    valueText.text = Mathf.Round(mainSlider.value * 1.0f).ToString() + "%";

                else
                    valueText.text = Mathf.Round(mainSlider.value * 1.0f).ToString();
            }

            else
            {
                if (usePercent == true)
                    valueText.text = mainSlider.value.ToString("F1") + "%";

                else
                    valueText.text = mainSlider.value.ToString("F1");
            }
        }
    }
}