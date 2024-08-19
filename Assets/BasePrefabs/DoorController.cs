using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Door controller
/// </summary>
public class DoorController : MonoBehaviour, ILeverControllable
{
    private bool _isDoorOpen;
    private SpriteRenderer _renderer;
    private BoxCollider2D _collider2D;

    /// <summary>
    /// Initialize all required components
    /// </summary>
    void Start() {
        _isDoorOpen = false;
        _renderer = this.gameObject.transform.Find("DoorSurface").GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<BoxCollider2D>();
    }

    public void OnLeverStateChanged(bool leverState) {
        if (leverState) {
            _renderer.color = new Color(0, 0, 0);
            _collider2D.enabled = false;
        } else {
            _renderer.color = new Color(255, 255, 255);
            _collider2D.enabled = true;
        }
    }
}
