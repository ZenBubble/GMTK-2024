using Unity.VisualScripting;
using UnityEngine;

public class GameEndObjective : EdibleScript
{
    protected override void EatAction()
    {
        Debug.Log(10);
        // end the game
        base.EatAction();
        GameManager.Instance.UpdateGameState(GameState.Win); // win!
    }
}