
namespace Inventory
{
    /// <summary>
    /// Interface for items that can be combined.
    /// </summary>
    public interface ICombinableItem
    {
        /// <summary>
        /// Combine this item with another item.
        /// </summary>
        /// <param name="otherItem">Item to combine with this item.</param>
        /// <returns>The resulting item.  Null if no combination could be made.</returns>
        ItemBase CombineItem(ICombinableItem otherItem);
    }
}