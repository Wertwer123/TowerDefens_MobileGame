using UnityEngine;

namespace General.Helper
{
    public struct MathHelper
    {
        public static Vector3 GetPositionOnCircleByDegrees(float degrees, float radius, float height)
        {
            float x = radius * Mathf.Sin(degrees);
            float y = radius * Mathf.Cos(degrees);
            
            return new Vector3(x, height , y);
        }
    }
}