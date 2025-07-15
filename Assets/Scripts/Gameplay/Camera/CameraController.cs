using Gameplay.Player.Controlls;
using General.Enums;
using UnityEngine;

namespace Gameplay.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera cam;
        [SerializeField] private TouchController touchController;
        [SerializeField] private float zoomSpeed = 1.5f;
        [SerializeField, Range(0,100.0f)] private float maxFov = 1.5f;
        [SerializeField, Range(0,100f)] private float minFov = 1.5f;

        void Start()
        {
            touchController.OnZoom += Zoom;
        }
        
        void Zoom(Zoom zoom, float zoomedDistance)
        {
            if (zoom == General.Enums.Zoom.In)
            {
                cam.fieldOfView -= zoomedDistance * zoomSpeed;
            }
            else if (zoom == General.Enums.Zoom.Out)
            {
                cam.fieldOfView += zoomedDistance * zoomSpeed;
            }
            
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFov, maxFov);
        }
    }
}