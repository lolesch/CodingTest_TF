using CodingTest.Runtime.Provider;
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class ShowTooltipCommand : BaseCommand
    {
        public string Tooltip { get; private set; }
        public float Delay { get; private set; }
        public Vector2 Position { get; private set; }

        public static event Action<string, Vector2> OnShowTooltip;

        public ShowTooltipCommand(string tooltip, float delay)
        {
            Tooltip = tooltip;
            Delay = delay;
        }

        public ShowTooltipCommand(ShowTooltipMemento memento) => Deserialize(memento);

        public override void Execute()
        {
            if (!ReplayProvider.Instance.IsReplaying)
                Position = Input.mousePosition;

            OnShowTooltip?.Invoke(Tooltip, Position);

            ReplayProvider.Instance.AddRecording(Serialize());
        }

        public override CommandMemento Serialize() => new ShowTooltipMemento(this);
        public override void Deserialize(CommandMemento memento)
        {
            var m = memento as ShowTooltipMemento;
            Tooltip = m.TooltipText;
            Delay = m.Delay;
            Position = m.Position;
        }
    }

    [DataContract]
    public sealed class ShowTooltipMemento : CommandMemento
    {
        [DataMember] public readonly string TooltipText;
        [DataMember] public readonly float Delay;
        [DataMember] public readonly Vector2 Position;
        public ShowTooltipMemento(ShowTooltipCommand command) : base(command)
        {
            TooltipText = command.Tooltip;
            Delay = command.Delay;
            Position = command.Position;
        }
    }
}
