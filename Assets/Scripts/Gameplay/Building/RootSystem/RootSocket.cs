using System;
using General.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Building.RootSystem
{
    [RequireComponent(typeof(Collider))]
    public class RootSocket : MonoBehaviour, ITappable
    {
        [SerializeField] private bool isOccupied = false;
        [SerializeField] private GameObject builtBuilding;

        private TreeRoot _owningTreeRoot;
        
        public event Action<RootSocket> OnSocketClicked;
        public bool IsOccupied => isOccupied;

        public void InitSocket(TreeRoot owningTreeRoot)
        {
            _owningTreeRoot = owningTreeRoot;
        }
        
        //WIll be exchanged by the actual building
        public void OccupySocket(GameObject newlyBuiltBuilding)
        {
                this.builtBuilding = newlyBuiltBuilding;
        }
        
        public GameObject GetTappedObject()
        {
            return gameObject;
        }

        public void OnTapped()
        {
            Debug.Log("OnTapped");
            OnSocketClicked?.Invoke(this);
        }
    }
}