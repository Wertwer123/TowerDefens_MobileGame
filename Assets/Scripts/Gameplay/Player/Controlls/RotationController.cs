using System;
using General.Enums;
using General.Helper;
using UI.BaseOverrides;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Player.Controlls
{
    public class RotationController : MonoBehaviour
    {
        [SerializeField] BaseButton leftRotationButton;
        [SerializeField] BaseButton rightRotationButton;
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform cameraTransform;
        [SerializeField] private Vector3 rotationCenter;
        [SerializeField] LayerMask groundLayer;
        [SerializeField, Min(0.0f)] float rotationSpeed;

        private float _currentRotationAngle = 0.0f;
        
            
        private void Start()
        {
            //init the statring position of the camera
        }

        private void Update()
        {
            if (leftRotationButton.IsButtonPressed)
            {
                Rotate(RotationDirection.Left);
            }

            if (rightRotationButton.IsButtonPressed)
            {
                Rotate(RotationDirection.Right);
            }
        }

        private void FixedUpdate()
        {
            SetRotationCenter();
        }

        void SetRotationCenter()
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 startPosition = playerTransform.position;
            
            bool hit = Physics.Raycast(startPosition, forward, out RaycastHit hitInfo, float.MaxValue,groundLayer);

            if (hit)
            {
                rotationCenter = hitInfo.point;
            }
        }

        void Rotate(RotationDirection direction)
        {
            //Perform pythagoras the find the x distance between camera and center
            float hypotenuse = Vector3.Distance(playerTransform.position, rotationCenter);
            float yDistance = playerTransform.position.y - rotationCenter.y;
            float xDistance = Mathf.Sqrt(Mathf.Pow(hypotenuse, 2) - Mathf.Pow(yDistance, 2));
            Vector3 rotationCenterWithPlayerHeight = new (rotationCenter.x, playerTransform.position.y, rotationCenter.z);

            switch (direction)
            {
                case RotationDirection.Right:
                    _currentRotationAngle -= rotationSpeed * Time.deltaTime;
                    playerTransform.position = rotationCenter +  MathHelper.GetPositionOnCircleByDegrees(_currentRotationAngle,
                        xDistance, playerTransform.position.y);
                    playerTransform.forward = (rotationCenterWithPlayerHeight - cameraTransform.position).normalized;
                    break;
                case RotationDirection.Left:
                    _currentRotationAngle += rotationSpeed * Time.deltaTime;
                    playerTransform.position = rotationCenter + MathHelper.GetPositionOnCircleByDegrees(_currentRotationAngle,
                        xDistance, playerTransform.position.y);
                    playerTransform.forward = (rotationCenterWithPlayerHeight - cameraTransform.position).normalized;
                    break;
            }
        }
    }
}