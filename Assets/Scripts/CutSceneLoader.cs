using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cut Scene Loader
/// 
/// NOTE:
/// To use this, a canvas object must be created in the starting scene with this singleton script loaded on it.
/// 
/// TODO:
/// Feel free to adjust the Cutscene animation in Unity. <see cref="Animations/CutsceneCoverCanvas.controller"/> 
/// </summary>
public class CutSceneLoader : MonoBehaviour
{

    private Animator cutsceneAnimator;

    #region Singleton
    // singleton instance
    private static CutSceneLoader _instance;
    public static CutSceneLoader Instance {
        get {
            if (_instance == null) {
                Debug.LogError("CutSceneLoader is null!");
            }
            return _instance;
        }
    }

    void Awake() {
        if (_instance) {
            Destroy(gameObject);
        } else {
            _instance = this;
        }

        // do not destroy the cutscene canvas
        DontDestroyOnLoad(this);
    }
    #endregion

    void Start() {
        cutsceneAnimator = this.gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// Fade out the cutscene.
    /// </summary>
    public void FadeOut() {
        cutsceneAnimator.SetBool("FadeIn", false);
        cutsceneAnimator.SetBool("FadeOut", true);
    }

    /// <summary>
    /// Fade in the cutscene.
    /// </summary>
    public void FadeIn() {
        cutsceneAnimator.SetBool("FadeIn", true);
        cutsceneAnimator.SetBool("FadeOut", false);
    }
}
