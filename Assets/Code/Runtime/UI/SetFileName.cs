using CodingTest_TF.Runtime.CommandPattern;
using TMPro;
using UnityEngine;

namespace CodingTest_TF.Runtime.UI
{
    [RequireComponent(typeof(TMP_InputField))]
    public sealed class SetFileName : MonoBehaviour
    {
        private SetFileNameCommand setFileNameCommand;

        private TMP_InputField inputField;

        public TMP_InputField InputField => inputField == null ? inputField = GetComponent<TMP_InputField>() : inputField;

        private void Start() => InputField.onEndEdit.AddListener(SetName);

        private void SetName(string fileName)
        {
            setFileNameCommand = new SetFileNameCommand(fileName);
            setFileNameCommand.Execute();

        }
    }
}
