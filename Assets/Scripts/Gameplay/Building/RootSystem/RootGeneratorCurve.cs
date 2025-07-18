using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using General.Base;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Building.RootSystem
{
    public class RootGeneratorCurve : BezierCurve
    {
        [SerializeField] private MeshFilter mesh;
        [SerializeField] private string meshFileName;
        [SerializeField, Min(0.1f)] private float rootThickness;
        [SerializeField, Min(4)] private int meshSegments;

        [SerializeField] private List<Vector3> rootMeshVertices = new List<Vector3>();
        [SerializeField, HideInInspector]private List<RootSegment> rootSegments = new List<RootSegment>();
        
        public List<RootSegment> GetRootSegments() => rootSegments;
        
        public void SampleRootMesh()
        {
            rootMeshVertices.Clear();
            rootSegments.Clear();
            Sample();
            //Sample a circle around each point of the curve
            int meshVertIndex = 0;
            for (int sampleIndex = 0; sampleIndex < Samples; sampleIndex++)
            {
                //thats our circle rotation axis to sample points
                bool isLastIteration = sampleIndex == Samples - 1;
                int nextPointIndex = isLastIteration ? sampleIndex - 1 : sampleIndex + 1;
                Vector3 currentPoint = GetPoint(sampleIndex);
                Vector3 nextPoint = GetPoint(nextPointIndex);
                Vector3 directionToNextPoint = (currentPoint - nextPoint).normalized;
                if (isLastIteration)
                {
                    directionToNextPoint = -directionToNextPoint;
                }
                Vector3 rightVector =  Vector3.Cross(directionToNextPoint, Vector3.up);
                
                float circleStep = 360.0f / meshSegments;

                RootSegment segment = new ();
                //rotate the vector meshsegments time around the forward axis
                for (int j = 0; j < meshSegments; j++)
                {
                    Vector3 point = isLastIteration ? currentPoint : currentPoint +
                                    (Quaternion.AngleAxis(circleStep * j, directionToNextPoint)) * rightVector * rootThickness;
                    
                    Vertice vertice = new (point, meshVertIndex);
                    rootMeshVertices.Add(point);
                    segment.AddVertice(vertice);
                    meshVertIndex++;
                }
                
                rootSegments.Add(segment);
            }
            
            TriangulateRoot();
        }

        private void TriangulateRoot()
        {
            List<int> trianglesForTube = new List<int>();
            Mesh newRootMesh = new();
            
            for (int i = 0; i < rootSegments.Count - 1; i++)
            {
                RootSegment segment = rootSegments[i];
                RootSegment nextSegment = rootSegments[i + 1];
                List<int> triangles = segment.ConnectSegments(nextSegment);
                
                trianglesForTube.AddRange(triangles);
            }
            
            newRootMesh.vertices = rootMeshVertices.Select(point => transform.InverseTransformPoint(point)).ToArray();
            newRootMesh.triangles = trianglesForTube.ToArray();
            newRootMesh.RecalculateNormals();
            newRootMesh.RecalculateBounds();
            newRootMesh.RecalculateTangents();
            mesh.sharedMesh = newRootMesh;
            
            SaveMeshToToFile(mesh.sharedMesh, meshFileName);
        }

        void SaveMeshToToFile(Mesh mesh, string fileName)
        {
            if (!AssetDatabase.IsValidFolder("Assets/GeneratedTreeRootMeshes"))
            {
                AssetDatabase.CreateFolder("Assets", "GeneratedTreeRootMeshes");
            }
            
            AssetDatabase.CreateAsset(mesh, "Assets/GeneratedTreeRootMeshes/" + fileName + ".asset");
            AssetDatabase.Refresh();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if (!debug)
            {
                return;
            }
            Gizmos.color = Color.yellow;

            for (int i = 0; i < rootMeshVertices.Count; i++)
            {
                Gizmos.DrawSphere(rootMeshVertices[i], 0.05f);
            }
        }
    }
   
}