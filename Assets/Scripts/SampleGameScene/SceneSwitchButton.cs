using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Test switching scenes and update state of the game using GameManager.
/// </summary>
public class SceneSwitchButton : MonoBehaviour
{

    [SerializeField] private int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(onButtonClick);
    }

    LevelLoseSound levelLoseSound;

    private void Awake()
    {
        levelLoseSound = GameObject.FindGameObjectWithTag("Sound").GetComponent<LevelLoseSound>();
    }

    void onButtonClick() {
        levelLoseSound.PlaySFX(levelLoseSound.button);
        Debug.Log(levelIndex);
        // This is an example of switch level to levelIndex.
        GameManager.Instance.SwitchLevel(levelIndex);
        // This is an example of update game state to "onGame".
        GameManager.Instance.UpdateGameState(GameState.OnGame);
    }
}
