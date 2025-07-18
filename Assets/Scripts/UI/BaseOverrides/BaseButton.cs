using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.BaseOverrides
{
    public class BaseButton : Button
    {
        public event Action OnSelected;
        public event Action OnDeselected;
        public event Action OnPressed;
        public event Action OnUnpressed;
        
        public bool IsButtonPressed { get; private set; }

        public override void OnSelect(BaseEventData eventData)
        {
            OnSelected?.Invoke();
            base.OnSelect(eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            OnDeselected?.Invoke();
            base.OnDeselect(eventData);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            IsButtonPressed = true;
            OnPressed?.Invoke();
            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            IsButtonPressed = false;
            OnUnpressed?.Invoke();
            base.OnPointerUp(eventData);
        }
    }
}