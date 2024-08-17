using UnityEngine;

namespace BasePrefabs
{
    /// <summary>
    /// Util class containing useful extensions
    /// </summary>
    public static class UtilExtensions
    {
    
        /// <summary>
        /// Simple generic function for determining if a particular component is on a GameObject. 
        /// </summary>
        /// <param name="obj">The GameObject</param>
        /// <typeparam name="T">The component object to look for</typeparam>
        /// <returns>True if the gameobject contains the component</returns>
        public static bool HasComponent <T>(this GameObject obj) where T:Component
        {
            return obj.GetComponent<T>() != null;
        }
    
        /// <summary>
        /// Simple function for determining if two values are somewhat close
        /// </summary>
        /// <param name="ff">Num 1</param>
        /// <param name="target">Num 2</param>
        /// <returns>True if they are somewhat near</returns>
        public static bool IsNear(this float ff, float target)
        {
            float difference = ff-target;
            difference = Mathf.Abs(difference);
            if ( difference < 20f ) return true;
            else return false;
        }
    }
}