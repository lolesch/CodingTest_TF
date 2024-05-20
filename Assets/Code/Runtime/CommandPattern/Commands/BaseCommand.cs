using CodingTest.Runtime.Serialization;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public abstract class BaseCommand : ICommand, ISerializable<CommandMemento>
    {
        public abstract void Execute();

        public abstract CommandMemento Serialize();
        public abstract void Deserialize(CommandMemento memento);
    }

    [DataContract]
    public abstract class CommandMemento : AbstractMemento
    {
        [DataMember] public readonly string CommandName;

        public CommandMemento(BaseCommand command) => CommandName = command.GetType().Name;
    }
}
