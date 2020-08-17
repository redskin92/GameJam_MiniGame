using UnityEngine;
using System.Collections.Generic;
using System;
using Player.Weapons;

namespace Inventory
{
    /// <summary>
    /// Player inventory.
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The name of the Inventory button.
        /// </summary>
        private const string INVENTORY_BUTTON_NAME = "Inventory";

        /// <summary>
        /// Inventory manager.
        /// </summary>
        [SerializeField]
        private InventoryManager inventoryManager;

        /// <summary>
        /// The amount of inventory space the player has.
        /// </summary>
        private int inventorySpace = 10;

        /// <summary>
        /// List of items in the inventory.
        /// </summary>
        private List<ItemBase> itemList = new List<ItemBase>();

        /// <summary>
        /// The currently equipped weapon.
        /// </summary>
        private WeaponBase currentWeapon;

        /// <summary>
        /// The currently equipped utility item.
        /// </summary>
        private UtilityItemBase currentUtilityItem;

        /// <summary>
        /// Is the inventory currently showing?
        /// </summary>
        private bool isInventoryShowing;

        #endregion

        #region Events

        public event EventHandler OnWeaponEquipped;

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

        #region Unity Methods

        private void Start()
        {
            inventoryManager.Initialize(InventorySpace, itemList);
            inventoryManager.OnItemUsed += Item_OnUsed;
        }

        private void Update()
        {
            if (Input.GetButtonDown(INVENTORY_BUTTON_NAME))
            {
                inventoryManager.ShowMenu(!isInventoryShowing);

                isInventoryShowing = !isInventoryShowing;
            }
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

            inventoryManager.Initialize(InventorySpace, itemList);
            return true;
        }

        /// <summary>
        /// Equip the item to the correct location.
        /// </summary>
        /// <param name="equippableItem">The item to equip.</param>
        private void EquipItem(ItemBase equippableItem)
        {
            var weapon = equippableItem as WeaponBase;
            if (weapon != null)
            {
                weapon.EquipItem();
                FireOnWeaponEquipped(weapon.weaponInfo);

                currentWeapon = weapon;
                itemList.Remove(weapon);
                return;
            }

            var utilityItem = equippableItem as UtilityItemBase;
            if (utilityItem != null)
            {
                utilityItem.EquipItem();

                currentUtilityItem = utilityItem;
                itemList.Remove(utilityItem);
                return;
            }
        }

        /// <summary>
        /// Consume the item.
        /// </summary>
        /// <param name="itemToConsume">The item to be consumed.</param>
        private void ConsumeItem(ItemBase itemToConsume)
        {
            if (itemToConsume.CurrentStack == 1)
                itemList.Remove(itemToConsume);
            else
                itemToConsume.CurrentStack--;
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
                ItemBase itemInInventory = itemList[itemList.IndexOf(usedItem)];

                var equippableItem = itemInInventory as IEquippable;
                if (equippableItem != null)
                {
                    EquipItem(itemInInventory);
                    return;
                }

                var consumableItem = itemInInventory as IConsumableItem;
                if (consumableItem != null)
                {
                    ConsumeItem(itemInInventory);
                    return;
                }
            }
        }

        private void FireOnWeaponEquipped(WeaponInfo info)
        {
            var handler = OnWeaponEquipped;
            if (handler != null)
            {
                handler(info, EventArgs.Empty);
            }
        }

        #endregion
    }
}