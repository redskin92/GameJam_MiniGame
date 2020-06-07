// fuck file headers copy my code idc
// this class deals with some button selected functions

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonBehaviors : MonoBehaviour
{
    /// <summary>
    /// Called when the continue button on the game rules is pressed
    /// </summary>
    public void ContinueGameRules()
    {
        GameFlow.Instance.GameRulesComplete();
    }

    /// <summary>
    /// Called when the continue button on the display results is pressed
    /// </summary>
    public void ContinueDisplayResults()
    {
        GameFlow.Instance.DisplayResultsComplete();
    }
}
