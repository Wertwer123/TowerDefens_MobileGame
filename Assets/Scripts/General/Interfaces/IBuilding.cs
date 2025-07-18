using Gameplay.Building;
using General.Enums;
using UnityEngine;

namespace General.Interfaces
{
    public interface IBuilding
    {
        public void OnBuilt(SocketLocation placedOnSocket);
        public BuildingData GetBuildingData();
        public GameObject GetOwningGameObject();
    }
}