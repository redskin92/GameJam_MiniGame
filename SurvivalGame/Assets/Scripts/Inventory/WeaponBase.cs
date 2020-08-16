
namespace Inventory
{
    /// <summary>
    /// Base class for weapons.
    /// </summary>
    public class WeaponBase : ItemBase, IEquippable
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