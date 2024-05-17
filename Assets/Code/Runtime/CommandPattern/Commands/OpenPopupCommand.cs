using System;
using UnityEngine;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class OpenPopupCommand : ICommand
    {
        private readonly string popupText;

        public OpenPopupCommand(string popupText) => this.popupText = popupText;

        public void UnExecute() => throw new NotImplementedException();

        // recording entry
        public void Execute() => Debug.Log($"TODO: show the popup with this text: {popupText}");
    }
}
