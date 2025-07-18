using System;
using Gameplay.Building;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace TDEditor
{
    [CustomEditor(typeof(BezierCurve), true)]
    public class BezierCurveEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            BezierCurve bezierCurve = (BezierCurve)target;
            
            if (GUILayout.Button("SampleCurve"))
            {
                bezierCurve.Sample();    
            }
        }

        protected virtual void OnSceneGUI()
        {
            BezierCurve bezierCurve = (BezierCurve)target;
            Transform curveTransform = bezierCurve.transform;
            Quaternion handleRotation = curveTransform.rotation;

            Vector3 p0 = curveTransform.TransformPoint(bezierCurve.StartPoint);
            Vector3 ctrP0 = curveTransform.TransformPoint(bezierCurve.ControlPoint1);
            Vector3 ctrP1 = curveTransform.TransformPoint(bezierCurve.ControlPoint2);
            Vector3 p1 = curveTransform.TransformPoint(bezierCurve.EndPoint);
            
            EditorGUI.BeginChangeCheck();
            p0 = Handles.DoPositionHandle(p0, handleRotation);
            ctrP0 = Handles.DoPositionHandle(ctrP0, handleRotation);
            ctrP1= Handles.DoPositionHandle(ctrP1, handleRotation);
            p1 = Handles.DoPositionHandle(p1, handleRotation);
            
            bezierCurve.Sample();
            
            if (!EditorGUI.EndChangeCheck()) return;
            Undo.RecordObject(bezierCurve, "SampleCurve");
            EditorUtility.SetDirty(bezierCurve);
            
            bezierCurve.StartPoint = curveTransform.InverseTransformPoint(p0);
            bezierCurve.ControlPoint1 = curveTransform.InverseTransformPoint(ctrP0);
            bezierCurve.ControlPoint2 = curveTransform.InverseTransformPoint(ctrP1);
            bezierCurve.EndPoint = curveTransform.InverseTransformPoint(p1);
        }
    }
}
