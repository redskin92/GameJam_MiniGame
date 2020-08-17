using UnityEngine;
using System;

namespace Inventory
{
    /// <summary>
    /// Inventory slot.
    /// </summary>
    public class InventorySlot : MonoBehaviour
    {
        #region Fields

        private ItemBase heldItem;

        #endregion

        #region Events

        public event EventHandler OnSlotClicked;

        #endregion

        #region Properties

        public ItemBase HeldItem {
            get
            {
                return heldItem;
            }
        }

        #endregion

        #region Methods

        public void StoreItem(ItemBase item)
        {
            heldItem = item;
        }

        #endregion

        #region Event Handling

        public void Slot_FireOnSlotClicked()
        {
            var handler = OnSlotClicked;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion
    }
}