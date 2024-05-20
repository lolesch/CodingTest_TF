using CodingTest.Data;
using CodingTest.Runtime.CommandPattern;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodingTest.Runtime.UI.Buttons
{
    public abstract class AbstractButton : Selectable, IPointerClickHandler
    {
        // TODO: disable the button for x seconds to prevent spam clicking

        protected ShowTooltipCommand showTooltipShortDelayCommand;
        protected ShowTooltipCommand showTooltipLongDelayCommand;
        protected HideTooltipCommand hideTooltipCommand;

        [SerializeField] private string tooltipText = $"Set the tooltip text in the inspector";

        protected override void Start()
        {
            base.Start();
            showTooltipShortDelayCommand = new ShowTooltipCommand(tooltipText, Constants.TooltipDelay);
            showTooltipLongDelayCommand = new ShowTooltipCommand(tooltipText, Constants.TooltipDelayAfterInteraction);
            hideTooltipCommand = new HideTooltipCommand();
        }

        protected abstract void OnLeftClick();
        protected abstract void OnRightClick();

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable || eventData.dragging)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                OnLeftClick();
            else if (eventData.button == PointerEventData.InputButton.Right)
                OnRightClick();

            hideTooltipCommand.Execute();
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
            hideTooltipCommand.Execute();
        }
    }
}