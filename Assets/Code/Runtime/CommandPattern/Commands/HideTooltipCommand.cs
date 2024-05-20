using CodingTest.Runtime.Provider;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class HideTooltipCommand : BaseCommand
    {
        public static event Action OnHideTooltip;

        public HideTooltipCommand() { }
        public HideTooltipCommand(HideTooltipMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            OnHideTooltip?.Invoke();

            ReplayProvider.Instance.AddRecording(Serialize());
        }

        public override CommandMemento Serialize() => new HideTooltipMemento(this);
        public override void Deserialize(CommandMemento memento) { }
    }

    [DataContract]
    public sealed class HideTooltipMemento : CommandMemento
    {
        public HideTooltipMemento(BaseCommand command) : base(command) { }
    }
}
