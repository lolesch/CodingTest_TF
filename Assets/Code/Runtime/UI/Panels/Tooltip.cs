using CodingTest.Runtime.CommandPattern;
using CodingTest.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace CodingTest.Runtime.UI.Panels
{
    public sealed class Tooltip : AbstractPanel
    {
        private TextMeshProUGUI tooltip;
        private bool showLeft;

        [SerializeField, Range(0, 100)] private float horOffset = 10;
        private float OffsetX => showLeft ? +horOffset : -horOffset;

        protected override void OnDisable()
        {
            base.OnDisable();

            ShowTooltipCommand.OnShowTooltip -= ShowTooltip;
            HideTooltipCommand.OnHideTooltip -= HideTooltip;
        }

        private void OnEnable()
        {
            ShowTooltipCommand.OnShowTooltip -= ShowTooltip;
            ShowTooltipCommand.OnShowTooltip += ShowTooltip;

            HideTooltipCommand.OnHideTooltip -= HideTooltip;
            HideTooltipCommand.OnHideTooltip += HideTooltip;
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

            showLeft = Input.mousePosition.x < (Screen.width * 0.5);

            SetPosition();

            FadeIn();
        }

        private void SetPosition()
        {
            /// pivot pointing towards center of screen
            var pivotX = showLeft ? 0 : 1;
            var pivotY = Input.mousePosition.y.MapTo01(0, Screen.height);
            RectTransform.pivot = new(pivotX, pivotY);

            var mousePos = (Vector2)Input.mousePosition / RectTransform.lossyScale;
            RectTransform.anchoredPosition = new Vector2(mousePos.x + OffsetX, mousePos.y);
        }
    }
}
