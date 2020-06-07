using UnityEngine;

namespace ElijahScripts.Tank
{
    public class TankColor : MonoBehaviour
    {
        [SerializeField]
        protected SpriteRenderer[] tankSpriteRenderers;

        public void SetColor(Color color)
        {
            color.a = 1.0f;
            for (int i = 0; i < tankSpriteRenderers.Length; i++)
            {
                tankSpriteRenderers[i].color = color;
            }
        }
    }
}
