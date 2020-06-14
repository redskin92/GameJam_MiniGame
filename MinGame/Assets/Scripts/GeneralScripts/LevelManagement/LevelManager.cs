// fuck file headers copy my code idc
// this class deals with Managing each level when it should be loaded/ unloaded
// controlled by GameFlow

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int numRounds = 5;

    public List<string> levelNames;

    public BackgroundFadeManager backgroundFadeManager;

    public event Action<LevelManager> TransitionComplete;

    string currentSceneLoaded;
    string newSceneToLoad;

    private List<int> levels;

    public int LevelIndex { get; set; }

    public int LevelSelected { get; set; }

    // set some variables just in case
    void Awake()
    {
        LevelIndex = 0;

        LevelSelected = 0;

        if (levelNames != null && levelNames.Count > 0)
        {
            newSceneToLoad = levelNames[0];
        }

        levels = new List<int>();

        for(int i = 1; i < levelNames.Count; ++i)
        {
            levels.Add(i);
        }
    }

    // move on to the next level (one of our levels)
    public bool MoveToNextLevel(bool fadeOut)
    {
        // safety check
        if (LevelIndex >= 0)
        {
            currentSceneLoaded = newSceneToLoad;
            newSceneToLoad = levelNames[LevelIndex];

            // if we want to fade out the screen
            if (fadeOut)
            {
                backgroundFadeManager.FadeInComplete += FadeInCompleted;
                backgroundFadeManager.SetTimer(1.0f, true);
            }
            // if we want to just load the scene
            else
            {
                SceneManager.sceneLoaded += LevelLoaded;
                SceneManager.LoadSceneAsync(newSceneToLoad, LoadSceneMode.Additive);
            }

            // inscrese to the next level
            //++LevelIndex;

            if (numRounds > 0)
            {
                LevelIndex = levels[LevelSelected];

                //levels.Remove(LevelIndex);

                --numRounds;
            }
            else
            {
                LevelIndex = -1;
            }

            return true;
        }

        // return false if we are at the end of levels
        return false;
    }

    // load in the game rules
    public void MoveToGameRules()
    {
        currentSceneLoaded = newSceneToLoad;
        newSceneToLoad = "GameRules";

        backgroundFadeManager.FadeInComplete += FadeInCompleted;
        backgroundFadeManager.SetTimer(1.0f, true);
    }

    // move to the next of our levels after the game rules are shown
    public void MoveFromGameRules()
    {
        MoveToNextLevel(true);
    }

    // move to display results after one of our levels
    public void MoveToDisplayResults()
    {
        currentSceneLoaded = newSceneToLoad;
        newSceneToLoad = "ShowResults";

        backgroundFadeManager.FadeInComplete += FadeInCompleted;
        backgroundFadeManager.SetTimer(1.0f, true);
    }

    // move to display results after one of our levels
    public void MoveToBoard()
    {
        currentSceneLoaded = newSceneToLoad;
        newSceneToLoad = "BoardScene";

        backgroundFadeManager.FadeInComplete += FadeInCompleted;
        backgroundFadeManager.SetTimer(1.0f, true);
    }

    public int GetLevelCount()
    {
        return levels.Count;
    }

    /// <summary>
    /// When the level is loaded in, fade out the blanker
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void LevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LevelLoaded;

        backgroundFadeManager.FadeOutComplete += FadeOutCompleted;
        backgroundFadeManager.SetTimer(0.5f, false);
    }

    /// <summary>
    /// When the level is fully unloaded, load in the next one
    /// </summary>
    /// <param name="scene"></param>
    private void LevelUnLoaded(Scene scene)
    {
        SceneManager.sceneUnloaded -= LevelUnLoaded;

        SceneManager.sceneLoaded += LevelLoaded;

        SceneManager.LoadSceneAsync(newSceneToLoad, LoadSceneMode.Additive);
    }

    /// <summary>
    /// When the fade in has completed, unload the selected level
    /// </summary>
    /// <param name="manager"></param>
    private void FadeInCompleted(BackgroundFadeManager manager)
    {
        backgroundFadeManager.FadeInComplete -= FadeInCompleted;

        SceneManager.sceneUnloaded += LevelUnLoaded;

        SceneManager.UnloadSceneAsync(currentSceneLoaded);
    }

    /// <summary>
    /// When the fader out has completed, let one of our levels know it can start
    /// </summary>
    /// <param name="manager"></param>
    private void FadeOutCompleted(BackgroundFadeManager manager)
    {
        backgroundFadeManager.FadeOutComplete -= FadeOutCompleted;

        TransitionComplete(this);
    }
}
