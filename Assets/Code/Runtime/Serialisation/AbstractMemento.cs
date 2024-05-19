using CodingTest.Runtime.CommandPattern;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.Serialization
{
    [DataContract]
    public abstract class AbstractMemento { }

    [DataContract]
    public abstract class AbstractICommandMemento : AbstractMemento
    {
        [DataMember] public readonly ICommand Command;

        public AbstractICommandMemento(ICommand command) => Command = command;
    }
}
