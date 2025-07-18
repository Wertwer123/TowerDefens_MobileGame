using System.Collections.Generic;
using General.Base;

namespace Gameplay.Building.RootSystem
{
     [System.Serializable]
    public class RootSegment
    {
        private List<Vertice> Vertices = new List<Vertice>();
        
        public List<Vertice> GetVertices() => Vertices;

        public void AddVertice(Vertice vertice)
        {
            Vertices.Add(vertice);
        }

        public Vertice GetVertice(int index)
        {
            return Vertices[index];
        }

        /// <summary>
        /// Connects two circle segments by returning a list of the formed triangles
        /// </summary>
        /// <returns></returns>
        public List<int> ConnectSegments(RootSegment rootSegment)
        {
            List<int> connectedSegments = new List<int>();

            for (int i = 0; i < Vertices.Count; i++)
            {
                int nextVerticeIndex = i == Vertices.Count - 1 ? 0 : i + 1;
                
                Vertice currentVerticeToConnect = Vertices[i];
                Vertice nextVerticeToConnect = Vertices[nextVerticeIndex];
                Vertice otherSegmentVerticeToConnect = rootSegment.GetVertice(i);
                Vertice otherSegmentNextVerticeToConnect = rootSegment.GetVertice(nextVerticeIndex);
                
                connectedSegments.Add(currentVerticeToConnect.id);
                connectedSegments.Add(otherSegmentVerticeToConnect.id);
                connectedSegments.Add(nextVerticeToConnect.id);
                connectedSegments.Add(otherSegmentVerticeToConnect.id);
                connectedSegments.Add(otherSegmentNextVerticeToConnect.id);
                connectedSegments.Add(nextVerticeToConnect.id);
            }

            return connectedSegments;
        }
    }
}