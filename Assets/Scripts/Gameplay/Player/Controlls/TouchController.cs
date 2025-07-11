using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player.Controlls
{
    public sealed class TouchController : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private int supportedTouches = 2;
        [SerializeField] private List<Touch> _touches = new List<Touch>();

        private int _activeTouches = 0;
        public int ActiveTouches => _activeTouches;
        
        private void Awake()
        {
            for (int i = 0; i < supportedTouches; i++)
            {
                _touches.Add(new Touch(i));
            }    
        }

        private void Update()
        {
            UpdateTouches();
        }
        
        public void BindEventToOnTouchMoved(int index, Action<Vector2> onTouchMoved)
        {
            _touches[index].OnTouchMoved += onTouchMoved;
        }
        public void BindEventToTouchStarted(int index, Action<Vector2> onTouchStart)
        {
            _touches[index].OnTouchStarted += onTouchStart;
        }
        
        public bool IsTouchActive(int touchId)
        {
            return _touches[touchId].WasActive;
        }
        public Vector3 GetTouchCurrentPosition(int index)
        {
            return _touches[index].CurrentPosition;
        }

        public Vector3 GetTouchStartPosition(int index)
        {
            return _touches[index].StartPosition;
        }
        
        private void UpdateTouches()
        {
            _activeTouches = Input.touchCount;
            
            //Start each touch if it wasnt started and update its position
            for (int i = 0; i < _activeTouches; i++)
            {
                Touch touch = _touches[i];
                UnityEngine.Touch unityTouch = Input.GetTouch(i);
                Debug.Log(touch.WasActive);
                if (!touch.WasActive)
                {
                    touch.StartTouch(unityTouch.position);
                }
                

                touch.UpdateTouchPosition(unityTouch.position);
            }
            
            //disable all not actiavated touches
            for (int i = _touches.Count - 1; i >= _activeTouches; i--)
            {
                Touch touch = _touches[i];
                Debug.Log("disabled touch : " +  i);
                if (touch.WasActive)
                {
                    touch.EndTouch();
                }
            }
        }
        
    }
}