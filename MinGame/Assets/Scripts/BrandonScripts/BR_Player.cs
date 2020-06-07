using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BR_Player : MonoBehaviour
{
    [SerializeField]
    private TextMesh healthText;

    [SerializeField]
    private Vector3 bombOffset;
    
    [SerializeField]
    private Vector3 cameraOffset;

    [SerializeField]
    private SpriteRenderer avatarRenderer;


    private float health = 100.0f;

    private void Start()
    {
        healthText.text = health.ToString();
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public int GetScore()
    {
        return (int)(100 - health);
    }

    public void SetSprite(Sprite sprite)
    {
        avatarRenderer.sprite = sprite;
    }

    public void DamagePlayer(float damage)
    {
        damage = Mathf.Round(damage) * 4;
        health -= damage;
        if (health <= 0)
            health = 0.0f;

        healthText.text = health.ToString();
    }

    public Vector3 GetBombStartPosition()
    {
        Vector3 returnVal = this.transform.position + bombOffset;
        returnVal.z = 0.7f;
        return returnVal;
    }

    public Vector3 GetCameraStartOffset()
    {
        return this.transform.position + cameraOffset;
    }
}
