using Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class OptionsWindow : MenuWindow
    {
        [SerializeField]
        private Button saveButton;
        
        [SerializeField]
        private Toggle allowLevelSelectToggle;
        
        public override void InitializeWindow()
        {
            base.InitializeWindow();
        }
        
        public override void Open()
        {
            base.Open();
            UpdateOptions();
            RegisterOptionEvents();
        }

        public override void Close()
        {
            base.Close();
            UnregisterOptionEvents();
        }

        public void SaveSettings()
        {
            SettingsManager.Instance.AllowLevelSelect = allowLevelSelectToggle.isOn;
            SettingsManager.Instance.SaveSettings();
        }

        private void UpdateOptions()
        {
            allowLevelSelectToggle.isOn = SettingsManager.Instance.AllowLevelSelect;
        }

        private void RegisterOptionEvents()
        {
            saveButton.onClick.AddListener(SaveButton_Pressed);
        }

        private void UnregisterOptionEvents()
        {
            saveButton.onClick.RemoveListener(SaveButton_Pressed);
        }

        private void SaveButton_Pressed()
        {
            SaveSettings();
        }
    }
}
