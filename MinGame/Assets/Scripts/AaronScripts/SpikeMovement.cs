using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    private const float YBOUNDS = 8;

    // Update is called once per frame
    void Update()
    {
        float currentY = transform.position.y;

        currentY -= Time.deltaTime * speed;

        if(currentY < -YBOUNDS)
        {
            Destroy(gameObject);
        }

        transform.position = new Vector3(transform.position.x, currentY, transform.position.z);
    }
}
