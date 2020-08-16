using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    /// <summary>
    /// Stat container that displays the player's health.
    /// </summary>
    public class PlayerHealthContainer : PlayerStatContainer
    {
        #region Fields

        [SerializeField]
        private Image healthBar;
        
        private float _health;
        private float _maxHealth;

        #endregion

        #region Properties

        private float HealthPercentage => _health / _maxHealth;

        #endregion

        #region Methods

        public override void DataUpdated(PlayerData data)
        {
            _health = data.health;
            _maxHealth = data.maxHealth;

            healthBar.fillAmount = HealthPercentage;
        }

        #endregion
    }
}
