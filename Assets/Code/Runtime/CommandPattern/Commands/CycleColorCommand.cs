using CodingTest_TF.Data.Enums;
using CodingTest_TF.Data.Recordings;
using CodingTest_TF.Runtime.UI.Buttons;
using UnityEngine;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class CycleColorCommand : ICommand
    {
        public CycleColorCommand(CommandButton receiver) => this.receiver = receiver;

        [SerializeField] private CommandButton receiver;

        public void Execute()
        {
            CycleColor();

            ActionRecording.AddEntry(this);
        }

        private void CycleColor() => receiver.CurrentTint = receiver.CurrentTint switch
        {
            ButtonTint.Red => ButtonTint.Green,
            ButtonTint.Green => ButtonTint.Blue,
            ButtonTint.Blue => ButtonTint.Red,

            _ => ButtonTint.Red
        };
    }
}
