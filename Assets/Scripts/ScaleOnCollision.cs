using pattern;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScaleOnCollision : CollisionObserver
    {
        [SerializeField] private float scaleFactorX = 2.0f;
        [SerializeField] private float scaleFactorY = 1.5f;
        [SerializeField] private float scaleFactorZ = 1f; // we'll just leave it at one, it's a 2d game.

        protected override void Scale()
        {
            var prevX = gameObject.transform.localScale.x;
            var prevY = gameObject.transform.localScale.y;
            var prevZ = gameObject.transform.localScale.z;
            gameObject.transform.localScale =
                new Vector3(prevX * scaleFactorX, prevY * scaleFactorY, prevZ * scaleFactorZ);
        }
    }
}