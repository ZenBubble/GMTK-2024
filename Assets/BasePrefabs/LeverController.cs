using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    private bool _isLeverOn;
    private Transform _leverTransform;

    public GameObject controllableGameObj;

    private ILeverControllable _controllable;

    void Start() {
        _isLeverOn = false;
        // get the transform component of the lever pivot
        _leverTransform = this.gameObject.GetComponent<Transform>();
        _controllable = controllableGameObj?.GetComponent<ILeverControllable>();
        // set the lever to off state
        GetComponent<Rigidbody2D>().AddTorque(-100);
    }

    void Update() {
        float rotation = _leverTransform.localEulerAngles.z;
        if (rotation <= 120) {
            // off
            if (_isLeverOn) {
                _controllable.OnLeverStateChanged(false);
                _isLeverOn = false;
            }
        } else if (rotation > 120) {
            // on
            if (!_isLeverOn) {
                _controllable.OnLeverStateChanged(true);
                _isLeverOn = true;
            }
        }
    }

    public void Toggle() {
        float rotation = _leverTransform.localEulerAngles.z;
        if (_isLeverOn) {
            GetComponent<Rigidbody2D>().AddTorque(-1000);
        } else {
            GetComponent<Rigidbody2D>().AddTorque(1000);
        }
    }
}
