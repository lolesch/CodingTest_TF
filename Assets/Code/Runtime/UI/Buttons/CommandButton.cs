using CodingTest_TF.Data;
using CodingTest_TF.Data.Enums;
using CodingTest_TF.Runtime.CommandPattern;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodingTest_TF.Runtime.UI.Buttons
{
    public sealed class CommandButton : AbstractButton, IBeginDragHandler, IEndDragHandler
    {
        private OpenPopupCommand openPopupCommand;
        private ShowTooltipCommand showTooltipCommand;
        private CycleColorCommand cycleColorCommand;
        private ApplyColorCommand applyColorCommand;

        private Coroutine showTooltip;

        public static event Action<string> OnShowPopup;

        public static event Action<string> OnShowTooltip;
        public static event Action OnHideTooltip;

        [Header("Initial Values")]
        [SerializeField] private string popupText = $"Set the popup text in the inspector";
        [SerializeField] private string tooltipText = $"Set the tooltip text in the inspector";
        public ButtonTint CurrentTint = ButtonTint.Red;

        protected override void Start()
        {
            base.Start();
            openPopupCommand = new OpenPopupCommand(popupText);
            showTooltipCommand = new ShowTooltipCommand(tooltipText);
            cycleColorCommand = new CycleColorCommand(this);
            applyColorCommand = new ApplyColorCommand(this);

            applyColorCommand.Execute();
        }

        protected override void OnLeftClick()
        {
            HideTooltip();

            openPopupCommand.Execute();

            showTooltip = StartCoroutine(ShowTooltip(Constants.TooltipDelayAfterInteraction));
        }

        protected override void OnRightClick()
        {
            HideTooltip();

            cycleColorCommand.Execute();
            applyColorCommand.Execute();

            showTooltip = StartCoroutine(ShowTooltip(Constants.TooltipDelayAfterInteraction));
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            showTooltip = StartCoroutine(ShowTooltip(Constants.TooltipDelay));
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            HideTooltip();

            //showTooltipCommand.UnExecute();
        }

        private void HideTooltip()
        {
            if (showTooltip != null)
                StopCoroutine(showTooltip);

            OnHideTooltip?.Invoke();
        }

        private IEnumerator ShowTooltip(float delay)
        {
            yield return new WaitForSeconds(delay);

            OnShowTooltip?.Invoke(tooltipText);

            //showTooltipCommand.Execute();
        }

        public void OnBeginDrag(PointerEventData eventData) => HideTooltip();
        public void OnEndDrag(PointerEventData eventData) => showTooltip = StartCoroutine(ShowTooltip(Constants.TooltipDelayAfterInteraction));
    }
}
