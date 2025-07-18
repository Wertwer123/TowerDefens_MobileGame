using UnityEngine;

namespace General.Base
{
    public struct Vertice
    {
        public readonly Vector3 position;
        public readonly int id;

        public Vertice(Vector3 position, int id)
        {
            this.position = position;
            this.id = id;
        }
    }
}