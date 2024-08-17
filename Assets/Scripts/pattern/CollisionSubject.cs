using System;
using System.Collections.Generic;
using UnityEngine;

namespace pattern
{
    /// <summary>
    ///     Abstract CollisionSubject -- behaves like IObservable. Add this to your player or your powerups, probably?
    ///     TODO: clean up behaviour with collision matrices instead of specifying floor.
    /// </summary>
    public abstract class CollisionSubject : MonoBehaviour, IObservable<Collision>
    {
        [SerializeField] private GameObject floor;
        private readonly HashSet<IObserver<Collision>> _observers = new();


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject == floor) return;

            foreach (var observer in _observers) observer.OnNext(other);
        }

        public IDisposable Subscribe(IObserver<Collision> observer)
        {
            _observers.Add(observer);
            return null!; // what? 
        }
    }
}