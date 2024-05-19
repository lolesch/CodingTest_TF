﻿using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodingTest.Runtime.UI.Buttons
{
    public abstract class AbstractButton : Selectable, IPointerClickHandler
    {
        // TODO: disable the button for x seconds to disable button spaming

        protected abstract void OnLeftClick();
        protected abstract void OnRightClick();

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable || eventData.dragging)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                OnLeftClick();
            else if (eventData.button == PointerEventData.InputButton.Right)
                OnRightClick();
        }
    }
}