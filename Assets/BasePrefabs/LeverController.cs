using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    private bool _isLeverOn;
    private Transform _leverTransform;

    public GameObject controllableGameObj;

    private ILeverControllable _controllable;
    [SerializeField] private AudioManager audioManager;

    void Start() {
        _isLeverOn = false;
        // get the transform component of the lever pivot
        _controllable = controllableGameObj?.GetComponent<ILeverControllable>();
    }

    void Update() {
    }

    public void Toggle() {
        _isLeverOn = !_isLeverOn;
        _controllable.OnLeverStateChanged(_isLeverOn);
    }
}
