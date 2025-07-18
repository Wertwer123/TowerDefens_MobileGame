using System;
using Gameplay.Building.RootSystem;

namespace General.Base
{
    /// <summary>
    /// A global container to map to general gameplay event s that are of  global interrest
    /// All global events van then be funneled through this event bus so that we dont have any direct dependencies for them
    /// </summary>
    public class EventBus : Singleton<EventBus>
    {
        public event Action<RootSocket> OnRootSocketTapped;
        public event Action<RootSocket> OnBuildingPlacedOnSocket;

        public void SendRootSocketTapped(RootSocket socket)
        {
            OnRootSocketTapped?.Invoke(socket);
        }

        public void SendBuildingPlacedOnSocket(RootSocket socket)
        {
            OnBuildingPlacedOnSocket?.Invoke(socket);
        }
    }
}