using System;
using System.Net.Sockets;
using General.Base;
using General.Enums;
using General.Interfaces;
using UnityEngine;

namespace Gameplay.Building.RootSystem
{
    [RequireComponent(typeof(Collider))]
    public class RootSocket : MonoBehaviour, ITappable
    {
        [SerializeField] private bool isOccupied = false;
        [SerializeField] private Transform socketTransform;
        [SerializeField] private SocketLocation socketLocation;
        [SerializeField] private BuildableType placeableTypes;
        [SerializeField] private MeshCollider rootSocketCollider;
        
        private IBuilding _buildingInstance;
        private TreeRoot _owningTreeRoot;
        
        public bool IsOccupied => isOccupied;
        public BuildableType PlaceableTypes => placeableTypes;
        public SocketLocation SocketLocation => socketLocation;
        public Collider RootSocketCollider => rootSocketCollider;
        public Transform SocketTransform => socketTransform;

        public void InitSocket(TreeRoot owningTreeRoot)
        {
            _owningTreeRoot = owningTreeRoot;
        }

        public void DisableSocket()
        {
            gameObject.SetActive(false);
        }
        //WIll be exchanged by the actual building
        public void OccupySocket(IBuilding newlyBuiltBuilding)
        {
            _buildingInstance = newlyBuiltBuilding;
            _buildingInstance.OnBuilt(socketLocation);
            isOccupied = true;
            
            EventBus.Instance.SendBuildingPlacedOnSocket(this);
            DisableSocket();
        }
        
        public GameObject GetTappedObject()
        {
            return gameObject;
        }

        public void OnTapped()
        {
            EventBus.Instance.SendRootSocketTapped(this);
        }
    }
}