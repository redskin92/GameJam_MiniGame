using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TylerLevelComplete : MonoBehaviour
{
    public float waitTimeToComplete = 3.0f;
    public int scoreToWin = 3;

    public static int player1Score = 0;
    public static int player2Score = 0;

    bool called = false;

    void Update()
    {
        waitTimeToComplete -= Time.deltaTime;

        // just shows what needs to be called to end a level
        // if we call it multiple times the fade manager will continue to fade
        // so try not to call LevelComplete multiple times plz ;)
        // unless you wanna troll
        if ((player1Score >= scoreToWin || player2Score >= scoreToWin) && !called)
        {
            called = true;

            // all that should happen is that this is called at the end of your game
            GameFlow.Instance.LevelComplete(new List<int>() { player1Score, player2Score });
        }
    }
}
