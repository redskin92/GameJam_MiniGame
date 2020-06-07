using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField]
    DisplayEndInformation displayEndInformation;

    public List<int> PlayerScores { get; set; }

    public float CurrentScore { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentScore = 0;

        PlayerScores = new List<int> { -1, -1 };
    }

    // Update is called once per frame
    void Update()
    {
        CurrentScore += Time.deltaTime;
    }

    public void PlayerDie(int player)
    {
        if (PlayerScores.Count > player)
        {
            PlayerScores[player] = (int)(CurrentScore * 100);

            bool reportScore = true;

            for (int i = 0; i < PlayerScores.Count; ++i)
            {
                if (PlayerScores[i] < 0)
                {
                    reportScore = false;
                }
            }

            if(reportScore)
            {
                displayEndInformation.DisplayEndResults();
            }
        }
    }
}
