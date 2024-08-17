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
    public int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(onButtonClick);
    }

    void onButtonClick() {
        Debug.Log("Button is Clicked");
        // This is an example of switch level to levelIndex.
        GameManager.Instance.SwitchLevel(levelIndex);
        // This is an example of update game state to "onGame".
        GameManager.Instance.UpdateGameState(GameState.OnGame);
    }
}
