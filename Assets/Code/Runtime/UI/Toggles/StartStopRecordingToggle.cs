using CodingTest_TF.Runtime.CommandPattern;
using CodingTest_TF.Utility.Extensions;
using UnityEngine;

namespace CodingTest_TF.Runtime.UI.Toggles
{
    public sealed class StartStopRecordingToggle : AbstractToggle
    {
        private readonly StartRecordingCommand startRecordingCommand = new();

        // register to SetFileName to disable if fileName is empty

        protected override void Toggle(bool isOn)
        {
            if (isOn)
                startRecordingCommand.Execute();
            else
                Debug.Log($"TOGGLE:\t{name.ColoredComponent()} was toggled off", this);
        }

        // TODO: tooltip for required fileName
    }
}
