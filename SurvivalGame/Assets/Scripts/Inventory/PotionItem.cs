using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Potion.  Used to restore HP.
    /// </summary>
    public class PotionItem : ItemBase, IConsumableItem
    {
        #region Fields

        /// <summary>
        /// The amount to heal.
        /// </summary>
        [SerializeField]
        private float amountToHeal;

        #endregion

        #region IConsumableItem

        /// <summary>
        /// Restores player HP if it is not already full.
        /// </summary>
        public void UseItem()
        {
            // TODO
        }

        #endregion
    }
}