using CodingTest_TF.Runtime.CommandPattern;
using TMPro;

namespace CodingTest_TF.Runtime.UI.Panels
{
    public sealed class Popup : AbstractPanel
    {
        private TextMeshProUGUI popupText;
        protected override void OnDisable()
        {
            base.OnDisable();

            OpenPopupCommand.OnShowPopup -= OpenPopup;
        }

        private void OnEnable()
        {
            OpenPopupCommand.OnShowPopup -= OpenPopup;
            OpenPopupCommand.OnShowPopup += OpenPopup;
        }
        private void Start() => popupText = GetComponentInChildren<TextMeshProUGUI>();

        private void OpenPopup(string text)
        {
            popupText.text = text;

            FadeIn();
        }
    }
}
