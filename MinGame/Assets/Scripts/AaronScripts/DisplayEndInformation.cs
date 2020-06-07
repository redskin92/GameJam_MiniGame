using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEndInformation : MonoBehaviour
{
    [SerializeField]
    private ScoreCalculator scoreCalculator;

    [SerializeField]
    private Text player1Score;

    [SerializeField]
    private Text player2Score;

    [SerializeField]
    private DisplayPlayerInfo player1Info;

    [SerializeField]
    private DisplayPlayerInfo player2Info;

    [SerializeField]
    private GameObject background;

    private bool endLevel;

    private void Start()
    {
        endLevel = false;
    }

    private void Update()
    {
        if(endLevel)
        {
            if(Input.GetKey(KeyCode.Return))
            {
                MoveToNextLevel();
                endLevel = false;
            }
        }
    }

    public void DisplayEndResults()
    {
        if (scoreCalculator.PlayerScores.Count >= 2)
        {
            if (player1Score != null)
            {
                player1Score.text = player1Info.name + " Score: " + (scoreCalculator.PlayerScores[0]).ToString();
            }
            if (player2Score != null)
            {
                player2Score.text = player2Info.name + " Score: " + (scoreCalculator.PlayerScores[1]).ToString();
            }

            background.SetActive(true);
            endLevel = true;
        }
    }

    public void MoveToNextLevel()
    {
        if (GameFlow.Instance != null)
        {
            GameFlow.Instance.LevelComplete(scoreCalculator.PlayerScores);
        }
        else
        {
            // end the game when not running from Initial
            UnityEngine.Debug.Log("End of game");
        }
    }
}
