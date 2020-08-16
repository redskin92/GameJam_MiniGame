using System;
using UnityEngine;
using UnityEngine.Windows;
using File = System.IO.File;

namespace Settings
{
    public class SettingsManager
    {
        #region Fields

        private static SettingsManager _instance;

        private SettingsData _settingsData;

        #endregion

        #region Properties

        public static SettingsManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new SettingsManager();

                return _instance;
            }
        }

        private static string SettingsPath => Application.persistentDataPath + "/settings.json";

        #region Setting Accessors

        public bool AllowLevelSelect
        {
            get => _settingsData.allowLevelSelect;
            set => _settingsData.allowLevelSelect = value;
        }

        #endregion

        #endregion

        #region Methods

        SettingsManager()
        {
            LoadSettings();
        }

        public void SaveSettings()
        {
            string settingsJson = JsonUtility.ToJson(_settingsData);
            File.WriteAllText(SettingsPath, settingsJson);
            Debug.Log(SettingsPath);
        }

        public void LoadSettings()
        {
            if (File.Exists(SettingsPath))
                _settingsData = JsonUtility.FromJson<SettingsData>(File.ReadAllText(SettingsPath));
            else
            {
                _settingsData = new SettingsData();
                SaveSettings();
            }
        }

        #endregion

        #region Classes

        [Serializable]
        public class SettingsData
        {
            public bool allowLevelSelect;
        }

        #endregion
    }
}
