using CodingTest.Runtime.Provider;
using TMPro;
using UnityEngine;

namespace CodingTest.Runtime.UI
{
    [RequireComponent(typeof(TMP_InputField))]
    public sealed class SetRecordingName : MonoBehaviour
    {
        private TMP_InputField inputField;

        public TMP_InputField InputField => inputField == null ? inputField = GetComponent<TMP_InputField>() : inputField;

        private void OnDisable() => InputField.onEndEdit.RemoveListener(ReplayProvider.Instance.SetRecordingName);
        private void OnEnable() => InputField.onEndEdit.AddListener(ReplayProvider.Instance.SetRecordingName);

        // TODO: make it event based instead of update
        private void FixedUpdate() => InputField.interactable = !ReplayProvider.Instance.IsRecording && !ReplayProvider.Instance.IsReplaying;
    }
}
