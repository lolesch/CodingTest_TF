using CodingTest_TF.Data.Enums;
using CodingTest_TF.UI.Buttons;
using System;
using UnityEngine;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class ApplyColorCommand : ICommand
    {
        public ApplyColorCommand(CommandButton receiver) => this.receiver = receiver;

        [SerializeField] private CommandButton receiver;

        public void UnExecute() => throw new NotImplementedException();

        public void Execute() => ApplyColor(receiver.CurrentTint);

        private void ApplyColor(ButtonTint tint) => receiver.targetGraphic.color = tint switch
        {
            ButtonTint.Red => Color.red,
            ButtonTint.Green => Color.green,
            ButtonTint.Blue => Color.blue,

            _ => receiver.targetGraphic.color
        };
    }


}
