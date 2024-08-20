using Characters.Player;
using Unity.VisualScripting;
using UnityEngine;

public class GameEndObjective : EdibleScript
{
    private Animator playerAnimator;
    protected override void EatAction()
    {
        Debug.Log(10);
        // end the game
        base.EatAction();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerAnimator.SetBool("isWin", true);
        GameManager.Instance.UpdateGameState(GameState.Win); // win!
    }
}