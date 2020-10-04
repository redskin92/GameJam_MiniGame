using System;
using System.Collections.Generic;
using UnityEngine;

namespace HUD
{
    public class HUDManager : MonoBehaviour
    {
        #region Fields

        public static HUDManager Instance { get; private set; }

        public List<PlayerStatContainer> statContainers;

        #endregion

        #region Methods

        private void Awake()
        {
            Instance = this;
            Player.Player.PlayerHPChanged += UpdatePlayerData;
        }

        private void OnDestroy()
        {
            Player.Player.PlayerHPChanged -= UpdatePlayerData;
        }

        /// <summary>
        /// Event that should be called when the player's stats change.
        /// </summary>
        /// <param name="data"></param>
        public void UpdatePlayerData(Player.Player.PlayerEventParams data)
        {
            if (statContainers == null)
                return;
        
            for (int i = 0; i < statContainers.Count; i++)
            {
                statContainers[i].DataUpdated(data);
            }
        }

        #endregion
    }
}
