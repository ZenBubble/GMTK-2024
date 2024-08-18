using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour, IInteractable
{
    private bool _isLeverOn;
    private Transform _leverTransform;

    public GameObject controllableGameObj;

    private ILeverControllable _controllable;

    void Start() {
        _isLeverOn = false;
        // get the transform component of the lever pivot
        _leverTransform = this.gameObject.transform.parent.GetComponent<Transform>();
        _controllable = controllableGameObj?.GetComponent<ILeverControllable>();
    }

    public void SwitchLeverState() {
        _isLeverOn = !_isLeverOn;
        _leverTransform.rotation = _isLeverOn ? Quaternion.Euler(0, 0, 30) : Quaternion.Euler(0, 0, -30);
    }

    public void OnInteraction() {
        SwitchLeverState();
        _controllable.OnTriggered(_isLeverOn);
    }
}
