using System;
using UnityEngine;

namespace Gameplay.Player.Controlls
{
    [System.Serializable]
    public class Touch
    {
        public Vector2 StartPosition { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        public Vector2 SwipingDirection { get; private set; }

        public bool WasActive { get; private set;}
        public int Id { get; }
        private Vector2 _lastPosition;

        private float _lastActivatedTime;
        private float _tapDelay;

        public event Action<Vector2> OnTouchStarted;
        public event Action<Vector2> OnTouchMoved;
        public event Action OnTouchEnded;

        /// <summary>
        /// Takes the tapped screen position as input
        /// </summary>
        public event Action<Vector2> OnTapped;
        public Touch(int id, float tapDelay)
        {
            CurrentPosition = default;
            StartPosition = default;
            WasActive = false;
            _tapDelay = tapDelay;
            
            Id = id;
        }

        public void UpdateTouchPosition(Vector2 position)
        {
            _lastPosition = CurrentPosition;
            CurrentPosition = position;
            OnTouchMoved?.Invoke(position);
        }

        public void UpdateTouchDirection()
        {
            SwipingDirection = (CurrentPosition - _lastPosition).normalized;
        }

        public void StartTouch(Vector2 position)
        {
            WasActive = true;
            _lastActivatedTime = Time.time;
            StartPosition = position;
            _lastPosition = StartPosition;
            OnTouchStarted?.Invoke(position);
        }

        public void EndTouch()
        {
            WasActive = false;
            OnTouchEnded?.Invoke();
            
            if (Time.time - _lastActivatedTime < _tapDelay)
            {
                OnTapped?.Invoke(CurrentPosition);
            }
        }
    }
}