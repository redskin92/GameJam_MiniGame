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

        //if (levelNames != null && levelNames.Count > 0)
        //{
        //    newSceneToLoad = levelNames[0];
        //}

        levels = new List<int>();

        for(int i = 1; i < levelNames.Count; ++i)
        {
            levels.Add(i);
        }
    }

    public void LoadInUI()
    {
        SceneManager.LoadSceneAsync("GameHUD", LoadSceneMode.Additive);
    }

    public void UnLoadUI()
    {
        SceneManager.UnloadSceneAsync("GameHUD");
    }

    // move on to the next level (one of our levels)
    public bool MoveToLevel(bool fadeOut, int level)
    {
        // safety check
        if (level >= 0)
        {
            LevelIndex = level;

            currentSceneLoaded = newSceneToLoad;
            newSceneToLoad = levelNames[level];

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

            return true;
        }

        // return false if we are at the end of levels
        return false;
    }

    public void MoveToMainMenu()
    {
        MoveToLevel(true, 0);
        UnLoadUI();
    }

    // move to display results after one of our levels
    public void MoveToBeach()
    {
        MoveToLevel(true, 1);
        LoadInUI();
    }

    // move to display results after one of our levels
    public void MoveToForest()
    {
        MoveToLevel(true, 2);
        LoadInUI();
    }

    // load in the game rules
    public void MoveToRockyCoast()
    {
        MoveToLevel(true, 3);
        LoadInUI();
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

        if (currentSceneLoaded != null && currentSceneLoaded.Length > 1)
        {
            SceneManager.sceneUnloaded += LevelUnLoaded;

            SceneManager.UnloadSceneAsync(currentSceneLoaded);
        }
        else
        {
            SceneManager.sceneLoaded += LevelLoaded;

            SceneManager.LoadSceneAsync(newSceneToLoad, LoadSceneMode.Additive);
        }
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
