using System;

namespace CodingTest_TF.Runtime.CommandPattern
{
    public sealed class SetFileNameCommand : ICommand
    {
        private readonly string fileName;
        public static event Action<string> OnSetFileName;

        public SetFileNameCommand(string fileName) => this.fileName = fileName;

        public void Execute() => OnSetFileName?.Invoke(fileName);
    }
}
