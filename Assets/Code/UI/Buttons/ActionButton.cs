using CodingTest_TF.Serialization;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodingTest_TF.UI.Buttons
{
    public sealed class ActionButton : AbstractButton, ISerializable<ActionButton.Memento>
    {
        // command pattern?

        // TODO: serialize initial values

        // TODO: dragging is another component => IDragHandler

        private enum ButtonTint
        {
            None = 0,

            Red = 1,
            Green = 2,
            Blue = 3,
        }

        [Header("Settings")]
        [SerializeField] private ButtonTint currentTint = ButtonTint.Red;
        [SerializeField] private string popupText = $"Set the tooltip text in the inspector or in the referenced SO";
        [SerializeField] private float tooltipDelay = .5f;
        [SerializeField] private Coroutine showTooltipCoroutine;

        protected override void OnLeftClick() => OpenPopUp();

        protected override void OnRightClick() => CycleButtonColor();

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            showTooltipCoroutine = StartCoroutine(ShowTooltip());
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            StopCoroutine(showTooltipCoroutine);
        }

        private IEnumerator ShowTooltip()
        {
            yield return new WaitForSeconds(tooltipDelay);

            // TODO: show tooltip
            Debug.Log($"This button has the following popup text: {popupText}");
        }

        private void OpenPopUp() => Debug.Log($"TODO: whow the popup with this text: {popupText}");

        #region Color Cycle
        private void CycleButtonColor()
        {
            CycleColor();
            ApplyColor(currentTint);
        }

        private void CycleColor() => currentTint = currentTint switch
        {
            ButtonTint.Red => ButtonTint.Green,
            ButtonTint.Green => ButtonTint.Blue,
            ButtonTint.Blue => ButtonTint.Red,

            _ => currentTint
        };

        private void ApplyColor(ButtonTint tint) => targetGraphic.color = tint switch
        {
            ButtonTint.Red => Color.red,
            ButtonTint.Green => Color.green,
            ButtonTint.Blue => Color.blue,

            _ => targetGraphic.color
        };
        #endregion Color Cycle

        #region ISerializable
        public void Deserialize(Memento memento) => throw new System.NotImplementedException();
        public Memento Serialize() => throw new System.NotImplementedException();
        #endregion ISerializable

        public sealed class Memento : AbstractMemento
        {

        }

        protected override void OnValidate()
        {
            base.OnValidate();
            ApplyColor(currentTint);
        }

        //protected override void Start() => Deserialize(); // if no memento was found, use default values from scriptable object
    }
}