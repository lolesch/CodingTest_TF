using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ShowTooltipCommand : ICommand, ISerializable<ShowTooltipCommand.Memento>
    {
        private readonly string tooltip;
        public static event Action<string> OnShowTooltip;

        public ShowTooltipCommand(string tooltip) => this.tooltip = tooltip;

        public void Execute()
        {
            OnShowTooltip?.Invoke(tooltip);

            ReplayProvider.Instance.AddEntry(Serialize());
        }
        public Memento Serialize() => throw new System.NotImplementedException();
        public void Deserialize(Memento memento) => throw new System.NotImplementedException();

        [DataContract]
        public class Memento : AbstractICommandMemento
        {
            public Memento(ICommand command) : base(command)
            {
            }
        }
    }
}
