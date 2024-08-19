using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Marks an object is controllable by a lever
/// </summary>
public interface ILeverControllable
{
    /// <summary>
    /// This will be called everytime when the lever state is changed 
    /// (from off to on or from on to off)
    /// </summary>
    /// <param name="leverState">current new state of the lever</param>
    void OnLeverStateChanged(bool leverState);
}
