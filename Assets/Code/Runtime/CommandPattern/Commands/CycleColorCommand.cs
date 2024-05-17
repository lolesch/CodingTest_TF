using CodingTest_TF.Data.Enums;
using CodingTest_TF.UI.Buttons;
using System;
using UnityEngine;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class CycleColorCommand : ICommand
    {
        public CycleColorCommand(CommandButton receiver) => this.receiver = receiver;

        [SerializeField] private CommandButton receiver;

        public void UnExecute() => throw new NotImplementedException();

        public void Execute() => CycleColor();

        private void CycleColor() => receiver.CurrentTint = receiver.CurrentTint switch
        {
            ButtonTint.Red => ButtonTint.Green,
            ButtonTint.Green => ButtonTint.Blue,
            ButtonTint.Blue => ButtonTint.Red,

            _ => ButtonTint.Red
        };
    }
}
