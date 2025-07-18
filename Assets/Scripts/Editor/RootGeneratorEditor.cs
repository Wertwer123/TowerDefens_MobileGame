using System;
using Gameplay.Building.RootSystem;
using UnityEditor;
using UnityEngine;

namespace TDEditor
{
    [CustomEditor(typeof(RootGeneratorCurve))]
    public class RootGeneratorEditor : BezierCurveEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            RootGeneratorCurve rootGeneratorCurve = (RootGeneratorCurve)target;
            
            if (GUILayout.Button("Generate Root Mesh"))
            {
                rootGeneratorCurve.SampleRootMesh();
            }
        }
    }
}