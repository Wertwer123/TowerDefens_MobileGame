using System;
using UnityEngine;

namespace Gameplay.Player.Controlls
{
    [System.Serializable]
    public class Touch
    {
        public Vector2 StartPosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }

        public bool WasActive;
        public int Id { get; }
        
        public Action<Vector2> OnTouchStarted { get; set; } = null;
        public Action<Vector2> OnTouchMoved { get; set; } = null;
        public Action OnTouchEnded { get; set; } = null;
        public Touch(int id)
        {
            CurrentPosition = default;
            StartPosition = default;
            WasActive = false;
            Id = id;
        }

        public void UpdateTouchPosition(Vector2 position)
        {
            CurrentPosition = position;
            OnTouchMoved?.Invoke(position);
        }

        public void StartTouch(Vector2 position)
        {
            WasActive = true;
            StartPosition = position;
            OnTouchStarted?.Invoke(position);
        }

        public void EndTouch()
        {
            WasActive = false;
            OnTouchEnded?.Invoke();
        }
    }
}