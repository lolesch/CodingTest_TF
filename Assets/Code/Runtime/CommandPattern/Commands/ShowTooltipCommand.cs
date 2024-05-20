using CodingTest.Runtime.Provider;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ShowTooltipCommand : BaseCommand
    {
        public string Tooltip { get; private set; }
        public float Delay { get; private set; }

        public static event Action<string, float> OnShowTooltip;

        public ShowTooltipCommand(string tooltip, float delay)
        {
            Tooltip = tooltip;
            Delay = delay;
        }

        public ShowTooltipCommand(ShowTooltipMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            OnShowTooltip?.Invoke(Tooltip, Delay);

            ReplayProvider.Instance.Record(Serialize());
        }

        public override CommandMemento Serialize() => new ShowTooltipMemento(this);
        public override void Deserialize(CommandMemento memento) => Tooltip = (memento as ShowTooltipMemento).TooltipText;
    }

    [DataContract]
    public class ShowTooltipMemento : CommandMemento
    {
        [DataMember] public readonly string TooltipText;
        [DataMember] public readonly float Delay;
        public ShowTooltipMemento(ShowTooltipCommand command) : base(command)
        {
            TooltipText = command.Tooltip;
            Delay = command.Delay;
        }
    }
}
