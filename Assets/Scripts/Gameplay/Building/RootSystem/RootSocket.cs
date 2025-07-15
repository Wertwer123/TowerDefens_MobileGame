using System;
using General.Base;
using General.Enums;
using General.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Building.RootSystem
{
    [RequireComponent(typeof(Collider))]
    public class RootSocket : MonoBehaviour, ITappable
    {
        [SerializeField] private bool isOccupied = false;
        [SerializeField] private bool isLocked = false;
        [SerializeField] private GameObject builtBuilding;
        [SerializeField] private BuildableType placeableTypes;

        private TreeRoot _owningTreeRoot;
        
        public event Action<RootSocket> OnSocketClicked;
        public bool IsOccupied => isOccupied;
        public bool IsLocked => isLocked;
        public BuildableType PlaceableTypes => placeableTypes;

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
            EventBus.Instance.SendRootSocketTapped(this);
            OnSocketClicked?.Invoke(this);
        }
    }
}