using System;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class OpenPopupCommand : ICommand
    {
        private readonly string popupText;
        public static event Action<string> OnShowPopup;

        public OpenPopupCommand(string popupText) => this.popupText = popupText;

        public void UnExecute() => throw new NotImplementedException();

        // recording entry
        public void Execute() => OnShowPopup?.Invoke(popupText);
    }
}
