using CodingTest.Data.Enums;
using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.Provider;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodingTest.Runtime.UI.Buttons
{
    public sealed class RecordableButton : AbstractButton, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private OpenPopupCommand openPopupCommand;
        private CycleColorCommand cycleColorCommand;
        private ApplyColorCommand applyColorCommand;

        [SerializeField] private string popupText = $"Set the popup text in the inspector";
        [SerializeField] internal ButtonTint CurrentTint = ButtonTint.Red;

        protected override void Start()
        {
            base.Start();

            openPopupCommand = new OpenPopupCommand(popupText);
            cycleColorCommand = new CycleColorCommand(this);
            applyColorCommand = new ApplyColorCommand(this);

            applyColorCommand.Execute();
        }

        // TODO: make it event based instead of update
        private void FixedUpdate() => interactable = !ReplayProvider.Instance.IsReplaying;

        protected override void OnLeftClick() => openPopupCommand.Execute();
        protected override void OnRightClick()
        {
            cycleColorCommand.Execute();
            applyColorCommand.Execute();
        }

        public void OnBeginDrag(PointerEventData eventData) => hideTooltipCommand.Execute();
        public void OnEndDrag(PointerEventData eventData) => showTooltipLongDelayCommand.Execute();
        public void OnDrag(PointerEventData eventData) => ReplayProvider.Instance.AddRecording(new SetPositionCommand(this, transform.position).Serialize());
    }
}
