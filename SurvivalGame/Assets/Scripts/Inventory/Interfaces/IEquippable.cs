
namespace Inventory
{
    /// <summary>
    /// Interface for items that can be equipped as a weapon.
    /// </summary>
    public interface IEquippable
    {
        /// <summary>
        /// Equip this item.
        /// </summary>
        void EquipItem();
    }
}