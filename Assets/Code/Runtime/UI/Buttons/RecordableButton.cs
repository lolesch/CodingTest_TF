using CodingTest.Data;
using CodingTest.Data.Enums;
using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.Provider;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodingTest.Runtime.UI.Buttons
{
    public sealed class RecordableButton : AbstractButton, IBeginDragHandler, IEndDragHandler
    {
        private OpenPopupCommand openPopupCommand;
        private ShowTooltipCommand showTooltipShortDelayCommand;
        private ShowTooltipCommand showTooltipLongDelayCommand;
        private HideTooltipCommand hideTooltipCommand;
        private CycleColorCommand cycleColorCommand;
        private ApplyColorCommand applyColorCommand;

        [Header("Initial Values")]
        [SerializeField] private string popupText = $"Set the popup text in the inspector";
        [SerializeField] private string tooltipText = $"Set the tooltip text in the inspector";
        [SerializeField] internal ButtonTint CurrentTint = ButtonTint.Red;

        protected override void Start()
        {
            base.Start();
            openPopupCommand = new OpenPopupCommand(popupText);
            showTooltipShortDelayCommand = new ShowTooltipCommand(tooltipText, Constants.TooltipDelay);
            showTooltipLongDelayCommand = new ShowTooltipCommand(tooltipText, Constants.TooltipDelayAfterInteraction);
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

            showTooltipLongDelayCommand.Execute();
        }

        protected override void OnRightClick()
        {
            HideTooltip();

            cycleColorCommand.Execute();
            applyColorCommand.Execute();

            showTooltipLongDelayCommand.Execute();
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            showTooltipShortDelayCommand.Execute();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            HideTooltip();
        }

        private void HideTooltip() => hideTooltipCommand.Execute();

        public void OnBeginDrag(PointerEventData eventData)
        {
            HideTooltip();
            new SetPositionCommand(this, transform.position).Execute();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            showTooltipLongDelayCommand.Execute();
            //TODO: lerp instead of set position
            new SetPositionCommand(this, transform.position).Execute();
        }
    }
}
