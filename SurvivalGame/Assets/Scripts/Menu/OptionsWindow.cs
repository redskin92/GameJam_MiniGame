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
            saveButton.onClick.AddListener(SaveButton_Pressed);
        }
        
        public override void Open()
        {
            base.Open();

            allowLevelSelectToggle.isOn = SettingsManager.Instance.AllowLevelSelect;
        }

        public override void Close()
        {
            base.Close();
        }

        public void SaveSettings()
        {
            SettingsManager.Instance.AllowLevelSelect = allowLevelSelectToggle.isOn;
            SettingsManager.Instance.SaveSettings();
        }

        private void SaveButton_Pressed()
        {
            SaveSettings();
        }
    }
}
