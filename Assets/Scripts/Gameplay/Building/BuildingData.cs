using General.Enums;
using UnityEngine;

namespace Gameplay.Building
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Building/New Building")]
    public  class BuildingData : ScriptableObject
    {
        [SerializeField] protected Sprite uiSprite;
        [SerializeField] protected GameObject buildingPrefab;
        [SerializeField] private string buildingName;
        [SerializeField] private BuildableType buildingType;
        
        public Sprite UISprite => uiSprite;
        public GameObject BuildingPrefab => buildingPrefab;
        public string BuildingName => buildingName;
        public BuildableType BuildingType => buildingType;
        
    }
}