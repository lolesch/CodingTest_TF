using CodingTest.Data.ReplaySystem;
using UnityEngine;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ShowTooltipCommand : ICommand
    {
        private readonly string tooltip;

        public ShowTooltipCommand(string tooltip) => this.tooltip = tooltip;

        // invoke action
        // have a tooltip object listening to this action?
        public void Execute()
        {
            Debug.Log($"TODO: show the tooltip with this text: {tooltip}");

            Recording.AddEntry(this);
        }
    }
}
