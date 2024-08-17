using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Main Game Manager
/// 
/// NOTE: 
/// To use this, an empty game object must be created in the starting scene with this script loaded on it.
/// This empty game object will be used as a singleton instance and will never be destroyed during the entire game.
/// 
/// A cutscene canvas must also be created, please see <see cref="CutSceneLoader.cs"/>.
/// Please refer to <seealso cref="Scenes/SampleGameScene.unity"/> on how to use this.
/// 
/// TODO: Feel free to add more states to the enum if needed.
/// 
/// </summary>
public class GameManager : MonoBehaviour
{

    // current game state
    public GameState State;

    // actions to trigger when game state changes
    public static event Action<GameState> onGameStateChanged;

    #region Singleton
    // singleton instance
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                Debug.LogError("GameManager is null!");
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

        // do not destroy the game manager.
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.StartingScreen);
    }

    /// <summary>
    ///     Update game states and triggers the game state changed action.
    /// </summary>
    public void UpdateGameState(GameState newState) {
        State = newState;
        switch (newState) {
            // all kinds of logic
            case GameState.OnGame:
                HandleGameStart();
                break;
            case GameState.StartingScreen:
                // do nothing for now
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.Lose:
                HandleLose();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        // trigger the action, ? prevents null pointer exceptions if no one subscribed.
        onGameStateChanged?.Invoke(newState);
    }

    /// <summary>
    /// Switch levels by index
    /// </summary>
    /// <param name="index">scene index from the building view.</param>
    public void SwitchLevel(int index) {
        // create the cutscene object
        StartCoroutine(LoadScene(index));
    }

    /// <summary>
    /// Load a new scene based on the index number of the scene asyncly with animations
    /// TODO: actually implement cutscene when the artist finished.
    /// </summary>
    /// <param name="index">scene index from the scene builder</param>
    /// <returns>async task</returns>
    private IEnumerator LoadScene(int index) {
        CutSceneLoader.Instance.FadeIn();
        // if you want the animation to be longer, change 1s to > 1s.
        yield return new WaitForSeconds(1);
        AsyncOperation aysncOp = SceneManager.LoadSceneAsync(index);
        // when the async task is completed
        aysncOp.completed += delegate {CutSceneLoader.Instance.FadeOut();};
    }

    /// <summary>
    ///     Handles lose event
    ///     Unimplemented - we don't know the specific logic rn.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void HandleLose() {
        // TODO: impelement this
        Debug.Log("Game Over");
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Handles win event
    ///     Unimplemented - we don't know the specific logic rn.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void HandleWin() {
        // TODO: implement this
        Debug.Log("Game Won");
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Handles game start event
    ///     Unimplemented - we don't know the specific logic rn.
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void HandleGameStart() {
        // TODO: implement this
        Debug.Log("Game Started");
        throw new NotImplementedException();
    }
}


/// <summary>
///     Defines all kinds of game states.
/// </summary>
public enum GameState {
    StartingScreen,
    OnGame,
    Win,
    Lose
}
