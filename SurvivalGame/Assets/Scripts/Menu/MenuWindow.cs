using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu
{
    public abstract class MenuWindow : MonoBehaviour
    {
        public MenuVisibilityChangedEvent MenuVisibilityChanged;
        
        [SerializeField]
        protected Button closeMenuButton;

        public virtual void Open()
        {
            ChangeMenuVisibility(true);
        }

        public virtual void Close()
        {
            ChangeMenuVisibility(false);
        }
        
        public virtual void InitializeWindow()
        {
            if (MenuVisibilityChanged == null)
                MenuVisibilityChanged = new MenuVisibilityChangedEvent();
            closeMenuButton.onClick.AddListener(CloseMenuButton_Pressed);
            Close();
        }

        protected virtual void ChangeMenuVisibility(bool nowVisible)
        {
            gameObject.SetActive(nowVisible);
            MenuVisibilityChanged.Invoke(nowVisible);
        }

        protected virtual void CloseMenuButton_Pressed()
        {
            Close();
        }
    }

    public class MenuVisibilityChangedEvent : UnityEvent<bool>
    {
        
    }
}
