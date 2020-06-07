using System;
using UnityEngine;

namespace ChristianScripts
{
    /// <summary>
    /// Class for the sheep doing the racing.
    /// </summary>
    public class SheepRacer : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Is this racer AI controlled?
        /// </summary>
        [SerializeField]
        private bool aiControlled;

        /// <summary>
        /// Animator reference.
        /// </summary>
        [SerializeField]
        private Animator animator;

        /// <summary>
        /// Sprite renderer reference.
        /// </summary>
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Player number.
        /// </summary>
        [SerializeField]
        private int playerNumber;

        /// <summary>
        /// Ref to audio source.
        /// </summary>
        private AudioSource audio;

        /// <summary>
        /// Is the key prompt enabled?
        /// </summary>
        private bool keyPromptEnabled;

        #endregion

        #region Events

        /// <summary>
        /// FIred when a jump is completed successfully.
        /// </summary>
        public event EventHandler OnJumpSuccess;

        #endregion

        #region Properties
        
        /// <summary>
        /// The key that was pressed.
        /// </summary>
        public KeyCode KeyUsed { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            audio = GetComponent<AudioSource>();

            ResetKeyUsed();

            AssignCharacterSprite();
        }

        private void Update()
        {
            if (keyPromptEnabled)
            {
                GetKeyPress();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Enable key prmopts.
        /// </summary>
        public void EnableKeyPrompt()
        {
            if (aiControlled)
                return;

            keyPromptEnabled = true;
        }

        /// <summary>
        /// Disable key prompts.
        /// </summary>
        public void DisableKeyPrompt()
        {
            if (aiControlled)
                return;

            keyPromptEnabled = false;
        }

        /// <summary>
        /// Jump was successful.
        /// </summary>
        public void JumpSuccess()
        {
            //Debug.Log("Jump Success!!!");

            animator.Play("JumpSuccess");

            FireOnJumpSuccess();

            DisableKeyPrompt();
            ResetKeyUsed();
        }

        /// <summary>
        /// Jump failed.
        /// </summary>
        public void JumpFail()
        {
            //Debug.Log("Jump Failed!");

            animator.Play("JumpFail");

            audio.Play();

            DisableKeyPrompt();
            ResetKeyUsed();
        }

        /// <summary>
        /// Reset the key used property.
        /// </summary>
        private void ResetKeyUsed()
        {
            KeyUsed = KeyCode.None;
        }

        /// <summary>
        /// Get the key that was pressed.
        /// </summary>
        /// <returns>True if a key was pressed.</returns>
        private bool GetKeyPress()
        {
            for (int i = 97; i <= 122; i++)
            {
                KeyCode key = (KeyCode)i;
                if (Input.GetKeyDown(key))
                {
                    KeyUsed = key;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Assign the character sprite.
        /// </summary>
        private void AssignCharacterSprite()
        {
            if (GameFlow.Instance == null)
                return;

            spriteRenderer.sprite = GameFlow.Instance.CharacterSprites[GameFlow.Instance.PlayerInfo[playerNumber - 1].spriteIndex];
        }

        #endregion

        #region Event Handling

        private void FireOnJumpSuccess()
        {
            var handler = OnJumpSuccess;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion
    }
}