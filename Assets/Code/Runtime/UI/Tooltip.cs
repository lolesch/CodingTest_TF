using CodingTest_TF.Runtime.UI.Buttons;
using CodingTest_TF.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace CodingTest_TF.Runtime.UI.Panels
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class Tooltip : AbstractPanel
    {
        private TextMeshProUGUI tooltip;
        private bool showLeft;
        private float OffsetX => showLeft ? +10 : -10;

        protected override void OnDisable()
        {
            base.OnDisable();

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

        private void LateUpdate()
        {
            if (IsActive)
                SetPosition();
        }

        private void HideTooltip() => FadeOut();
        private void ShowTooltip(string text)
        {
            tooltip.text = text;

            /// pivot pointing towards center of screen
            showLeft = Input.mousePosition.x < (Screen.width * 0.5);

            SetPosition();

            FadeIn();
        }

        private void SetPosition()
        {
            var pivotX = showLeft ? 0 : 1;
            var pivotY = Input.mousePosition.y.MapTo01(0, Screen.height);
            Transform.pivot = new(pivotX, pivotY);

            var mousePos = (Vector2)Input.mousePosition / Transform.lossyScale;
            Transform.anchoredPosition = new Vector2(mousePos.x + OffsetX, mousePos.y);
        }
    }
}