using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ChristianScripts
{
    /// <summary>
    /// Manager for the Sheep Racer game.
    /// </summary>
    public class SheepRacerGameManager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Base string for the Ready text.
        /// </summary>
        private const string READY_TEXT_BASE = "Player {0}, Ready!? GO!";

        /// <summary>
        /// Time to wait before spawning the first obstacle.
        /// </summary>
        [SerializeField]
        private float timeBeforeFirstObstacle = 3f;

        /// <summary>
        /// Time to spawn obstacles.
        /// </summary>
        [SerializeField]
        private float timeBetweenObstacles = 3f;

        /// <summary>
        /// The time after spawning the final object to end the game.
        /// </summary>
        [SerializeField]
        private float timeBeforeGameEnd = 1f;

        /// <summary>
        /// The time after spawning the final object to end the game.
        /// </summary>
        [SerializeField]
        private float textDisplayTime = 1f;

        /// <summary>
        /// The total number of obstacles to spawn.
        /// </summary>
        [SerializeField]
        private int obstaclesToSpawn;

        /// <summary>
        /// Obstacle prefab.
        /// </summary>
        [SerializeField]
        private GameObject obstaclePrefab;

        /// <summary>
        /// Spawn point for the obstacles.
        /// </summary>
        [SerializeField]
        private Transform obstacleSpawnPoint;

        /// <summary>
        /// Parent object for the obstacles.
        /// </summary>
        [SerializeField]
        private Transform obstacleParent;

        /// <summary>
        /// Array of racers.
        /// </summary>
        [SerializeField]
        private SheepRacer[] sheepRacers;

        /// <summary>
        /// Racer scores.
        /// </summary>
        [SerializeField]
        private Text racer1ScoreText, racer2ScoreText;

        /// <summary>
        /// The scores for each racer.
        /// </summary>
        private int racer1Score = 0, racer2Score = 0;

        [SerializeField]
        private Text readyText;

        /// <summary>
        /// The number of obstacles that have been spawned.
        /// </summary>
        private int obstaclesSpawned;

        #endregion

        #region Unity Methods

        private void Start()
        {
            sheepRacers[0].OnJumpSuccess += Racer1_OnJumpSuccess;
            sheepRacers[1].OnJumpSuccess += Racer2_OnJumpSuccess;

            StartGame();
        }

        private void OnDestroy()
        {
            if (sheepRacers != null)
            {
                if (sheepRacers[0] != null)
                    sheepRacers[0].OnJumpSuccess -= Racer1_OnJumpSuccess;

                if (sheepRacers[1] != null)
                    sheepRacers[1].OnJumpSuccess -= Racer2_OnJumpSuccess;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Start the game.
        /// </summary>
        public void StartGame()
        {
            StartCoroutine(RaceSequence());
        }

        /// <summary>
        /// The sequence for the race.
        /// </summary>
        private IEnumerator RaceSequence()
        {
            for (int i = 0; i < sheepRacers.Length; i++)
            {
                EnableSheepRacer(i);

                ClearObstacles();

                DisplayReadyText(i + 1);

                yield return new WaitForSeconds(textDisplayTime);

                HideReadyText();

                yield return new WaitForSeconds(timeBeforeFirstObstacle);

                do
                {
                    SpawnObstacle();

                    sheepRacers[i].EnableKeyPrompt();

                    yield return new WaitForSeconds(timeBetweenObstacles);
                } while (obstaclesSpawned < obstaclesToSpawn);

                yield return new WaitForSeconds(timeBeforeGameEnd);
            }

            GameFlow.Instance.LevelComplete(new List<int> { racer1Score, racer2Score });
        }

        /// <summary>
        /// Spawn an obstacle.
        /// </summary>
        private void SpawnObstacle()
        {
            GameObject obstacle = Instantiate(obstaclePrefab, obstacleSpawnPoint.position, obstaclePrefab.transform.rotation);
            obstacle.name = "Obstacle " + ++obstaclesSpawned;
            obstacle.transform.parent = obstacleParent;
        }

        /// <summary>
        /// Display the Ready text at the start of a turn.
        /// </summary>
        /// <param name="playerNum">The player we're displaying this for.</param>
        private void DisplayReadyText(int playerNum)
        {
            readyText.text = string.Format(READY_TEXT_BASE, playerNum);
            readyText.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide the ready text.
        /// </summary>
        private void HideReadyText()
        {
            readyText.gameObject.SetActive(false);
        }

        /// <summary>
        /// Enable the correct sheep.
        /// </summary>
        /// <param name="index"></param>
        private void EnableSheepRacer(int index)
        {
            for (int i = 0; i < sheepRacers.Length; i++)
            {
                sheepRacers[i].gameObject.SetActive(i == index);
            }
        }

        /// <summary>
        /// Clear the old obstacles.
        /// </summary>
        private void ClearObstacles()
        {
            foreach (Transform t in obstacleParent)
                Destroy(t.gameObject);

            obstaclesSpawned = 0;
        }

        #endregion

        #region Event Handling

        private void Racer1_OnJumpSuccess(object sender, EventArgs args)
        {
            racer1Score++;
            racer1ScoreText.text = racer1Score.ToString();
        }

        private void Racer2_OnJumpSuccess(object sender, EventArgs args)
        {
            racer2Score++;
            racer2ScoreText.text = racer2Score.ToString();
        }

        #endregion
    }
}