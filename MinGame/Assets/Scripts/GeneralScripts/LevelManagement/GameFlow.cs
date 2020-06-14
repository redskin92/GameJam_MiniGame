// fuck file headers copy my code idc
// this class deals with the game flow (loading levels, geting things flowing, you know)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    public LevelManager levelManager;

    public List<Sprite> characterSprites;

    // the static class for GameFlow
    public static GameFlow Instance
    {
        get; set;
    }

    // the list of sprite characters available
    public List<Sprite> CharacterSprites
    {
        get { return characterSprites; }
    }

    // the list of player infos (2 players)
    public List<PlayerInfo> PlayerInfo
    {
        get; set;
    }

    // the current level we are on (CharacterSelect is first level)
    public int CurrentLevel
    {
        get { return levelManager.LevelIndex; }
    }

    // Used to intialize level manager and move to the character select scene
    void Awake()
    {
        levelManager.TransitionComplete += TransitionCompleted;
        levelManager.MoveToNextLevel(false);

        PlayerInfo = new List<PlayerInfo>();
        Instance = this;
    }

    /// <summary>
    /// Get the info of a given player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public PlayerInfo GetPlayerInfo(int player)
    {
        if (player < PlayerInfo.Count)
        {
            return PlayerInfo[player];
        }

        return null;
    }

    /// <summary>
    /// Get the Sprite of a given player (either 1 or 2)
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Sprite GetPlayerSprite(int player)
    {
        if (player < PlayerInfo.Count && PlayerInfo[player].spriteIndex < characterSprites.Count)
        {
            return characterSprites[PlayerInfo[player].spriteIndex];
        }

        return null;
    }

    // Called when the character select screen has selected characters
    public void CharacterSelectionComplete(List<string> names, List<int> spriteIndexes)
    {
        for (int i = 0; i < names.Count; ++i)
        {
            PlayerInfo.Add(new PlayerInfo(names[i], spriteIndexes[i]));
        }

        levelManager.TransitionComplete += TransitionCompleted;

        levelManager.MoveToDisplayResults();
    }

    /// <summary>
    /// Called when one of our levels are complete
    // records the scores and moves on to displaying the results
    // just saw summary /// just now, sorry im drunk
    // moved up some functions but too lazy to fix
    /// </summary>
    /// <param name="scores"></param>
    public void LevelComplete(List<int> scores)
    {
        for (int i = 0; i < scores.Count; ++i)
        {
            PlayerInfo[i].score += scores[i];
        }

        levelManager.TransitionComplete += TransitionCompleted;

        levelManager.MoveToDisplayResults();
    }

    public void MoveToBoard()
    {
        levelManager.TransitionComplete += TransitionCompleted;

        levelManager.MoveToBoard();
    }

    public void MoveFromBoardRandom()
    {
        levelManager.LevelSelected = UnityEngine.Random.Range(0, levelManager.GetLevelCount() - 1);

        levelManager.TransitionComplete += TransitionCompleted;

        levelManager.MoveToGameRules();
    }

    public void MoveFromBoard(int nextLevel)
    {
        if (nextLevel >= 0 && nextLevel < levelManager.GetLevelCount())
        {
            levelManager.LevelSelected = nextLevel;

            levelManager.TransitionComplete += TransitionCompleted;

            levelManager.MoveToGameRules();
        }
        else
        {
            UnityEngine.Debug.LogError("NextLevel is not within the bounds of possibel levels: " + nextLevel);
        }
    }

    /// <summary>
    ///  Called when the game rules are completed
    ///  move on to to the game
    /// </summary>
    public void GameRulesComplete()
    {
        levelManager.TransitionComplete += TransitionCompleted;

        levelManager.MoveFromGameRules();
    }

    /// <summary>
    /// Move on to game rules of the next game after displaying the results
    /// </summary>
    public void DisplayResultsComplete()
    {
        if (levelManager.LevelIndex >= 0)
        {
            levelManager.TransitionComplete += TransitionCompleted;

            levelManager.MoveToBoard();
        }
        else
        {
            // end of game here
            UnityEngine.Debug.Log("EndOfGameHere");

            // maybe quit the program, maybe replay, idk
            // Application.Quit();
        }
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
