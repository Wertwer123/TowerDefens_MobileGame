using System.Collections.Generic;
using System.Linq;
using General.Enums;
using UnityEngine;

namespace Gameplay.Building
{
    [CreateAssetMenu(fileName = "New DataBase", menuName = "Building/New BuildingDataBase")]
    public class BuildingDataBase : ScriptableObject
    {
        [SerializeField] List<BuildingData> buildings = new List<BuildingData>();

        public List<BuildingData> GetBuildingsFilteredByType(BuildableType buildingType)
        {
            List<BuildingData> filteredBuildings = new List<BuildingData>();
            
            foreach (BuildingData buildingData in buildings)
            {
                if ((buildingType & buildingData.BuildingType) != 0)
                {
                    filteredBuildings.Add(buildingData);
                }
            }

            return filteredBuildings;
        }
    }
}