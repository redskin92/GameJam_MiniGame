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
        /// Spawn points for the obstacles.
        /// </summary>
        [SerializeField]
        private Transform[] obstacleSpawnPoints;

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

        private int racer1Score = 0, racer2Score = 0;

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

            // TEMP! Start Game should occur in a different way.
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

        public void StartGame()
        {
            StartCoroutine(RaceSequence());
        }

        private IEnumerator RaceSequence()
        {
            yield return new WaitForSeconds(timeBeforeFirstObstacle);

            do
            {
                SpawnObstacle();

                for (int i = 0; i < sheepRacers.Length; i++)
                    sheepRacers[i].EnableKeyPrompt();

                yield return new WaitForSeconds(timeBetweenObstacles);
            } while (obstaclesSpawned < obstaclesToSpawn);

            yield return new WaitForSeconds(timeBeforeGameEnd);

            GameFlow.Instance.LevelComplete(new List<int> { racer1Score, racer2Score });
        }

        private void SpawnObstacle()
        {
            for (int i = 0; i < obstacleSpawnPoints.Length; i++)
            {
                GameObject obstacle = Instantiate(obstaclePrefab, obstacleSpawnPoints[i].position, obstaclePrefab.transform.rotation);
                obstacle.name = "Obstacle " + (i + 1);
                obstacle.transform.parent = obstacleParent;
            }

            obstaclesSpawned++;
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