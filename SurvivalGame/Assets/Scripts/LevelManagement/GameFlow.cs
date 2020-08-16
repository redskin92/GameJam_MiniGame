// fuck file headers copy my code idc
// this class deals with the game flow (loading levels, geting things flowing, you know)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    public LevelManager levelManager;

    // the static class for GameFlow
    public static GameFlow Instance
    {
        get; set;
    }

    // the current level we are on (CharacterSelect is first level)
    public int CurrentLevel
    {
        get { return levelManager.LevelIndex; }
    }

    // Used to initalize level manager and move to the character select scene
    void Awake()
    {
        levelManager.TransitionComplete += TransitionCompleted;

        //initially move to MainMenu
        levelManager.MoveToLevel(false, 0);

        Instance = this;
    }

    public void MoveToMainMenu()
    {
        levelManager.TransitionComplete += TransitionCompleted;
        
        levelManager.MoveToMainMenu();
    }

    public void MoveToForest()
    {
        levelManager.TransitionComplete += TransitionCompleted;

        levelManager.MoveToForest();
    }

    public void MoveToBeach()
    {
        levelManager.TransitionComplete += TransitionCompleted;

        levelManager.MoveToBeach();
    }

    public void MoveToRockyCoast()
    {
        levelManager.TransitionComplete += TransitionCompleted;

        levelManager.MoveToRockyCoast();
    }

    /// <summary>
    /// This function is not used for now but could be to resport when a level has been loaded in fully
    /// maybe to delay a game from starting till the transition is complete
    /// </summary>
    /// <param name="manager"></param>
    private void TransitionCompleted(LevelManager manager)
    {
        levelManager.TransitionComplete -= TransitionCompleted;
    }
}
