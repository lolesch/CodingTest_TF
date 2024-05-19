using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class HideTooltipCommand : ICommand, ISerializable<HideTooltipCommand.Memento>
    {
        public static event Action OnHideTooltip;

        public HideTooltipCommand() { }

        public void Execute()
        {
            OnHideTooltip?.Invoke();

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
