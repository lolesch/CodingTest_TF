using CodingTest_TF.Utility.AttributeRefs;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodingTest_TF.Runtime.UI.Toggles
{
    public abstract class AbstractToggle : Selectable, IPointerClickHandler
    {
        [field: SerializeField] public bool IsOn { get; private set; } = false;

        [SerializeField, ReadOnly] protected RadioGroup radioGroup = null;
        public RadioGroup RadioGroup => radioGroup != null ? radioGroup : radioGroup = GetComponentInParent<RadioGroup>();

        [SerializeField] private Sprite toggledOffSprite;
        [SerializeField] private Sprite toggledOnSprite;
        [SerializeField] private string toggledOffText;
        [SerializeField] private string toggledOnText;

        public event Action<bool> OnToggle;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (RadioGroup != null && RadioGroup.transform != transform.parent)
                radioGroup = null;

            if (IsOn && RadioGroup)
                RadioGroup.Activate(this);
        }
#endif // if UNTIY_EDITOR

        protected override void OnDisable()
        {
            base.OnDisable();

            if (RadioGroup)
                RadioGroup.Unregister(this);

            if (targetGraphic && DOTween.IsTweening(targetGraphic.transform))
                _ = DOTween.Kill(targetGraphic.transform);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (RadioGroup && interactable)
                RadioGroup.Register(this);
        }

        protected override void Start()
        {
            base.Start();

            SetToggle(IsOn);
        }

        internal void SetToggle(bool isOn)
        {
            IsOn = isOn;
            OnToggle?.Invoke(IsOn);

            if (targetGraphic is Image image)
            {
                if (toggledOffSprite != null && toggledOnSprite != null)
                    image.sprite = IsOn ? toggledOnSprite : toggledOffSprite;
            }
            else if (targetGraphic is TextMeshProUGUI tmp)
            {
                if (toggledOffText != string.Empty && toggledOnText != string.Empty)
                    tmp.text = IsOn ? toggledOnText : toggledOffText;
            }

            if (IsOn && RadioGroup)
                RadioGroup.Activate(this);

            if (!IsOn)
                DoStateTransition(SelectionState.Normal, false);

            PlayToggleSound(IsOn);

            Toggle(isOn);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            if (interactable)
                DoStateTransition(IsOn ? SelectionState.Selected : SelectionState.Normal, false);
        }

        protected abstract void Toggle(bool isOn);

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)
                return;

            if (RadioGroup && !RadioGroup.AllowSwitchOff && IsOn)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                SetToggle(!IsOn);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (interactable)
                DoStateTransition(IsOn ? SelectionState.Selected : SelectionState.Normal, false);
        }

        public virtual void PlayToggleSound(bool isOn) { } // => AudioProvider.Instance.PlayButtonClick();
    }
}