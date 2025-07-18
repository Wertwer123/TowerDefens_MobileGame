using System;
using System.Collections.Generic;
using General;
using General.Enums;
using General.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Player.Controlls
{
    public sealed class TouchController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera mainCamera;
        [SerializeField] private List<Touch> touches = new List<Touch>();
        [SerializeField] private LayerMask tappableLayer;
        [SerializeField, Range(0, 10)] private int supportedTouches = 2;
        [SerializeField, Min(0.0f)] float startZoomThreshold = 10.0f;   
        [SerializeField, Min(0.0f)] float tapDelay = 0.2f;   

        private int _activeTouches = 0;
        public int ActiveTouches => _activeTouches;
        /// <summary>
        /// Takes as input if its zooming out or in and the distance that has been zoomed 
        /// </summary>
        public event Action<Zoom, float> OnZoom;
        
        private void Awake()
        {
            for (int i = 0; i < supportedTouches; i++)
            {
                Touch touchToAdd = new Touch(i, tapDelay);
                touches.Add(touchToAdd);
                touchToAdd.OnTapped += PerformTap;
            }    
        }

        private void Update()
        {
            UpdateTouches();
            CheckForZoom();
        }
        
        public void BindEventToOnTouchMoved(int index, Action<Vector2> onTouchMoved)
        {
            touches[index].OnTouchMoved += onTouchMoved;
        }
        public void BindEventToTouchStarted(int index, Action<Vector2> onTouchStart)
        {
            touches[index].OnTouchStarted += onTouchStart;
        }
        
        public bool IsTouchActive(int touchId)
        {
            return touches[touchId].WasActive;
        }
        public Vector3 GetTouchCurrentPosition(int index)
        {
            return touches[index].CurrentPosition;
        }

        public Vector3 GetTouchStartPosition(int index)
        {
            return touches[index].StartPosition;
        }

        private void PerformTap(Vector2 position)
        {
           
            //Shoot a ray at the tapped positon
            Ray ray = mainCamera.ScreenPointToRay(position);
            bool tappedSomething = Physics.Raycast(ray, out RaycastHit hit, float.MaxValue,tappableLayer);

            if (!tappedSomething)
            {
                return;
            }
            if (hit.transform.gameObject.TryGetComponent(out ITappable tappedObject))
            {
                tappedObject.OnTapped();
            }
        }
        
        private Vector3 GetTouchSwipeDirection(int index)
        {
            return touches[index].SwipingDirection;
        }
        
        void CheckForZoom()
        {
            //Two fingers on the screen
            if (_activeTouches == 2)
            {
                Vector3 touch1SwipingDirection = GetTouchSwipeDirection(0);
                Vector3 touch2SwipingDirection = GetTouchSwipeDirection(1);
                Vector3 currentTouchPosition1 = GetTouchCurrentPosition(0);
                Vector3 currentTouchPosition2 = GetTouchCurrentPosition(1);
                
                float distanceBetweenTwoTouches = Vector2.Distance(currentTouchPosition1, currentTouchPosition2);
                // if these two vectors are moving towards another zoom out if otherwise zoom in
                Vector3 nextPositionTouch1 = currentTouchPosition1 + touch1SwipingDirection;
                Vector3 nextPositionTouch2 = currentTouchPosition2 + touch2SwipingDirection;
                
                float distanceBetweenNextPositions = Vector2.Distance(nextPositionTouch1, nextPositionTouch2);
                float zoomedDistance = Mathf.Abs(distanceBetweenNextPositions - distanceBetweenTwoTouches);
                if (zoomedDistance < startZoomThreshold)
                {
                    return;
                }
                
                if (distanceBetweenNextPositions < distanceBetweenTwoTouches)
                {
                    OnOnZoom(Zoom.Out, zoomedDistance);
                }
                else if(distanceBetweenNextPositions > distanceBetweenTwoTouches)
                {
                    OnOnZoom(Zoom.In, zoomedDistance);
                }
            }
        }
        
        private void UpdateTouches()
        {
            _activeTouches = Input.touchCount;
            if (_activeTouches > supportedTouches)
            {
                return;
            }
            
            //Start each touch if it wasnt started and update its position
            for (int i = 0; i < _activeTouches; i++)
            {
                Touch touch = touches[i];
                UnityEngine.Touch unityTouch = Input.GetTouch(i);
               
                if (!touch.WasActive)
                {
                    touch.StartTouch(unityTouch.position);
                }
                
                touch.UpdateTouchPosition(unityTouch.position);
                touch.UpdateTouchDirection();
            }
            
            //disable all not actiavated touches
            for (int i = touches.Count - 1; i >= _activeTouches; i--)
            {
                Touch touch = touches[i];
                if (touch.WasActive)
                {
                    touch.EndTouch();
                }
            }
        }

        private void OnOnZoom(Zoom zoom, float zoomDistance)
        {
            OnZoom?.Invoke(zoom, zoomDistance);
        }
    }
}