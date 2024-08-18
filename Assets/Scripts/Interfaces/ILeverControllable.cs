using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Marks an object is controllable by a lever
/// </summary>
public interface ILeverControllable
{
    /// <summary>
    /// Function will be called when the the lever is triggered
    /// </summary>
    /// <param name="leverState">current new state of the lever</param>
    void OnTriggered(bool leverState);
}
