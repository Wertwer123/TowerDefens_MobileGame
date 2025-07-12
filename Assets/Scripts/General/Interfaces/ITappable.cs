using UnityEngine;

namespace General.Interfaces
{
    public interface ITappable
    {
        public GameObject GetTappedObject();
        public void OnTapped();
    }
}