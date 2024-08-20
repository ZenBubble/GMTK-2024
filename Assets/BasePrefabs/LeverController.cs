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
        _leverTransform = this.gameObject.transform.parent.GetComponent<Transform>();
        _controllable = controllableGameObj?.GetComponent<ILeverControllable>();
        _leverTransform.rotation = Quaternion.Euler(0, 0, -50);
    }

    void Update() {
    }

    public void Toggle() {
        _isLeverOn = !_isLeverOn;
        _controllable.OnLeverStateChanged(_isLeverOn);
        if (_isLeverOn) {
            _leverTransform.rotation = Quaternion.Euler(0, 0, 50);
        } else {
            _leverTransform.rotation = Quaternion.Euler(0, 0, -50);
        }
    }
}
