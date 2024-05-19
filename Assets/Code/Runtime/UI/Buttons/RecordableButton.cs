using CodingTest.Data;
using CodingTest.Data.Enums;
using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.Provider;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodingTest.Runtime.UI.Buttons
{
    public sealed class RecordableButton : AbstractButton, IBeginDragHandler, IEndDragHandler
    {
        private OpenPopupCommand openPopupCommand;
        private ShowTooltipCommand showTooltipCommand;
        private HideTooltipCommand hideTooltipCommand;
        private CycleColorCommand cycleColorCommand;
        private ApplyColorCommand applyColorCommand;

        // can we make this a separate component?
        private Coroutine showTooltip;

        [Header("Initial Values")]
        [SerializeField] private string popupText = $"Set the popup text in the inspector";
        [SerializeField] private string tooltipText = $"Set the tooltip text in the inspector";
        [SerializeField] internal ButtonTint CurrentTint = ButtonTint.Red;

        protected override void Start()
        {
            base.Start();
            openPopupCommand = new OpenPopupCommand(popupText);
            showTooltipCommand = new ShowTooltipCommand(tooltipText);
            hideTooltipCommand = new HideTooltipCommand();
            cycleColorCommand = new CycleColorCommand(this);
            applyColorCommand = new ApplyColorCommand(this);

            applyColorCommand.Execute();
        }

        // TODO: make it event based instead of update
        private void FixedUpdate() => interactable = !ReplayProvider.Instance.IsReplaying;

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
        }

        private void HideTooltip()
        {
            if (showTooltip != null)
                StopCoroutine(showTooltip);

            hideTooltipCommand.Execute();
        }

        private IEnumerator ShowTooltip(float delay)
        {
            yield return new WaitForSeconds(delay);

            showTooltipCommand.Execute();
        }

        public void OnBeginDrag(PointerEventData eventData) => HideTooltip();
        public void OnEndDrag(PointerEventData eventData) => showTooltip = StartCoroutine(ShowTooltip(Constants.TooltipDelayAfterInteraction));
    }
}
