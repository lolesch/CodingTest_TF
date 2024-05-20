using CodingTest.Runtime.CommandPattern;
using CodingTest.Runtime.Provider;

namespace CodingTest.Runtime.UI.Buttons
{
    public sealed class ClosePopupButton : AbstractButton
    {
        private ClosePopupCommand closePopupCommand;

        protected override void Start()
        {
            base.Start();
            closePopupCommand = new ClosePopupCommand();
        }

        // TODO: make it event based instead of update
        private void FixedUpdate() => interactable = !ReplayProvider.Instance.IsReplaying;

        protected override void OnLeftClick() => closePopupCommand.Execute();
        protected override void OnRightClick() { }
    }
}
