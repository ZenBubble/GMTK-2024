using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All entities that can be interacted with a tongue touch must implement this interface.
/// </summary>
public interface IInteractable
{
    void OnInteraction();
}
