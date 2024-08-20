using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class IntroCutSceneHandler : MonoBehaviour
{

    private PlayableDirector _playableDirector;
    private bool _isAnimationFinished = false;

    void Start() {
        _playableDirector = GetComponent<PlayableDirector>();
        _playableDirector.Play();
    }

    void Update() {
        if (!_isAnimationFinished && _playableDirector.time == _playableDirector.duration) {
            GameManager.Instance.SwitchLevel(CurrentLevel.Tutorial.ToIndex());
            _isAnimationFinished = true;
        }
    }
}
