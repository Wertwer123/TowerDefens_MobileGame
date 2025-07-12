using System;
using UnityEngine;

namespace Gameplay.Building.RootSystem
{
    public class TreeRoot : MonoBehaviour
    {
        [SerializeField] private RootSocket[] treeSockets;
        [SerializeField] TreeRoot nextTreeRoot = null;
        [SerializeField] TreeRoot previousTreeRoot = null;

        private void Start()
        {
            InitializeSockets();
        }

        void InitializeSockets()
        {
            foreach (RootSocket rootSocket in treeSockets)
            {
                rootSocket.OnSocketClicked += OnTreeSocketClicked;
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
        
        private void OnTreeSocketClicked(RootSocket socket)
        {
            //Replace this by actually making th ebuilding window popup from the side 
        }
    }
}
