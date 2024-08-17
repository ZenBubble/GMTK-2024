using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Test switching scenes and update state of the game using GameManager.
/// </summary>
public class SceneSwitchButton : MonoBehaviour
{

    public Button thisButton;

    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(onButtonClick);
    }

    void onButtonClick() {
        // This is an example of switch level to level 1.
        GameManager.Instance.SwitchLevel(1);
        // This is an example of update game state to "onGame".
        GameManager.Instance.UpdateGameState(GameState.OnGame);
    }
}
