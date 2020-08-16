using UnityEngine;
using System.Collections.Generic;
using System;

namespace Inventory
{
    /// <summary>
    /// Player inventory.
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The amount of inventory space the player has.
        /// </summary>
        private int inventorySpace = 10;

        /// <summary>
        /// List of items in the inventory.
        /// </summary>
        private List<ItemBase> itemList;

        /// <summary>
        /// The currently equipped weapon.
        /// </summary>
        private WeaponBase currentWeapon;

        /// <summary>
        /// The currently equipped utility item.
        /// </summary>
        private UtilityItemBase currentUtilityItem;

        #endregion

        #region Properties

        /// <summary>
        /// Public accessor for inventory space.
        /// </summary>
        public int InventorySpace
        {
            get { return inventorySpace; }
            set { inventorySpace = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add an item to the inventory, if there is space.
        /// </summary>
        /// <param name="itemToAdd">The item to add.</param>
        /// <returns>True if the add was successful; false otherwise.</returns>
        public bool AddItem(ItemBase itemToAdd)
        {
            if (itemList.Exists(item => item.ItemName == itemToAdd.ItemName))
            {
                ItemBase heldItem = itemList.Find(item => item.ItemName == itemToAdd.ItemName);
                heldItem.AddToStack(itemToAdd.CurrentStack);

                return true;
            }

            if (itemList.Count == InventorySpace)
            {
                Debug.Log("Inventory full.  Cannot add item.");

                return false;
            }

            itemList.Add(itemToAdd);

            return true;
        }

        /// <summary>
        /// Equip the item to the correct location.
        /// </summary>
        /// <param name="equippableItem">The item to equip.</param>
        private void EquipItem(IEquippable itemToEquip)
        {
            var weapon = itemToEquip as WeaponBase;
            if (weapon != null)
            {
                weapon.EquipItem();

                currentWeapon = weapon;
                return;
            }

            var utilityItem = itemToEquip as UtilityItemBase;
            if (utilityItem != null)
            {
                utilityItem.EquipItem();

                currentUtilityItem = utilityItem;
                return;
            }
        }

        /// <summary>
        /// Consume the item.
        /// </summary>
        /// <param name="itemToConsume">The item to be consumed.</param>
        private void ConsumeItem(IConsumableItem itemToConsume)
        {
            ItemBase heldItem = itemList.Find(item => item.ItemName == (itemToConsume as ItemBase).ItemName);
            if (heldItem.CurrentStack == 1)
                itemList.Remove(heldItem);
            else
                heldItem.CurrentStack--;
        }

        #endregion

        #region Event Handling

        /// <summary>
        /// Called when an item is used.
        /// Uses the item appropriately depending on the type.
        /// </summary>
        /// <param name="sender">The item that was used.</param>
        /// <param name="args">Unused.</param>
        private void Item_OnUsed(object sender, EventArgs args)
        {
            var usedItem = sender as ItemBase;
            if (usedItem != null)
            {
                if (usedItem as IEquippable != null)
                {
                    EquipItem(usedItem as IEquippable);
                }
                else if (usedItem as IConsumableItem != null)
                {
                    ConsumeItem(usedItem as IConsumableItem);
                }
            }
        }

        #endregion
    }
}