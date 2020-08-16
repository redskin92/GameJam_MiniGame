using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class StartGameWindow : MenuWindow
    {
        [SerializeField]
        private Button beachButton;

        [SerializeField]
        private Button forestButton;

        [SerializeField]
        private Button coastButton;

        public override void InitializeWindow()
        {
            base.InitializeWindow();
            
        }

        public override void Open()
        {
            base.Open();
            RegisterButtonEvents();
        }

        public override void Close()
        {
            base.Close();
            UnregisterButtonEvents();
        }

        private void RegisterButtonEvents()
        {
            beachButton.onClick.AddListener(BeachButton_Pressed);
            forestButton.onClick.AddListener(ForestButton_Pressed);
            coastButton.onClick.AddListener(CoastButton_Pressed);

            beachButton.interactable = true;
            forestButton.interactable = true;
            coastButton.interactable = true;
        }

        private void UnregisterButtonEvents()
        {
            beachButton.onClick.RemoveListener(BeachButton_Pressed);
            forestButton.onClick.RemoveListener(ForestButton_Pressed);
            coastButton.onClick.RemoveListener(CoastButton_Pressed);
            
            beachButton.interactable = false;
            forestButton.interactable = false;
            coastButton.interactable = false;
        }

        private void BeachButton_Pressed()
        {
            UnregisterButtonEvents();
            GameFlow.Instance.MoveToBeach();
        }
        
        private void ForestButton_Pressed()
        {
            UnregisterButtonEvents();
            GameFlow.Instance.MoveToForest();
        }
        
        private void CoastButton_Pressed()
        {
            UnregisterButtonEvents();
            GameFlow.Instance.MoveToRockyCoast();
        }
    }
}