using CodingTest.Data.ReplaySystem;
using System;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class OpenPopupCommand : ICommand
    {
        private readonly string popupText;
        public static event Action<string> OnShowPopup;

        public OpenPopupCommand(string popupText) => this.popupText = popupText;

        public void Execute()
        {
            OnShowPopup?.Invoke(popupText);

            Recording.AddEntry(this);
        }
    }
}
