using System.Collections;
using Player.Controlls;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Gameplay.Player.Controlls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private TouchController touchController;
        [SerializeField, Min(0.0f)] private float speed;
        [SerializeField, Min(0.0f)] private float movementStartingDistance;
        
        private Vector2 _startTouchPosition;
        private Vector3 _movementVector;
        
        void Start()
        {
            SetupInput();
        }

        private void SetupInput()
        {
            touchController.BindEventToOnTouchMoved(0, MovePlayer);
           touchController.BindEventToTouchStarted(0, OnStartMovePlayer);
        }

        void OnStartMovePlayer(Vector2 startTouchPosition)
        {
            _startTouchPosition = startTouchPosition;
        }
        
        void MovePlayer(Vector2 currentTouchPosition)
        {
            if (touchController.ActiveTouches != 1)
                return;
            
            Vector2 touchPosition = currentTouchPosition;
            if (Vector2.Distance(touchPosition, _startTouchPosition) > movementStartingDistance)
            {
                Vector2 direction = (touchPosition - _startTouchPosition).normalized;
                _movementVector = new Vector3(direction.x, 0, direction.y) * (speed * Time.deltaTime);
                transform.Translate(_movementVector, Space.World);
            }
            else
            {
                _movementVector = Vector2.zero;
            }
        }
    }
}
