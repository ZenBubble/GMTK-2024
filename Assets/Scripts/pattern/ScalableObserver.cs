using System;
using UnityEngine;

namespace pattern
{
    /// <summary>
    ///     A class that subscribes to a certain CollisionSubject, and gets scaled by scaling factors
    ///     when the CollisionSubjects collides with something.
    ///     TODO: look into collision matrices to limit the calls to OnNext.
    ///     TODO: look into non-linear transformations of objects?
    /// </summary>
    public class ScalableObserver : MonoBehaviour, IObserver<Collision>
    {
        [SerializeField] private CollisionSubject collisionSubscription;
        [SerializeField] private float scaleFactorX = 2.0f;
        [SerializeField] private float scaleFactorY = 1.5f;
        [SerializeField] private float scaleFactorZ = 1f; // we'll just leave it at one, it's a 2d game.

        /// <summary>
        ///     Subscribes the assigned gameObject to the collisionSubscription in the editor.
        /// </summary>
        private void Start()
        {
            collisionSubscription.Subscribe(this);
        }


        /// <summary>
        ///     Unimplemented - no use for OnCompleted for now, our object lifetime is the entire game!
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Unimplemented - no use for OnError for now, there shouldn't be a way to error out our objects
        /// </summary>
        /// <param name="error"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Observer pattern, when there's a collision in CollisionSubject, it will call OnNext to all its observers.
        /// </summary>
        /// <param name="value"></param>
        public void OnNext(Collision value)
        {
            ScaleGameObjectByFields();
        }

        /// <summary>
        ///     Transforms - i.e., scales the current gameObject by some amount in fields, currently only multiplication.
        ///     TODO: look into delegates and custom behiavour that can be changed in the editor.
        /// </summary>
        private void ScaleGameObjectByFields()
        {
            var prevX = gameObject.transform.localScale.x;
            var prevY = gameObject.transform.localScale.y;
            var prevZ = gameObject.transform.localScale.z;
            gameObject.transform.localScale =
                new Vector3(prevX * scaleFactorX, prevY * scaleFactorY, prevZ * scaleFactorZ);
        }
    }
}