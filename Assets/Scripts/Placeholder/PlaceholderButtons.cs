using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceholderButtons : MonoBehaviour
{

    public Button loseButton;
    public Button winButton; 

    public int nextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        loseButton.onClick.AddListener(onLoseButtonClick);
        winButton.onClick.AddListener(onWinButtonClick);
    }

    void onLoseButtonClick() {
        GameManager.Instance.SwitchLevel(nextSceneIndex);
        // This is an example of update game state to "onGame".
        GameManager.Instance.UpdateGameState(GameState.OnGame);
    }

    void onWinButtonClick() {
        GameManager.Instance.SwitchLevel(4);
        // This is an example of update game state to "onGame".
        GameManager.Instance.UpdateGameState(GameState.Lose);
    }
}
