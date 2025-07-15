using General.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Building
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Building/New Building")]
    public  class BuildingData : ScriptableObject
    {
        [SerializeField] protected Sprite uiSprite;
        [SerializeField] private string buildingName;
        [SerializeField] private BuildableType buildingType;
        
        public Sprite UISprite => uiSprite;
        public string BuildingName => buildingName;
        public BuildableType BuildingType => buildingType;
        
    }
}