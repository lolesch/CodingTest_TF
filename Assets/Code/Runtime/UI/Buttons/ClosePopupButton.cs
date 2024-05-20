using CodingTest.Runtime.CommandPattern;

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

        protected override void OnLeftClick() => closePopupCommand.Execute();
        protected override void OnRightClick() { }
    }
}
