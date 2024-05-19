using CodingTest.Data.Enums;
using CodingTest.Data.ReplaySystem;
using CodingTest.Runtime.UI.Buttons;
using UnityEngine;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class CycleColorCommand : ICommand
    {
        public CycleColorCommand(CommandButton receiver) => this.receiver = receiver;

        [SerializeField] private CommandButton receiver;

        public void Execute()
        {
            CycleColor();

            Recording.AddEntry(this);
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
