using CodingTest.Data.Enums;
using CodingTest.Data.ReplaySystem;
using CodingTest.Runtime.UI.Buttons;
using CodingTest.Utility.Extensions;
using UnityEngine;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ApplyColorCommand : ICommand
    {
        public ApplyColorCommand(CommandButton receiver) => this.receiver = receiver;

        [SerializeField] private CommandButton receiver;

        public void Execute()
        {
            ApplyColor(receiver.CurrentTint);

            Recording.AddEntry(this);
        }

        private void ApplyColor(ButtonTint tint) => receiver.targetGraphic.color = tint switch
        {
            ButtonTint.Red => ColorExtensions.ButtonRed,
            ButtonTint.Green => ColorExtensions.ButtonGreen,
            ButtonTint.Blue => ColorExtensions.ButtonBlue,

            _ => receiver.targetGraphic.color
        };
    }


}
