using System;
using System.Collections.Generic;
using General.Base;
using General.Enums;
using General.Interfaces;
using UnityEngine;

namespace Gameplay.Building.RootSystem
{
    public class TreeRoot : MonoBehaviour, IBuilding
    {
        [SerializeField] private BuildingData buildingData;
        [SerializeField] private List<RootSocket> treeSockets;
        [SerializeField] private TreeRoot nextTreeRoot = null;
        [SerializeField] private TreeRoot previousTreeRoot = null;

        private void Start()
        {
            InitializeSockets();
        }

        void InitializeSockets()
        {
            foreach (RootSocket rootSocket in treeSockets)
            {
                rootSocket.InitSocket(this);
            }
        }

        public void SetPreviousTreeRoot(TreeRoot treeRoot)
        {
            previousTreeRoot = treeRoot;
        }

        public void SetNextTreeRoot(TreeRoot treeRoot)
        {
            nextTreeRoot = treeRoot;   
        }

        public void DisableSocketAtLocation(SocketLocation socketLocation)
        {
            treeSockets.Find(treeSocket => treeSocket.SocketLocation == socketLocation)?.DisableSocket();
        }

        public void OnBuilt(SocketLocation placedOnSocket)
        {
            DisableSocketsBasedOnTreeRootLocation(placedOnSocket);
            //Do an extra check in here where we check all non disabled sockets if they would overlap with anything else and if so deactivate them
            DisableSocketsThatOverlapOtherSockets();
        }

        /// <summary>
        /// Checks based on bound if the socket overlaps another socket and then disables this socket if so
        /// </summary>
        private void DisableSocketsThatOverlapOtherSockets()
        {
            foreach (RootSocket rootSocket in treeSockets)
            {
                Bounds bounds = rootSocket.RootSocketCollider.bounds;
                Collider[] results = new Collider[12];
                var size = Physics.OverlapBoxNonAlloc(bounds.center, bounds.extents, results, rootSocket.transform.rotation, rootSocket.gameObject.layer);
               
                for (int i = 0; i < size; i++)
                {
                    //Fix this logic doesnt dissable correctly need to go see sisters aa
                    Collider hitCollider = results[i];
                    if (rootSocket.RootSocketCollider != hitCollider && !treeSockets.Find(treeSocket => treeSocket.RootSocketCollider == hitCollider))
                    {
                        rootSocket.DisableSocket();
                    }
                }
            }
        }

        void DisableSocketsBasedOnTreeRootLocation(SocketLocation socketLocation)
        {
            switch (socketLocation)
            {
                case SocketLocation.Top:
                    DisableSocketAtLocation(SocketLocation.Front);
                    DisableSocketAtLocation(SocketLocation.Bottom);
                    break;
                case SocketLocation.Bottom:
                    DisableSocketAtLocation(SocketLocation.Top);
                    DisableSocketAtLocation(SocketLocation.Front);
                    break;
                case SocketLocation.Left:
                case SocketLocation.Right:
                case SocketLocation.Front:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(socketLocation), socketLocation, null);
            }
        }

        public BuildingData GetBuildingData()
        {
            return buildingData;
        }

        public GameObject GetOwningGameObject()
        {
            return gameObject;
        }
    }
}
