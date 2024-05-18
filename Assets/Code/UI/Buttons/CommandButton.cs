using CodingTest_TF.Data;
using CodingTest_TF.Data.Enums;
using CodingTest_TF.Runtime.CommandPattern;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodingTest_TF.UI.Buttons
{
    public sealed class CommandButton : AbstractButton
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

        protected override void OnLeftClick() => openPopupCommand.Execute();
        protected override void OnRightClick()
        {
            cycleColorCommand.Execute();
            applyColorCommand.Execute();
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            showTooltip = StartCoroutine(ShowTooltip());
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (showTooltip != null)
                StopCoroutine(showTooltip);

            OnHideTooltip?.Invoke();

            //showTooltipCommand.UnExecute();
        }

        private IEnumerator ShowTooltip()
        {
            yield return new WaitForSeconds(Constants.TooltipDelay);

            OnShowTooltip?.Invoke(tooltipText);

            //showTooltipCommand.Execute();
        }
    }
}
