using UnityEngine;

namespace General.Helper
{
    /// <summary>
    /// Tweens should be done in local space as they are always childed to something and it makes stuff more handy
    /// </summary>
    [System.Serializable]
    public struct TweenTransform
    {
        [SerializeField] Vector3 position;
        [SerializeField] Vector3 eulerAngles;
        [SerializeField] Vector3 scale;
        
        public Vector3 Position { get => position;}
        public Vector3 EulerAngles { get => eulerAngles;}
        public Vector3 Scale { get => scale;}
        
    }
}