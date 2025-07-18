using System;
using UnityEditor;
using UnityEngine;

namespace Gameplay.Building
{
    public class BezierCurve : MonoBehaviour
    {
        [SerializeField] private Vector3 startPoint;
        [SerializeField] private Vector3 controlPoint1;
        [SerializeField] private Vector3 controlPoint2;
        [SerializeField] private Vector3 endPoint;
        [SerializeField, Min(0)] private int samples = 0;
        [SerializeField] protected bool debug = false;

        public Vector3 StartPoint { get => startPoint; set => startPoint = value; }
        public Vector3 ControlPoint1 { get => controlPoint1; set => controlPoint1 = value; }
        public Vector3 ControlPoint2 { get => controlPoint2; set => controlPoint2 = value; }
        public Vector3 EndPoint { get => endPoint; set => endPoint = value; }
        public int Samples { get => samples + 1;}
        public float SampleStep => 1.0f / samples; 
        
        protected Vector3[] _sampledPoints;
        

        public void Sample()
        {
            _sampledPoints = new Vector3[Samples];
            
            float currentSample = 0.0f;
            
            for (int i = 0; i < Samples; i++)
            {
                _sampledPoints[i] = GetPoint(currentSample);
                currentSample += SampleStep;
            }
        }

        public Vector3 GetPoint(int pointIndex)
        {
            return _sampledPoints[pointIndex];
        }
        public Vector3 GetPoint(float t)
        {
            Vector3 point = Mathf.Pow(1.0f - t, 3) * startPoint + 3 * Mathf.Pow(1 - t, 2) * t * controlPoint1 +
                            3 * (1 - t) * Mathf.Pow(t, 2) * controlPoint2 + Mathf.Pow(t, 3) * endPoint;
            return transform.TransformPoint(point);
        }
        
        protected virtual void OnDrawGizmos()
        {
            if (!debug)
            {
                return;
            }
            Handles.color = Color.black;
            if (_sampledPoints == null || _sampledPoints.Length == 0)
            {
                return;
            }
            
            for (int i = 0; i < _sampledPoints.Length - 1; i ++)
            {
                Handles.DrawLine(_sampledPoints[i], _sampledPoints[i + 1]);
            }

            for (int i = 0; i < _sampledPoints.Length; i++)
            {
                Gizmos.DrawSphere(_sampledPoints[i], 0.03f);
            }

            Handles.color = Color.aquamarine;
            Handles.DrawLine(transform.TransformPoint(controlPoint1), transform.TransformPoint(startPoint));
            Handles.DrawLine(transform.TransformPoint(controlPoint1), transform.TransformPoint(controlPoint2));
            Handles.DrawLine(transform.TransformPoint(controlPoint2), transform.TransformPoint(endPoint));
            Handles.DrawLine(transform.TransformPoint(controlPoint2), transform.TransformPoint(controlPoint1));
        }
    }
}
