using System;
using UnityEngine;

namespace pattern
{
    /// <summary>
    ///     A class that subscribes to a certain CollisionSubject, and gets scaled by scaling factors
    ///     when the CollisionSubjects collides with something.
    /// </summary>
    public abstract class CollisionObserver : MonoBehaviour, IObserver<Collision>
    {
        [SerializeField] private CollisionSubject collisionSubscription;

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
            Scale();
        }

        /// <summary>
        ///     Custom, must be overriden, abstract scaling behaviour on the associated gameobject.
        /// </summary>
        protected abstract void Scale();
    }
}