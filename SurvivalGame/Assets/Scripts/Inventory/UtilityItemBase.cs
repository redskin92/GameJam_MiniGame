
namespace Inventory
{
    /// <summary>
    /// Base class for Utility items.
    /// </summary>
    public class UtilityItemBase : ItemBase, IEquippable
    {
        #region IEquippable

        /// <summary>
        /// Equips the item and handle anything associated with that.
        /// </summary>
        public void EquipItem()
        {
            // TODO
        }

        #endregion
    }
}