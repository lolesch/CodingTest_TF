using CodingTest_TF.Data.Enums;
using CodingTest_TF.Data.Recordings;
using CodingTest_TF.Runtime.UI.Buttons;
using CodingTest_TF.Utility.Extensions;
using UnityEngine;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class ApplyColorCommand : ICommand
    {
        public ApplyColorCommand(CommandButton receiver) => this.receiver = receiver;

        [SerializeField] private CommandButton receiver;

        public void Execute()
        {
            ApplyColor(receiver.CurrentTint);

            ActionRecording.AddEntry(this);
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
