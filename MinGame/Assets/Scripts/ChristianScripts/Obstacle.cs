using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChristianScripts
{
    public class Obstacle : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Reference to the Text showing the prompt.
        /// </summary>
        [SerializeField]
        private TextMesh text;

        /// <summary>
        /// The obstacle move speed.
        /// </summary>
        [SerializeField]
        private float obstacleMoveSpeed;

        private KeyCode chosenKeyCode;

        #endregion

        #region Unity Methods

        private void Start()
        {
            GenerateText();
        }

        private void Update()
        {
            transform.Translate(-Vector3.right * obstacleMoveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var sheep = other.GetComponent<SheepRacer>();
            if (sheep != null)
            {
                if (sheep.KeyUsed == chosenKeyCode)
                    sheep.JumpSuccess();
                else
                    sheep.JumpFail();
            }
        }

        #endregion

        #region Methods

        private void GenerateText()
        {
            int randomNum = Random.Range(97, 122);

            chosenKeyCode = (KeyCode)randomNum;
            text.text = chosenKeyCode.ToString();
        }

        #endregion
    }
}