using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace Inventory
{
    /// <summary>
    /// Manages the inventory display.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// Inventory UI.
        /// </summary>
        [SerializeField]
        private GameObject inventoryUI;

        /// <summary>
        /// Prefab for inventory slots.
        /// </summary>
        [SerializeField]
        private GameObject inventorySlotPrefab;

        /// <summary>
        /// List of inventory slots.
        /// </summary>
        private List<Image> inventorySlotList;

        #endregion

        #region Events

        /// <summary>
        /// Fires when the item is used.
        /// </summary>
        public event EventHandler OnItemUsed;

        #endregion

        #region Methods

        /// <summary>
        /// Initialize data and create objects.
        /// </summary>
        /// <param name="numSlots">Number of slots to draw.</param>
        /// <param name="heldItems">The list of held items.</param>
        public void Initialize(int numSlots, List<ItemBase> heldItems)
        {
            DestroyCurrent();

            CreateSlots(numSlots);

            AssignItems(heldItems);
        }

        /// <summary>
        /// Show/hide the menu.
        /// </summary>
        /// <param name="show">True if we should show, false otherwise.</param>
        public void ShowMenu(bool show)
        {
            inventoryUI.SetActive(show);
        }

        /// <summary>
        /// Create the background image for our slots.
        /// </summary>
        /// <param name="numSlots"></param>
        private void CreateSlots(int numSlots)
        {
            inventorySlotList = new List<Image>();

            for (int i = 0; i < numSlots; i++)
            {
                var slot = Instantiate(inventorySlotPrefab, inventoryUI.transform);
                slot.name = "Slot " + i;
                Image image = slot.GetComponent<Image>();

                slot.GetComponent<InventorySlot>().OnSlotClicked += Slot_OnSlotClicked;

                inventorySlotList.Add(image);
            }
        }

        /// <summary>
        /// Assign the appropriate image to our slots.
        /// </summary>
        /// <param name="heldItems">List of inventory items.</param>
        private void AssignItems(List<ItemBase> heldItems)
        {
            for (int i = 0; i < heldItems.Count; i++)
            {
                inventorySlotList[i].sprite = heldItems[i].ItemSprite;
            }
        }

        /// <summary>
        /// Destroy the current slots.
        /// </summary>
        private void DestroyCurrent()
        {
            if (inventorySlotList == null)
                return;

            for (int i = 0; i < inventorySlotList.Count; i++)
            {
                inventorySlotList[i].GetComponent<InventorySlot>().OnSlotClicked -= Slot_OnSlotClicked;
                Destroy(inventorySlotList[i].gameObject);
            }
        }

        #endregion

        #region Event Handling

        private void Slot_OnSlotClicked(object sender, EventArgs args)
        {
            InventorySlot slot = sender as InventorySlot;

            if (slot.HeldItem == null)
                return;

            FireOnItemUsed(slot.HeldItem);
        }

        private void FireOnItemUsed(ItemBase item)
        {
            var handler = OnItemUsed;
            if (handler != null)
                handler(item, EventArgs.Empty);
        }

        #endregion
    }
}