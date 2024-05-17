using CodingTest_TF.Utility.Extensions;
using System;
using UnityEngine;

namespace CodingTest_TF.Runtime.CommandPattern
{
    [Serializable]
    public class TestCommand : ICommand
    {
        public void Execute() => Debug.Log($"{nameof(TestCommand)} was {"executed".Colored(ColorExtensions.Orange)}");
        public void UnExecute() => Debug.Log($"{nameof(TestCommand)} was {"unexecuted".Colored(ColorExtensions.Orange)}");
    }
}
