using System;

namespace Inventory
{
    /// <summary>
    /// Interface for items that can be used.
    /// </summary>
    public interface IConsumableItem
    {
        /// <summary>
        /// Use this item.
        /// </summary>
        void UseItem();
    }
}