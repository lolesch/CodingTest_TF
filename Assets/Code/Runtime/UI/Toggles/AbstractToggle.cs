using CodingTest.Data;
using CodingTest.Runtime.CommandPattern;
using CodingTest.Utility.AttributeRefs;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodingTest.Runtime.UI.Toggles
{
    public abstract class AbstractToggle : Selectable, IPointerClickHandler
    {
        [SerializeField] protected string tooltipForNotInteractable = $"Set the tooltip text in the inspector";
        [SerializeField] protected string tooltipForOn = $"Set the tooltip text in the inspector";
        [SerializeField] protected string tooltipForOff = $"Set the tooltip text in the inspector";

        private ShowTooltipCommand showTooltipNotInteractable;
        private ShowTooltipCommand showTooltipForOnShortDelay;
        private ShowTooltipCommand showTooltipForOnLongDelay;
        private ShowTooltipCommand showTooltipForOffShortDelay;
        private ShowTooltipCommand showTooltipForOffLongDelay;
        private HideTooltipCommand hideTooltip;

        [field: SerializeField] public bool IsOn { get; private set; } = false;

        [SerializeField, ReadOnly] protected RadioGroup radioGroup = null;
        public RadioGroup RadioGroup => radioGroup == null ? radioGroup = GetComponentInParent<RadioGroup>() : radioGroup;

        [SerializeField, ReadOnly] protected TextMeshProUGUI displayText = null;
        public TextMeshProUGUI DisplayText => displayText == null ? displayText = GetComponentInChildren<TextMeshProUGUI>() : displayText;
        [SerializeField] private string toggledOffText;
        [SerializeField] private string toggledOnText;

        [SerializeField, ReadOnly] protected Image icon = null;
        public Image Icon => icon == null ? icon = GetComponentsInChildren<Image>()[1] : icon;
        [SerializeField, PreviewIcon] private Sprite toggledOffSprite;
        [SerializeField, PreviewIcon] private Sprite toggledOnSprite;

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
            showTooltipNotInteractable = new ShowTooltipCommand(tooltipForNotInteractable, Constants.TooltipDelay);
            showTooltipForOnShortDelay = new ShowTooltipCommand(tooltipForOn, Constants.TooltipDelay);
            showTooltipForOnLongDelay = new ShowTooltipCommand(tooltipForOn, Constants.TooltipDelayAfterInteraction);
            showTooltipForOffShortDelay = new ShowTooltipCommand(tooltipForOff, Constants.TooltipDelay);
            showTooltipForOffLongDelay = new ShowTooltipCommand(tooltipForOff, Constants.TooltipDelayAfterInteraction);
            hideTooltip = new HideTooltipCommand();

            SetToggle(IsOn);
        }

        internal void SetToggle(bool isOn)
        {
            IsOn = isOn;
            OnToggle?.Invoke(IsOn);

            if (Icon != null)
            {
                if (toggledOffSprite != null && toggledOnSprite != null)
                    Icon.sprite = IsOn ? toggledOnSprite : toggledOffSprite;
            }

            if (DisplayText != null)
            {
                if (toggledOffText != string.Empty && toggledOnText != string.Empty)
                    DisplayText.text = IsOn ? toggledOnText : toggledOffText;
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

            hideTooltip.Execute();

            if (IsOn)
                showTooltipForOnLongDelay.Execute();
            else
                showTooltipForOffLongDelay.Execute();
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            if (!interactable)
            {
                showTooltipNotInteractable.Execute();
                return;
            }

            if (IsOn)
                showTooltipForOnShortDelay.Execute();
            else
                showTooltipForOffShortDelay.Execute();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (interactable)
                DoStateTransition(IsOn ? SelectionState.Selected : SelectionState.Normal, false);

            hideTooltip.Execute();
        }

        public virtual void PlayToggleSound(bool isOn) { } // => AudioProvider.Instance.PlayButtonClick();
    }
}