using Gameplay.Building;
using Gameplay.Building.RootSystem;
using General.Interfaces;
using UI.BaseOverrides;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Building
{
    public class BuildingButton : MonoBehaviour
    {
        [SerializeField] BaseButton buildingButton;
        //ad this to the building menu and create a prefab for this
        private Sprite _buildingSprite;
        private BuildingData _buildingData;
        private RootSocket _contextSocket;
        
        public void Initialize(BuildingData buildingData, RootSocket contextSocket)
        {
            _buildingSprite = buildingData.UISprite;
            _buildingData = buildingData;
            _contextSocket = contextSocket;
            
            buildingButton.image.sprite = _buildingSprite;
            buildingButton.onClick.AddListener(PlaceBuildingOnSocket);
        }

        private void PlaceBuildingOnSocket()
        {
            var placedBuildingInstance = Instantiate(_buildingData.BuildingPrefab, _contextSocket.SocketTransform.position, _contextSocket.SocketTransform.rotation);
            _contextSocket.OccupySocket(placedBuildingInstance.GetComponent<IBuilding>());
        }
    }
}