using System;
using Settings;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        #region Enums

        public enum MenuState
        {
            Root,
            StartGame,
            Options
        };

        #endregion

        #region Fields

        [SerializeField]
        private Button startButton;
        
        [SerializeField]
        private Button optionButton;
        
        [SerializeField]
        private Button exitButton;

        [SerializeField]
        private Canvas menuButtonCanvas;

        [SerializeField]
        private StartGameWindow startGameWindow;

        [SerializeField]
        private OptionsWindow optionsWindow;

        private MenuState _currentState = MenuState.Root;

        #endregion

        #region Properties

        public MenuState CurrentState
        {
            private get => _currentState;
            set
            {
                _currentState = value;
            }
        }

        #endregion

        #region Methods

        private void Start()
        {
            // Register menu buttons
            startButton.onClick.AddListener(StartButton_Pressed);
            optionButton.onClick.AddListener(OptionButton_Pressed);
            exitButton.onClick.AddListener(ExitButton_Pressed);
            
            // Initialize windows
            startGameWindow.InitializeWindow();
            optionsWindow.InitializeWindow();
            
            // Register menu visiblity events
            startGameWindow.MenuVisibilityChanged.AddListener(StartGameWindow_MenuVisibilityChanged);
            optionsWindow.MenuVisibilityChanged.AddListener(OptionsWindow_MenuVisibilityChanged);

            // Set to default state
            CurrentState = MenuState.Root;
        }

        private void StartButton_Pressed()
        {
            // If allow level select, open window to select level.
            if(SettingsManager.Instance.AllowLevelSelect)
                startGameWindow.Open();
            else
                GameFlow.Instance.MoveToBeach();
        }

        private void OptionButton_Pressed()
        {
            optionsWindow.Open();
        }

        private void ExitButton_Pressed()
        {
            #if UNITY_EDITOR
                // Handle exiting from UI in Editor.
                EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        private void StartGameWindow_MenuVisibilityChanged(bool visible)
        {
            CurrentState = visible ? MenuState.StartGame : MenuState.Root;

            menuButtonCanvas.enabled = !visible;
        }
        
        private void OptionsWindow_MenuVisibilityChanged(bool visible)
        {
            CurrentState = visible ? MenuState.Options : MenuState.Root;
            
            menuButtonCanvas.enabled = !visible;
        }

        #endregion
    }
}
