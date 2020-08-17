using UnityEngine;
using System;

namespace Inventory
{
    /// <summary>
    /// Base class for items.
    /// </summary>
    public abstract class ItemBase : ScriptableObject
    {
        #region Fields

        /// <summary>
        /// The name of the item.
        /// </summary>
        [SerializeField]
        private string itemName;

        /// <summary>
        /// The max amount of this item that can be stacked.
        /// </summary>
        [SerializeField]
        private int maxStack;

        /// <summary>
        /// Image for the item.
        /// </summary>
        [SerializeField]
        private Sprite itemSprite;

        /// <summary>
        /// The current number of this item.
        /// </summary>
        private int currentStack;

        #endregion

        #region Events

        /// <summary>
        /// Fired when the item is used.
        /// </summary>
        public event EventHandler OnItemUsed;

        #endregion

        #region Properties

        /// <summary>
        /// Public accessor for itemName.
        /// </summary>
        public string ItemName
        {
            get { return itemName; }
        }

        /// <summary>
        /// Public accessor for maxStack.
        /// </summary>
        public int MaxStack
        {
            get { return maxStack; }
        }

        /// <summary>
        /// Public accessor for currentStack.
        /// </summary>
        public int CurrentStack
        {
            get { return currentStack; }
            set { currentStack = value; }
        }

        /// <summary>
        /// PUblic accessor for itemSprite.
        /// </summary>
        public Sprite ItemSprite
        {
            get { return itemSprite; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add an item to this item's stack.
        /// </summary>
        /// <param name="numToAdd">The number of items to add.</param>
        public void AddToStack(int numToAdd)
        {
            currentStack = Mathf.Min(MaxStack, currentStack + numToAdd);
        }

        /// <summary>
        /// Called when the player interacts with the item in the inventory.
        /// </summary>
        public void UseItem()
        {
            FireOnItemUsed();
        }

        #endregion

        #region Event Handling

        private void FireOnItemUsed()
        {
            var handler = OnItemUsed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion
    }
}