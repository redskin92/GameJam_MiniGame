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

        public override void DataUpdated(Player.Player.PlayerEventParams data)
        {
            _health = data.currentHP;
            _maxHealth = data.maxHP;

            healthBar.fillAmount = HealthPercentage;
        }

        #endregion
    }
}
