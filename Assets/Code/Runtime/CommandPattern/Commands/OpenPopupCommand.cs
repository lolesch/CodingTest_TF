﻿using CodingTest.Runtime.Provider;
using CodingTest.Runtime.Serialization;
using System;
using System.Runtime.Serialization;

namespace CodingTest.Runtime.CommandPattern
{
    public sealed class OpenPopupCommand : ICommand, ISerializable<OpenPopupCommand.Memento>
    {
        private readonly string popupText;
        public static event Action<string> OnShowPopup;

        public OpenPopupCommand(string popupText) => this.popupText = popupText;

        public void Execute()
        {
            OnShowPopup?.Invoke(popupText);

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
