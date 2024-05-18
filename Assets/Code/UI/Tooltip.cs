using CodingTest_TF.UI.Buttons;
using CodingTest_TF.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace CodingTest_TF.UI
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class Tooltip : MonoBehaviour
    {
        /// <summary>
        /// A cached reference to the RectTransform
        /// </summary>
        public RectTransform RectTransform
        {
            get
            {
                if (m_rectTransform == null)
                    m_rectTransform = GetComponent<RectTransform>();
                return m_rectTransform;
            }
        }
        private RectTransform m_rectTransform;

        private TextMeshProUGUI tooltip;
        private bool isShowing;
        private bool showLeft;
        private float OffsetX => showLeft ? +10 : -10;

        private void OnDisable()
        {
            CommandButton.OnShowTooltip -= ShowTooltip;
            CommandButton.OnHideTooltip -= HideTooltip;
        }

        private void OnEnable()
        {
            CommandButton.OnShowTooltip -= ShowTooltip;
            CommandButton.OnShowTooltip += ShowTooltip;

            CommandButton.OnHideTooltip -= HideTooltip;
            CommandButton.OnHideTooltip += HideTooltip;
        }

        private void Start() => tooltip = GetComponentInChildren<TextMeshProUGUI>();

        private void Update()
        {
            if (isShowing)
                SetPosition();
        }

        private void HideTooltip() => isShowing = false;
        private void ShowTooltip(string text)
        {
            tooltip.text = text;

            isShowing = true;
        }

        private void SetPosition()
        {
            /// pivot pointing towards center of screen
            showLeft = Input.mousePosition.x < (Screen.width * 0.5);

            var pivotX = showLeft ? 0 : 1;
            var pivotY = Input.mousePosition.y.MapTo01(0, Screen.height);
            RectTransform.pivot = new(pivotX, pivotY);

            var mousePos = (Vector2)Input.mousePosition / RectTransform.lossyScale;
            RectTransform.anchoredPosition = new Vector2(mousePos.x + OffsetX, mousePos.y);
        }
    }
}
