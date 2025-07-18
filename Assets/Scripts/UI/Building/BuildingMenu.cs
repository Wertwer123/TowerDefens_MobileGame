using System;
using Gameplay.Building;
using Gameplay.Building.RootSystem;
using General.Base;
using General.Helper;
using General.Interfaces;
using UI.Tweens;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Building
{
   public class BuildingMenu : MonoBehaviour, IOpenable
   {
      [SerializeField] private Button openButton;
      [SerializeField] private GridLayoutGroup buildingGridLayoutGroup;
      [SerializeField] private BuildingDataBase dataBase;
      [SerializeField] private BuildingButton buildingDisplayPrefab;

      [Header("Animation")] 
      [SerializeField] private UITween tween;

      [SerializeField]private RootSocket _selectedRootSocket;
      
      public void Start()
      {
         openButton.onClick.AddListener(Open);
         EventBus.Instance.OnRootSocketTapped += (tappedRootSocket) =>
         {
            if (tappedRootSocket.IsOccupied)
            {
               return;
            }
            if (_selectedRootSocket == tappedRootSocket)
            {
               Close();
               _selectedRootSocket = null;
               return;
            }
            
            Open();
            InitializeBuildings(tappedRootSocket);
            _selectedRootSocket = tappedRootSocket;
         };
         EventBus.Instance.OnBuildingPlacedOnSocket += (_) =>
         {
            Close();
         };
      }

      public bool IsOpen { get; set; } = false;

      public void InitializeBuildings(RootSocket tappedRootSocket)
      {
         ClearBuildingGrid();

         foreach (var buildingData in dataBase.GetBuildingsFilteredByType(tappedRootSocket.PlaceableTypes))
         {
            var instance = Instantiate(buildingDisplayPrefab, buildingGridLayoutGroup.transform);
            instance.Initialize(buildingData, tappedRootSocket);
         }
      }

      void ClearBuildingGrid()
      {
         for (int i = 0; i < buildingGridLayoutGroup.transform.childCount; i++)
         {
            Destroy(buildingGridLayoutGroup.transform.GetChild(i).gameObject);
         }
      }
      
      public void Open()
      {
         if(IsOpen) return;
         
         tween.PlayTween(this, false);
         buildingGridLayoutGroup.enabled = true;
         IsOpen = true;
         
         openButton.onClick.RemoveListener(Open);
         openButton.onClick.AddListener(Close);
      }

      public void Close()
      {
         if(!IsOpen) return;
         
         tween.PlayTween(this, true);
         
         IsOpen = false;
         
         openButton.onClick.RemoveListener(Close);
         openButton.onClick.AddListener(Open);
      }
   }
}
