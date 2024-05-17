using System;
using UnityEngine;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class ShowTooltipCommand : ICommand
    {
        private readonly string tooltip;

        public ShowTooltipCommand(string tooltip) => this.tooltip = tooltip;

        public void UnExecute() => throw new NotImplementedException();

        // invoke action
        // have a tooltip object listening to this action?
        public void Execute() => Debug.Log($"TODO: show the tooltip with this text: {tooltip}");
    }
}
